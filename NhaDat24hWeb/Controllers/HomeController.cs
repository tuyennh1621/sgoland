using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NhaDat24h.Common;
using NhaDat24h.Common.Configuration;
using NhaDat24h.DataAccess.Entities;
using NhaDat24h.DataDto.Company;
using NhaDat24h.DataDto.Ctv;
using NhaDat24h.DataDto.Users;
using NhaDat24h.Service;
using NhaDat24h.Service.Api.Company;
using NhaDat24h.Service.Api.Ctv;
using NhaDat24h.Service.Api.Users;
using NhaDat24hWeb.Models;
using System.Diagnostics;


namespace NhaDat24hWeb.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsersApiServices _usersApiServices;
        private readonly ICtvApiServices _ctvApiServices;
        private readonly IMyTypedClientServices _client;
        private readonly IHttpContextAccessor _contx;
        private readonly ICompanyApiServices _companyApiServices;
        private string directorio = AppConfigs.Directorio;
        private string projectId;
        private FirestoreDb _firestore;
        public HomeController(ILogger<HomeController> logger, IUsersApiServices usersApiServices,
            IMyTypedClientServices client, ICtvApiServices ctvApiServices, IHttpContextAccessor contx, ICompanyApiServices companyApiServices)
        {
            _logger = logger;
            _usersApiServices = usersApiServices;
            _client = client;
            _ctvApiServices = ctvApiServices;
            _companyApiServices = companyApiServices;
            _contx = contx;

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", directorio);
            projectId = "gostay-1ae07";
            _firestore = FirestoreDb.Create(projectId);
        }

        public IActionResult Index()
        {
            FormCTV ctv = new FormCTV();
            var listCompany = _companyApiServices.GetListCompanyByCtv(20);
            ctv.ListCongty = listCompany.Data;
            ctv.ListPhongBan = new List<PositionDepartmentDto> { new PositionDepartmentDto() { Id = 0, Description = "Tùy sắp xếp" } };
            return View(ctv);
        }

        [HttpPost]
        public IActionResult CreateCtvCong(FormCTV FormData)
        {
            string urlcv = "";
            string[] charsToRemove = new string[] { "@", "[", "]", "'" };


            UserInsertDataDto user = new UserInsertDataDto()
            {
                IdU = Guid.NewGuid().ToString(),
                Fullname = FormData.Fullname,
                Address = FormData.Address,
                Mobile = FormData.Mobile,
                GioiTinh = FormData.GioiTinh,
                Email = FormData.Email,
            };

            CheckRegisterOutput _IDUser = CheckEmailExisting(FormData.Email);

            if (_IDUser.Id == 0)
            {
                _IDUser.Id = _usersApiServices.InsertUser(user).Data;
            }

            UploadImagesResponse temp = new UploadImagesResponse();
            List<IFormFile> listImageCtv = new List<IFormFile> { FormData.filesAvatar1, FormData.filesAvatar2, FormData.filesCMT1, FormData.filesCMT2 };
            temp = _client.PostImgAndGetData(listImageCtv, 1024, _IDUser.Id.ToString(), 11);

            foreach (var c in charsToRemove)
            {
                temp.data = temp.data.Replace(c, string.Empty);
                temp.size = temp.size.Replace(c, string.Empty);
            }

            var url = temp.data.Split(",");
            string[] urlimg = new string[url.Length];
            for (int i = 0; i < url.Length; i++)
            {

                var x = Path.GetFileNameWithoutExtension(url[i]) + Path.GetExtension(url[i]).Replace("\"", "");
                urlimg[i] = $"/uploads/SGOland/ctv/" + _IDUser.Id.ToString() + "/" + x;

            }
            user.Avatar = urlimg[0];

            List<IFormFile> cv = new List<IFormFile> { FormData.filesCV };
            if (cv[0] != null)
            {
                var cvupload = _client.PostCvAndGetData(cv, _IDUser.Id.ToString());

                foreach (var c in charsToRemove)
                {
                    cvupload.data = cvupload.data.Replace(c, string.Empty);
                    cvupload.size = cvupload.size.Replace(c, string.Empty);
                }

                var y = Path.GetFileName(cvupload.data).Replace("\"", "");
                urlcv = $"/uploads/SGOland/ctv/" + _IDUser.Id.ToString() + "/" + y;
            }

            var updateavataruser = _usersApiServices.UpdateAvatarUser(new UpdateAvatarUserDto()
            {
                Id = _IDUser.Id,
                Url = user.Avatar,
            });

            var ctv = new CtvInsertDataDto()
            {

                Id = _IDUser.Id,
                FullName = FormData.Fullname,
                DateOfBirth = new DateTime(FormData.Year, FormData.Month, FormData.Day),
                CCCD = FormData.CCCD,
                Address = FormData.Address,
                Phone = FormData.Mobile,
                FrontImageId = urlimg[2],
                BackImageId = urlimg[3],
                Refid = FormData.Refid,
                IdCongty = FormData.IdCongty,
                Avatar2 = urlimg[1],
                GIOITHIEU = FormData.GIOITHIEU,
                CvUrl = urlcv,
                DepartmentId = FormData.DepartmentId,
            };



            var inserCtv = _ctvApiServices.InsertCtv(ctv);



            try
            {

                string strEmailist = _companyApiServices.GetEmailOfSuperior(FormData.DepartmentId ?? 0).Data;
                var timeTmp = DateTime.Now;
                var time = ((DateTimeOffset)timeTmp).ToUnixTimeSeconds().ToString();

                DocumentReference colRf = _firestore.Collection("sgoland-dangky").Document(System.Guid.NewGuid().ToString());

                Dictionary<string, object> value = new Dictionary<string, object>()
                {
                            {"Fullname", FormData.Fullname +", Mobile: "+ FormData.Mobile},
                            {"Gioithieu", FormData.GIOITHIEU ?? ""},
                            {"Emailcc", strEmailist},
                            {"Time", time },
                };

                colRf.CreateAsync(value);
            }
            catch
            {

            }

            return HomePage();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult LoginWithPhone()
        {
            var user = GetCurrentUser();
            if (user != null)
                return HomePage();
            return View();
        }
        public IActionResult RegisterUserForm(TbUser user)
        {
            var reusult = _usersApiServices.CheckUserByEmail(user.Email);
            if (reusult != null && reusult.Id > 0)
            {
                SetCurrentUserCookie(user);
                return HomePage();
            }
            else
            {
                return Content("Đăng nhập không thành công, Email: " + user.Email + " Không tồn tại");
            }
        }
        public IActionResult RegisterUserGooleForm(TbUser user)
        {
            var reusult = _usersApiServices.RegisterUserPhone(user);
            if (reusult != null)
            {
                SetCurrentUserCookie(user);
                return HomePage();
            }
            else
            {
                return Content("Đăng kí không thành công !");
            }
        }
        public IActionResult RegisterUserPhone(string phoneNumber)
        {
            TbUser user = new TbUser { Phone = phoneNumber.StandardizedPhoneNumber() };
            return View(user);
        }
        public IActionResult ExecutePhoneVerified(string number, string statusStr)
        {
            try
            {
                var phoneNumber = JsonConvert.DeserializeObject<string>(number).StandardizedPhoneNumber();
                bool status = JsonConvert.DeserializeObject<bool>(statusStr);
                if (status)
                {
                    var user = _usersApiServices.CheckUserByPhone(phoneNumber);
                    if (user != null)
                    {
                        SetCurrentUserCookie(user);
                    }
                    else
                    {
                        var reusult = _usersApiServices.RegisterUserPhone(new TbUser
                        {
                            Mobile = phoneNumber,
                            Email = $"{phoneNumber}@email",
                            Password = System.Guid.NewGuid().ToString().Substring(0, 4)
                        });
                        SetCurrentUserCookie(reusult);
                    }

                    var data = new
                    {
                        Dashboard = true,
                        verified = true
                    };
                    return Json(data);
                }
                else
                    return HomePage();
            }
            catch (Exception ex)
            {
                return HomePage();
            }
        }

        public void GoogleLogin()
        {
            HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse" +
                "")
            });
        }
        public void FacebookLogin()
        {
            HttpContext.ChallengeAsync(FacebookDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("FacebookResponse")
            });
        }
        public async Task<IActionResult> FacebookResponse()
        {
            try
            {
                var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                var claims = result.Principal.Identities
                    .FirstOrDefault().Claims.Select(claim => new
                    {
                        claim.Value
                    }).ToArray();
                var email = claims[1].Value;
                var user = _usersApiServices.CheckUserByEmail(email);
                if (user != null)
                {
                    SetCurrentUserCookie(user);
                    return HomePage();
                }
                else
                {
                    return RedirectToAction("RegisterUserEmail", "Home", new
                    {
                        email = claims[1].Value,
                        fullName = claims[2].Value,
                        lastName = claims[3].Value,
                        firstName = claims[4].Value
                    });
                }
                return Json(claims);
            }
            catch
            {
                return HomePage();
            }
        }
        public async Task<IActionResult> GoogleResponse()
        {
            try
            {
                var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                var claims = result.Principal.Identities
                    .FirstOrDefault().Claims.Select(claim => new
                    {
                        claim.Value
                    }).ToArray();
                var email = claims[5].Value;
                var user = _usersApiServices.CheckUserByEmail(email);
                if (user != null)
                {
                    SetCurrentUserCookie(user);
                    return HomePage();
                }
                else
                {
                    return RedirectToAction("RegisterUserEmail", "Home", new
                    {
                        fullName = claims[1].Value,
                        lastName = claims[2].Value,
                        firstName = claims[3].Value,
                        avatar = claims[4].Value,
                        email = claims[5].Value
                    });
                }
                return Json(claims);
            }
            catch
            {
                return HomePage();
            }
        }
        public IActionResult LoginbyAccount(string email, string password)
        {
            try
            {
                //email = JsonConvert.DeserializeObject<string>(email);
                //password = JsonConvert.DeserializeObject<string>(password);
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    return Json(false);
                }

                var user = _usersApiServices.CheckUserByAccount(email, password);
                if (user == null)
                    return Json(false);
                else
                {

                    SetCurrentUserCookie(user);
                    return Redirect("/Dashboard");
                }
            }
            catch (Exception ex)
            {
                return Json(false);
            }
            return default;
        }

        //[HttpPost]
        //public IActionResult UpdateUserInfo(string userJson)
        //{
        //var _user = JsonConvert.DeserializeObject<TbUser>(userJson);
        //dynamic data = null;
        //try
        //{
        //    var p = new DynamicParameters();
        //    p.Add("@user_id", _user.UserId, System.Data.DbType.Int32);
        //    p.Add("@user_name", _user.UserName, System.Data.DbType.String);
        //    p.Add("@address", _user.Address, System.Data.DbType.String);
        //    p.Add("@mobileNo", _user.MobileNo, System.Data.DbType.String);
        //    p.Add("@email", _user.Email, System.Data.DbType.String);
        //    p.Add("@picture", _user.Picture, System.Data.DbType.String);
        //    p.Add("@password", _user.Password, System.Data.DbType.String);

        //    var reusult = DapperExtensions.ExecuteDapperStoreProc(Procedures.User_updateinfobyIDnotEmail, p);

        //    SetCurrentUserCookie(_userRepository.CheckUserByID(_user.UserId));

        //    data = new
        //    {
        //        status = "success",
        //        message = "Cập nhật thành công"
        //    };
        //    return Json(data);
        //}
        //catch (DbUpdateConcurrencyException)
        //{
        //    data = new
        //    {
        //        status = "fail!",
        //        message = "Cập nhật thất bại"
        //    };
        //    return Json(data);
        //}
        //}

        //[HttpPost]
        //public IActionResult loginWidthFireBase(string jsonUser)
        //{
        //    var User = JsonConvert.DeserializeObject<user>(jsonUser);

        //    var user = _userRepository.CheckUserByEmail(User.Email);
        //    if (user != null)
        //    {
        //        SetCurrentUserCookie(user);
        //    }
        //    else
        //    {
        //        var reusult = _userRepository.RegisterUserEmail(new User
        //        {
        //            MobileNo = "",
        //            UserName = User.DisplayName,
        //            Email = User.Email,
        //            Picture = User.PhotoURL,
        //            Password = System.Guid.NewGuid().ToString().Substring(0, 4)
        //        });
        //        SetCurrentUserCookie(reusult);
        //    }
        //    return HomePage();
        //}

        //public IActionResult RegisterUserEmail(string fullName, string lastName, string firstName, string email, string avatar)
        //{
        //    User user = new User { UserName = fullName, LastName = lastName, FirstName = firstName, Email = email, Picture = avatar };
        //    return View(user);
        //}
        public async Task<IActionResult> Logout()
        {
            RemoveCurrentUserCookie();
            return HomePage();
        }
        public CheckRegisterOutput CheckEmailExisting(string email)
        {
            var checkexis = _usersApiServices.CheckEmailExisting(email);
            var data = checkexis.Data;
            ViewBag.dataemail = data;
            return data;
        }

        public CheckRegisterOutput CheckPhoneExisting(string phone)
        {
            var checkexis = _usersApiServices.CheckPhoneExisting(phone);
            var data = checkexis.Data;
            ViewBag.dataPhone = data;
            return data;
        }

        public int CheckRefidExisting(int refid)
        {
            var checkexis = _usersApiServices.CheckRefidExisting(refid);
            var data = checkexis.Data;
            ViewBag.dataRefid = data;
            return data.Status;
        }

        public int CheckidExisting(int id)
        {
            var checkexis = _ctvApiServices.GetCtv(id);
            if (checkexis.Data != null && checkexis.Data.Status == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }


        public async Task<IActionResult> GooleLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return HomePage();
        }


    }
}