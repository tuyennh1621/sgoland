using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using NhaDat24h.Common;
using NhaDat24h.Common.Configuration;
using NhaDat24h.CommonCode;
using NhaDat24h.DataDto.Authen;
using NhaDat24h.DataDto.Company;
using NhaDat24h.DataDto.Ctv;
using NhaDat24h.DataDto.User;
using NhaDat24h.DataDto.Users;
using NhaDat24h.Service;
using NhaDat24h.Service.Api.Company;
using NhaDat24h.Service.Api.Ctv;
using NhaDat24h.Service.Api.RealEstates;
using NhaDat24h.Service.Api.Users;
using System.Diagnostics;

namespace NhaDat24hWeb.Areas.Partner.Controllers
{
    [Area("Partner")]
    public class UsersController : Controller
    {
        private string directorio = AppConfigs.Directorio;
        private string projectId;
        private IRealEstateApiServices _reApiServices;
        private FirestoreDb _firestore;
        private IHttpContextAccessor _httpContextAccessor;
        private IUsersApiServices _usersApiServices;
        private ICtvApiServices _ctvApiServices;
        private ICompanyApiServices _companyApiServices;
        private readonly IMyTypedClientServices _client;
        private ISessionManager _sessionManager;

        public class FormatMess
        {
            public string ErrorCode { get; set; }
            public string ErrorMess { get; set; }
        }

        public UsersController(IHttpContextAccessor httpContextAccessor, IUsersApiServices usersApiServices,
            ISessionManager sessionManager, ICtvApiServices ctvApiServices, ICompanyApiServices companyApiServices, IMyTypedClientServices client, IRealEstateApiServices reApiServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _usersApiServices = usersApiServices;
            _sessionManager = sessionManager;
            _ctvApiServices = ctvApiServices;
            _reApiServices = reApiServices;
            _companyApiServices = companyApiServices;
            _client = client;

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", directorio);
            projectId = "gostay-1ae07";
            _firestore = FirestoreDb.Create(projectId);
        }

        [Route("user")]
        public IActionResult Index()
        {

            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var Permission = _companyApiServices.GetPositionCtv(UserId);
            if (Permission.Data.Select(x => x.IdPosition).Contains(10) ||
                Permission.Data.Select(x => x.IdPosition).Contains(11) ||
                Permission.Data.Select(x => x.IdPosition).Contains(12) ||
                Permission.Data.Select(x => x.IdPosition).Contains(999999999) ||
                Permission.Data.Select(x => x.IdPositionDepartment).Contains(999999999))
            {
                return Json(ErrorCodeMessage.NoPermission.Value);
            }
 
            var dataSearch = _ctvApiServices.GetListCtv(UserId, null, null, 10, 0, 0, 0, 10, 1);

            if (dataSearch.Code == ErrorCodeMessage.NoPermission.Key )
            {
                return Json(ErrorCodeMessage.NoPermission.Value);
            }
            else
            {
                ModelListCtv model = new ModelListCtv()
                {
                    ListCtv = dataSearch.Data.ListCtv,
                    PageIndex = 1,
                    PageSize = 10,
                    ListCompany = dataSearch.Data.ListCompany,
                    Permission = Permission.Data.Select(x => x.IdPositionDepartment).ToList(),
                };
                if (Permission.Data.Any(x => x.IdPositionDepartment==1))
                {
                    model.isAdmin=true;
                }
                return View(model);
            }
        }

        [Route("user/ctv")]
        [HttpGet]
        public IActionResult ListCtv(int? idctv, string? searchkey, int? status
            , int pageSize, int pageIndex, int? department = 0, int idCompany = 0, int numdayoff = 0
            )
        {
            //var t1 = new Stopwatch();
            //var t2 = new Stopwatch();
            //var t3 = new Stopwatch();

            //t3.Start();
            if (status == null)
                status = 10;

            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            //t1.Start();
            var Permission = _companyApiServices.GetPositionCtv(UserId);
            if (Permission.Data.Select(x => x.IdPosition).Contains(10) ||
                Permission.Data.Select(x => x.IdPosition).Contains(11) ||
                Permission.Data.Select(x => x.IdPosition).Contains(12) ||
                Permission.Data.Select(x => x.IdPosition).Contains(999999999) ||
                Permission.Data.Select(x => x.IdPositionDepartment).Contains(999999999))
            {
                return Json(ErrorCodeMessage.NoPermission.Value);
            }
            //t1.Stop();
            if (searchkey != null)
            {
                searchkey = searchkey.RemoveUnicode();
                searchkey = searchkey.Replace(" ", string.Empty).ToLower();
            }
            //t2.Start();
            var dataSearch = _ctvApiServices.GetListCtv(UserId, idctv, searchkey, status, idCompany, department, numdayoff, pageSize, pageIndex);
            //t2.Stop();
            if (dataSearch.Code == ErrorCodeMessage.NoPermission.Key )
            {
                return Json(ErrorCodeMessage.NoPermission.Value);
            }
            else
            {
                ModelListCtv model = new ModelListCtv()
                {
                    ListCtv = dataSearch.Data.ListCtv,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    ListCompany = dataSearch.Data.ListCompany,
                    Permission = Permission.Data.Select(x => x.IdPositionDepartment).ToList()
                };
                //t3.Stop();
                return PartialView("~/Areas/Partner/Views/Users/ListCtvPartial.cshtml", model);
            }
        }

        [Route("export-list-ctv-report-excell")]
        public IActionResult ExportListCtvReportExcell(int? idctv, string? searchkey, int? status,
             int? companyid = 0, int? department = 0, int numdayoff = 0)
        {
            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            if (status == null)
                status = 10;
            try
            {
                var report = _usersApiServices.GetListCtvReportExcel(new SearchCtvParam()
                {
                    idUser = UserId,
                    idctv = idctv,
                    searchkey = searchkey,
                    status = status,
                    idDepartment = department,
                    idCompany = companyid,
                    numdayoff = numdayoff,
                });

                MemoryStream ms = new MemoryStream();
                report.CopyTo(ms);
                return File(ms.ToArray(), "application/vnd.ms-excel", "Nhansu_sgolandxlsx.xlsx");

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [Route("user/edit")]
        public IActionResult EditUser(int IdCtv)
        {
            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var positionUser = _companyApiServices.GetPositionCtv(UserId);
            var positionCtv = _companyApiServices.GetPositionCtv(IdCtv);
            if (positionUser.Data.Max(x => x.ClassPosition) <= positionCtv.Data.Max(x => x.ClassPosition) && positionUser.Data.Min(x => x.IdCompany) > 0)
            {
                var message = "You are not authorized to do this action";
                return Json(message);
            }


            CtvEditDto ctv = _ctvApiServices.GetCtv(IdCtv).Data;
            ctv.ListCongty = _companyApiServices.GetListCompanyByCtv(UserId).Data;
            ViewBag.ListDepartment = _companyApiServices.GetDepartmentInCompanyByCtv(new GetDepartmentInCompanyByCtvParam()
            {
                IdCtv = UserId,
                IdCompany = new List<int> { 0 }
            });
            if (positionUser.Data.Select(x => x.IdPositionDepartment).Contains(1))
            {
                ctv.isAdmin = true;
            }

            return View(ctv);
        }
        [HttpPost]
        [Route("user/edit-submit")]
        public IActionResult SubmitEditUser(CtvEditDto param, int company)
        {
            UploadImagesResponse temp = new UploadImagesResponse();
            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var positionUser = _companyApiServices.GetPositionCtv(UserId);
            var avatar = "";
            var avatar2 = "";
            var front = "";
            var back = "";
            var cv = "";
            if (param.AvtEdit != null)
            {
                List<IFormFile> Avt = new List<IFormFile> { param.AvtEdit };
                temp = _client.PostImgAndGetData(Avt, 1024, param.Id.ToString(), 11);

                string[] charsToRemove = new string[] { "@", "[", "]", "'" };
                foreach (var c in charsToRemove)
                {
                    temp.data = temp.data.Replace(c, string.Empty);
                    temp.size = temp.size.Replace(c, string.Empty);
                }
                var url = temp.data;

                for (int i = 0; i < url.Length; i++)
                {

                    var x = Path.GetFileNameWithoutExtension(url) + Path.GetExtension(url).Replace("\"", "");
                    avatar = $"/uploads/SGOland/ctv/" + param.Id.ToString() + "/" + x;

                }
            }
            if (param.Avt2Edit != null)
            {
                List<IFormFile> Avt2 = new List<IFormFile> { param.Avt2Edit };
                temp = _client.PostImgAndGetData(Avt2, 1024, param.Id.ToString(), 11);

                string[] charsToRemove = new string[] { "@", "[", "]", "'" };
                foreach (var c in charsToRemove)
                {
                    temp.data = temp.data.Replace(c, string.Empty);
                    temp.size = temp.size.Replace(c, string.Empty);
                }
                var url = temp.data;

                for (int i = 0; i < url.Length; i++)
                {

                    var x = Path.GetFileNameWithoutExtension(url) + Path.GetExtension(url).Replace("\"", "");
                    avatar2 = $"/uploads/SGOland/ctv/" + param.Id.ToString() + "/" + x;

                }
            }
            if (param.FrontIdEdit != null)
            {
                List<IFormFile> FrontImg = new List<IFormFile> { param.FrontIdEdit };
                temp = _client.PostImgAndGetData(FrontImg, 1024, param.Id.ToString(), 11);

                string[] charsToRemove = new string[] { "@", "[", "]", "'" };
                foreach (var c in charsToRemove)
                {
                    temp.data = temp.data.Replace(c, string.Empty);
                    temp.size = temp.size.Replace(c, string.Empty);
                }
                var url = temp.data;

                for (int i = 0; i < url.Length; i++)
                {

                    var x = Path.GetFileNameWithoutExtension(url) + Path.GetExtension(url).Replace("\"", "");
                    front = $"/uploads/SGOland/ctv/" + param.Id.ToString() + "/" + x;

                }
            }
            if (param.BackIdEdit != null)
            {
                List<IFormFile> BackImg = new List<IFormFile> { param.BackIdEdit };
                temp = _client.PostImgAndGetData(BackImg, 1024, param.Id.ToString(), 11);

                string[] charsToRemove = new string[] { "@", "[", "]", "'" };
                foreach (var c in charsToRemove)
                {
                    temp.data = temp.data.Replace(c, string.Empty);
                    temp.size = temp.size.Replace(c, string.Empty);
                }
                var url = temp.data;

                for (int i = 0; i < url.Length; i++)
                {

                    var x = Path.GetFileNameWithoutExtension(url) + Path.GetExtension(url).Replace("\"", "");
                    back = $"/uploads/SGOland/ctv/" + param.Id.ToString() + "/" + x;

                }
            }
            if (param.CVEdit != null)
            {
                List<IFormFile> CvUrl = new List<IFormFile> { param.CVEdit };
                var cvupload = _client.PostCvAndGetData(CvUrl, param.Id.ToString());

                string[] charsToRemove = new string[] { "@", "[", "]", "'" };
                foreach (var c in charsToRemove)
                {
                    cvupload.data = cvupload.data.Replace(c, string.Empty);
                    cvupload.size = cvupload.size.Replace(c, string.Empty);
                }
                var y = Path.GetFileName(cvupload.data).Replace("\"", "");
                cv = $"/uploads/SGOland/ctv/" + param.Id.ToString() + "/" + y;
            }
            var ctv = new CtvEditDto()
            {
                Id = param.Id,
                IdUser = UserId,
                FullName = param.FullName,
                Socmtnd = param.Socmtnd,
                Ngaysinh = param.Ngaysinh,
                GioiTinh = param.GioiTinh,
                Diachi = param.Diachi,
                Mobile = param.Mobile,
                Email = param.Email,
                IdcongTy = company,
                Refid = param.Refid,
                Avatar = param.Avatar,
                Avatar2 = param.Avatar2,
                Frontimageid = param.Frontimageid,
                Backimageid = param.Backimageid,
                CvUrl = param.CvUrl,
                DepartmentId = param.DepartmentId,
                Status = param.Status,
                IdUserRequest = UserId,
                Gioithieu = param.Gioithieu,
                FaceBook = param.FaceBook,
                Twitter = param.Twitter,
                Position = param.Position,
                ListCongty = _companyApiServices.GetListCompanyByCtv(UserId).Data
            };
            if (avatar != null && avatar != "")
            {
                ctv.Avatar = avatar;
            }
            if (avatar2 != null && avatar2 != "")
            {
                ctv.Avatar2 = avatar2;
            }
            if (front != null && front != "")
            {
                ctv.Frontimageid = front;
            }
            if (back != null && back != "")
            {
                ctv.Backimageid = back;
            }
            if (cv != null && cv != "")
            {
                ctv.CvUrl = cv;
            }
            var updateuser = _ctvApiServices.UpdateCtv(ctv);

            if (positionUser.Data.Select(x => x.IdPositionDepartment).Contains(1))
            {
                ctv.isAdmin = true;
            }

            return PartialView("~/Areas/Partner/Views/Users/EditUser.cshtml", ctv);
        }

        [Route("user/changestatus")]
        [HttpGet]
        public IActionResult ChangeCtvStatus(int IdCtv, byte Status)
        {
                var User = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var positionUser = _companyApiServices.GetPositionCtv(User);
            var positionCtv = _companyApiServices.GetPositionCtv(IdCtv);
            if (positionUser.Data.Max(x => x.ClassPosition) <= positionCtv.Data.Max(x => x.ClassPosition) && positionUser.Data.Min(x => x.IdCompany) > 0)
            {
                var message = "You are not authorized to do this action";
                return Json("0");
            }
            var ctv = _usersApiServices.CheckUserByID(IdCtv.ToString());
            var result = _ctvApiServices.UpdateStatusCtv(new UpdateStatusCtvDto()
            {
                IdUser = User,
                IdCtv = IdCtv,
                Status = Status
            });
            if (Status == 1)
            {
                AutosendEmail(ctv.Data.Email, "Xin chào mừng! Tài khoản của bạn đã được phê duyệt. Email đăng nhập là: " + ctv.Data.Email + ", mật khẩu là: " + ctv.Data.Password + ", đăng nhập tại: Sgoland.vn");
            }
            else
            {
                AutosendEmail(ctv.Data.Email, "Tài khoản của bạn đã bị thu hồi.");
            }
            return Json("1");
        }

        [Route("user/deletectv")]
        [HttpGet]
        public IActionResult DeleteCtv(int IdCtv)
        {
            var User = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var positionUser = _companyApiServices.GetPositionCtv(User);
            var positionCtv = _companyApiServices.GetPositionCtv(IdCtv);
            if (positionUser.Data.Max(x => x.ClassPosition) <= positionCtv.Data.Max(x => x.ClassPosition) && positionUser.Data.Min(x => x.IdCompany) > 0)
            {
                var message = "You are not authorized to do this action";
                return Json("0");
            }
            var ctv = _usersApiServices.CheckUserByID(IdCtv.ToString());
            var result = _ctvApiServices.DeleteCtv(User, IdCtv, "");

            return Json("1");
        }
        [Route("user/lockctv")]
        [HttpGet]
        public IActionResult LockCtv(int IdCtv)
        {
            var User = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var positionUser = _companyApiServices.GetPositionCtv(User);
            var positionCtv = _companyApiServices.GetPositionCtv(IdCtv);
            if (positionUser.Data.Max(x => x.ClassPosition) <= positionCtv.Data.Max(x => x.ClassPosition) && positionUser.Data.Min(x => x.IdCompany) > 0)
            {
                var message = "You are not authorized to do this action";
                return Json("0");
            }
            var ctv = _usersApiServices.CheckUserByID(IdCtv.ToString());
            var result = _ctvApiServices.LockCtv(User, IdCtv, "");

            return Json(result.Message);
        }
        [Route("user/changedepartment")]
        [HttpGet]
        public IActionResult ChangeCtvDepartment(int IdCtv, decimal departmentIdNew, decimal departmentIdOld, string chucvu)
        {
            var User = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var positionUser = _companyApiServices.GetPositionCtv(User);
            var positionCtv = _companyApiServices.GetPositionCtv(IdCtv);
            if (positionUser.Data.Max(x => x.ClassPosition) <= positionCtv.Data.Max(x => x.ClassPosition) && positionUser.Data.Min(x => x.IdCompany) > 0)
            {
                var message = "You are not authorized to do this action";
                return Json(message);
            }
            var ctv = _usersApiServices.CheckUserByID(IdCtv.ToString());
            var result = _ctvApiServices.UpdateDepartmentCtv(new UpdateDepartmentCtvDto()
            {
                IdUser = User,
                IdCtv = IdCtv,
                IdDepartmentNew = departmentIdNew,
                IdDepartmentOld = departmentIdOld,
            });
            if (departmentIdNew != 999999999999)
            {
                AutosendEmail(ctv.Data.Email, "Xin chào mừng! Tài khoản của bạn đã được phê duyệt vào chức vụ: <b>" + chucvu + "</b>. Email đăng nhập là: " + ctv.Data.Email + ", mật khẩu là: " + ctv.Data.Password + ", đăng nhập tại: Sgoland.vn");
            }
            else
            {
                AutosendEmail(ctv.Data.Email, "Tài khoản của bạn đã bị thu hồi vị trí.");
            }
            return Json(result.Message);
        }

        [Route("user/listpartner")]
        [HttpGet]
        public IActionResult GetListPartner(int IdCtv)
        {
            var result = _usersApiServices.GetListPartner(IdCtv, 50, 1);

            return PartialView("~/Areas/Partner/Views/Users/ListPartnerPartial.cshtml", result.Data);
        }
        //permission 
        [Route("user/listpermission")]
        [HttpGet]
        public IActionResult GetListPermission(int IdCtv, string NameCtv)
        {
            string message = "";
            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var positionCtv = _companyApiServices.GetPositionCtv(IdCtv).Data;

            var check = _companyApiServices.IsSuperior(UserId, IdCtv);
            if (check.Data == false)
            {
                 message = "Bạn không có quyền thực hiện chức năng này";
                return Json(message);
            }
            if (positionCtv.Count()==0)
            {
                 message = "Cần duyệt vị trí trước khi phân quyền";
                return Json(message);
            }
            else
            {
                if(positionCtv.Count()==1 && positionCtv.FirstOrDefault().Apply!=true)
                {
                     message = "Cần duyệt vị trí trước khi phân quyền";
                    return Json(message);
                }    
            }    
            var listPermission = _usersApiServices.GetListPermission();
            var model = new AuthorityModel()
            {
                IdCtv = IdCtv,
                NameCtv = NameCtv,
                ListPermission = listPermission.Data,
                listpermissionSelected = _usersApiServices.GetUserPermission(IdCtv).Data.Select(x=>x.Code).ToList(),
            };
            return PartialView("~/Areas/Partner/Views/Users/ListPermissionPartial.cshtml", model);
        }

        [Route("user/brower-permission")]
        public string BrowerPermission(string listID = "", int idCTV = 0)
        {
            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var check = _companyApiServices.IsSuperior(UserId, idCTV);
            if (check.Data == false)
            {
                var message = "Bạn không có quyền thực hiện chức năng này";
                return message;
            }

            string errormess = "Phê duyệt không thành công, bạn cần chọn quyền trước khi phê duyệt";
            if (string.IsNullOrEmpty(listID))
            {
                return errormess;
            }
            else
            {
                var _listID = listID?.Split(',')?.Select(Int32.Parse)?.ToList();
                if (_listID != null && _listID.Count > 0)
                {
                    var duyet = _usersApiServices.SetUserPermission(new SetPermisionParam { IdCtv = idCTV, IdUser = UserId, Permission = _listID }).Data;

                    return duyet.ToString();
                }
                else
                {
                    return errormess;
                }
            }
        }
        //
        [Route("user/get-model-duyet-vitri")]
        [HttpGet]
        public IActionResult getModelDuyetViTri(int IdCtv, string ListcongtySelected, string ListphongbanSelected, string ListVitriSelected)
        {
            var User = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var positionUser = _companyApiServices.GetPositionCtv(User);
            var positionCtv = _companyApiServices.GetPositionCtv(IdCtv);
            if (positionUser.Data.Max(x => x.ClassPosition) <= positionCtv.Data.Max(x => x.ClassPosition) && positionUser.Data.Min(x => x.IdCompany) > 0)
            {
                var message = "Bạn không có quyền thay đổi tài khoản này";
                return Json(message);
            }
            var ctv = _ctvApiServices.GetCtv(IdCtv).Data;
            var _ModelFormPheDuyet = new ModelFormPheDuyet();


            _ModelFormPheDuyet.ListCongTy = _companyApiServices.GetListCompanyByCtv(User).Data;
            _ModelFormPheDuyet.ListIDCongTySelected = ListcongtySelected?.Split(',')?.Select(Int32.Parse)?.ToList();
            _ModelFormPheDuyet.ListIDPhongBanSelected = ListphongbanSelected?.Split(',')?.Select(Int32.Parse)?.ToList();
            _ModelFormPheDuyet.ListIDViTriSelected = ListVitriSelected?.Split(',')?.Select(Int32.Parse)?.ToList();

            if (_ModelFormPheDuyet.ListIDCongTySelected != null && _ModelFormPheDuyet.ListIDCongTySelected.Count > 0)
            {
                List<DepartmentDto> list = _companyApiServices.GetDepartmentInCompanyByCtv(new GetDepartmentInCompanyByCtvParam { IdCtv = User, IdCompany = _ModelFormPheDuyet.ListIDCongTySelected }).Data;

                if (list != null && list.Count > 0)
                {
                    _ModelFormPheDuyet.ListPhongBan = new List<MultiSelectGroup>();
                    foreach (var id in _ModelFormPheDuyet.ListIDCongTySelected)
                    {
                        _ModelFormPheDuyet.ListPhongBan.Add(new MultiSelectGroup { Label = list.Where(x => x.IdCompany == id).FirstOrDefault()?.CompanyName ?? "ID Công ty: " + id.ToString(), Options = list.Where(x => x.IdCompany == id).Select(item => new MulltiKeyValue { Id = item.Id.ToString(), Title = item.Name }).ToList() });
                    }


                }

            }
            if (_ModelFormPheDuyet.ListIDPhongBanSelected != null && _ModelFormPheDuyet.ListIDPhongBanSelected.Count > 0)
            {
                List<PositionDepartmentDto> list = _companyApiServices.GetStillPositionInDepartment(new GetStillPositionInDepartmentParam()
                {
                    IdDepartment = _ModelFormPheDuyet.ListIDPhongBanSelected ?? new List<int> { 0 },
                    IdCtv = User
                }).Data;
                if (list != null && list.Count > 0)
                {
                    _ModelFormPheDuyet.ListVitri = new List<MultiSelectGroup>();

                    foreach (var id in _ModelFormPheDuyet.ListIDPhongBanSelected)
                    {
                        _ModelFormPheDuyet.ListVitri.Add(new MultiSelectGroup { Label = list.Where(x => x.IdDepartment == id).FirstOrDefault()?.DepartmentName ?? "ID Phòng ban: " + id.ToString(), Options = list.Where(x => x.IdDepartment == id).Select(item => new MulltiKeyValue { Id = item.Id.ToString(), Title = item.Description }).ToList() });
                    }

                }

            }

            _ModelFormPheDuyet.IdCtv = IdCtv;


            return PartialView("~/Areas/Partner/Views/Users/duyetvitri.cshtml", _ModelFormPheDuyet);
        }

        [Route("user/resendemail")]
        [HttpGet]
        public IActionResult reSendEmail(int IdCtv)
        {

            try
            {
                var User = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
                var ctv = _usersApiServices.CheckUserByID(IdCtv.ToString());
                _ctvApiServices.UpdateStatusCtv(new UpdateStatusCtvDto { IdCtv = IdCtv, IdUser = User, Status = 0 });
                AutosendEmail(ctv.Data.Email, "Email đăng nhập là: " + ctv.Data.Email + ", mật khẩu là: " + ctv.Data.Password + ", đăng nhập tại: Sgoland.vn");
                return Json("");

            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [Route("user/updateStatusOnline")]
        [HttpGet]
        public IActionResult updateStatusOnline(bool status)
        {
            try
            {
                var User = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
                _usersApiServices.ChangeOnlineStatus(new ChangeOnOffUser { IdUser = User, OnOff = status });
                return Json("Ok");
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [Route("user/insertcountertimeonline")]
        [HttpGet]
        public IActionResult InsertCountertimeOnline(int counter)
        {
            try
            {
                var User = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
                _ctvApiServices.InsertCountertimeOnline(new CounterTimeOnlineDto
                {
                    IdCtv = User,
                    Counter = counter,
                    Date = DateTime.Now.Date
                });
                return Json("Ok");
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public void AutosendEmail(string email, string Content)
        {

            FormatMess results = new FormatMess();
            results.ErrorCode = "-1";



            results.ErrorMess = "System error, please check email again!";
            try
            {
                var timeTmp = DateTime.Now;
                var time = ((DateTimeOffset)timeTmp).ToUnixTimeSeconds().ToString();
                DocumentReference colRf = _firestore.Collection("sgoland-pheduyet").Document(System.Guid.NewGuid().ToString());
                Dictionary<string, object> value = new Dictionary<string, object>()
                    {
                            {"Email", email },
                            {"Content", Content },
                            {"Time", time },
                        };
                colRf.CreateAsync(value);
                results.ErrorCode = "0";
                results.ErrorMess = "Success";

            }
            catch (Exception ex)
            {
                results.ErrorCode = "-1";
                results.ErrorMess = "System error, please check email again!";
            }
        }

        [Route("user/binDropdownAjax")]
        public IActionResult binDropdownAjax(string obj = "0", int style = 1)
        {

            List<MulltiKeyValue> v = new List<MulltiKeyValue>();

            if (style == 1)
            {
                var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;

                var list = _companyApiServices.GetDepartmentInCompanyByCtv(new GetDepartmentInCompanyByCtvParam()
                {
                    IdCtv = UserId,
                    IdCompany = new List<int>() { int.Parse(obj) }
                }).Data;

                foreach (var m in list)
                {
                    v.Add(new MulltiKeyValue { Id = m.Id.ToString(), Title = m.Name });
                }
            }
            else if (style == 2)
            {
                var list = _companyApiServices.GetStillPositionInCompanyLowLevel(int.Parse(obj)).Data;
                v.Add(new MulltiKeyValue { Id = "0", Title = "Theo sự sắp xếp của công ty" });
                if (int.Parse(obj) > 0 && list.Count > 0)
                {
                    foreach (var m in list)
                    {
                        v.Add(new MulltiKeyValue { Id = m.Id.ToString(), Title = m.Description });
                    }
                }
            }
            else if (style == 3)
            {
                if (obj!=null)
                {
                    var list = _reApiServices.GetListDistrictByIdProvince(int.Parse(obj)).Data;
                    if (int.Parse(obj) > 0 && list.Count > 0)
                    {
                        foreach (var m in list)
                        {
                            if (m.Tenquan == "Tất cả")
                            {
                                v.Add(new MulltiKeyValue { Id = "0", Title = m.Tenquan });
                            }
                            else
                            {
                                v.Add(new MulltiKeyValue { Id = m.IdQ.ToString(), Title = m.Tenquan });
                            }
                        }
                    }
                }
                

            }
            else if (style == 4)
            {
                var list = _reApiServices.GetListWardsByIdDistrict(int.Parse(obj)).Data;
                if (int.Parse(obj) > 0 && list.Count > 0)
                {
                    v.Add(new MulltiKeyValue { Id = "0", Title = "Tất cả" });
                    foreach (var m in list)
                    {
                        v.Add(new MulltiKeyValue { Id = m.Id.ToString(), Title = m.Tenphuong });
                    }
                }

            }
            else if (style == 5)
            {
                List<MultiSelectGroup> Model = new List<MultiSelectGroup>();

                var listID = obj?.Split(',')?.Select(Int32.Parse)?.ToList();
                var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;

                List<DepartmentDto> list = _companyApiServices.GetDepartmentInCompanyByCtv(new GetDepartmentInCompanyByCtvParam { IdCtv = UserId, IdCompany = listID }).Data;

                if (!string.IsNullOrEmpty(obj) && list.Count > 0)
                {
                    if (listID != null && listID.Count > 0)
                    {
                        foreach (var id in listID)
                        {

                            Model.Add(new MultiSelectGroup { Label = list.Where(x => x.IdCompany == id).FirstOrDefault()?.CompanyName ?? "ID Công ty: " + id.ToString(), Options = list.Where(x => x.IdCompany == id).Select(item => new MulltiKeyValue { Id = item.Id.ToString(), Title = item.Name }).ToList() });
                        }
                    }
                }
                return PartialView("~/Views/Shared/_MultiSelectGroup.cshtml", Model);
            }
            else if (style == 6)
            {
                List<MultiSelectGroup> Model = new List<MultiSelectGroup>();

                var listID = obj?.Split(',')?.Select(Int32.Parse)?.ToList();
                var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;

                var list = _companyApiServices.GetStillPositionInDepartment(new GetStillPositionInDepartmentParam()
                {
                    IdDepartment = listID ?? new List<int> { 0 },
                    IdCtv = UserId
                }).Data;

                if (!string.IsNullOrEmpty(obj) && list != null && list.Count > 0)
                {
                    if (listID != null && listID.Count > 0)
                    {
                        foreach (var id in listID)
                        {
                            Model.Add(new MultiSelectGroup { Label = (list.Where(x => x.IdDepartment == id).FirstOrDefault()?.DepartmentName ?? "ID Phòng ban: " + id.ToString()) + " | " + list.Where(x => x.IdDepartment == id).FirstOrDefault()?.CompanyName, Options = list.Where(x => x.IdDepartment == id).Select(item => new MulltiKeyValue { Id = item.Id.ToString(), Title = item.Description }).ToList() });
                        }
                    }
                }
                return PartialView("~/Views/Shared/_MultiSelectGroup.cshtml", Model);

            }
            else if (style == 7)
            {
                List<MultiSelectGroup> Model = new List<MultiSelectGroup>();

                var listID = obj?.Split(',')?.Select(Int32.Parse)?.ToList();
                var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;

                var list = _reApiServices.GetListWardsByMultiDistrict(listID).Data;

                if (!string.IsNullOrEmpty(obj) && list != null && list.Count > 0)
                {
                    if (listID != null && listID.Count > 0)
                    {
                        foreach (var id in listID)
                        {
                            Model.Add(new MultiSelectGroup { Label = list.Where(x => x.IdQuan == id).FirstOrDefault()?.TenQuan, Options = list.Where(x => x.IdQuan == id).Select(item => new MulltiKeyValue { Id = item.Id.ToString(), Title = item.Tenphuong }).ToList() });
                        }
                    }
                }
                return PartialView("~/Views/Shared/_MultiSelectGroup.cshtml", Model);

            }
            return PartialView("~/Views/Shared/_dropdownPartical.cshtml", v);
        }


        [Route("user/binduyetvitri")]
        public string binduyetvitri(string listID = "", int idCTV = 0)
        {
            string errormess = "Phê duyệt không thành công, bạn cần chọn vị trí trước khi phê duyệt";
            if (string.IsNullOrEmpty(listID))
            {
                return errormess;
            }
            else
            {
                var _listID = listID?.Split(',')?.Select(Int32.Parse)?.ToList();
                if (_listID != null && _listID.Count > 0)
                {
                    var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
                    var duyet = _companyApiServices.InsertCtvPosition(new CtvPositonInsertParam { IdCtv = idCTV, IdUser = UserId, IdPositionDepartment = _listID }).Data;

                    return duyet.ToString();
                }
                else
                {
                    return errormess;
                }
            }
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            string? msg = RemoveSessionAndCookies();
            if (msg != "Removed Successfully!")
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        public string? RemoveSessionAndCookies()
        {
            string? result = "Removed Successfully!";
            HttpContext.Session.Clear();
            if (Request.Cookies != null)
            {
                try
                {
                    foreach (var cookie in Request.Cookies.Keys)
                    {
                        Response.Cookies.Delete(cookie);
                    }
                }
                catch (Exception ex)
                {
                    string? errorMsg = ex.Message;
                    result = "An error occured.";
                    return result;
                }

            }

            return result;

        }
    }

}
