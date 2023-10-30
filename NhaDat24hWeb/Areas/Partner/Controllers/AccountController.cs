using Microsoft.AspNetCore.Mvc;
using NhaDat24h.Common;
using NhaDat24h.CommonCode;
using NhaDat24h.DataDto.Ctv;
using NhaDat24h.DataDto.User;
using NhaDat24h.DataDto.Users;
using NhaDat24h.Service;
using NhaDat24h.Service.Api.Company;
using NhaDat24h.Service.Api.Ctv;
using NhaDat24h.Service.Api.Users;

namespace NhaDat24hWeb.Areas.Partner.Controllers
{

    [Area("Partner")]
    public class AccountController : Controller
    {
        private IUsersApiServices _usersApiServices;
        private ICtvApiServices _ctvApiServices;
        private ISessionManager _sessionManager;
        private ICompanyApiServices _companyApiServices;
        private readonly IMyTypedClientServices _client;

        public AccountController(IUsersApiServices usersApiServices, ICtvApiServices ctvApiServices,
            ISessionManager sessionManager, ICompanyApiServices companyApiServices, IMyTypedClientServices client)
        {
            _usersApiServices = usersApiServices;
            _ctvApiServices = ctvApiServices;
            _sessionManager = sessionManager;
            _companyApiServices = companyApiServices;
            _client = client;
        }

        [Route("account")]
        public IActionResult Index()
        {
            var user = _sessionManager.GetLoginAdminFromSessionAdmin();
            var ctv = _ctvApiServices.GetCtv(user.Id);

            CtvEditDto model = new CtvEditDto()
            {
                Id = user.Id,
                IdUser = user.Id,
                Refid = ctv.Data.Refid,
                FullName = ctv.Data.FullName,
                Ngaysinh = ctv.Data.Ngaysinh,
                Socmtnd = ctv.Data.Socmtnd,
                Email = ctv.Data.Email,
                Mobile = ctv.Data.Mobile,
                GioiTinh = ctv.Data.GioiTinh,
                IdcongTy = ctv.Data.IdcongTy,
                Diachi = ctv.Data.Diachi,
                Avatar = ctv.Data.Avatar,
                Avatar2 = ctv.Data.Avatar2,
                Frontimageid = ctv.Data.Frontimageid,
                Backimageid = ctv.Data.Backimageid,
                ListCongty = _companyApiServices.GetListCompanyByCtv(user.Id).Data,
                CvUrl = ctv.Data.CvUrl,
                Gioithieu = ctv.Data.Gioithieu,
                FaceBook = ctv.Data.FaceBook,
                Twitter = ctv.Data.Twitter,
                Position = ctv.Data.Position,
            };
            return View(model);
        }
        [HttpPost]
        [Route("account/change-account")]
        public IActionResult SubmitChange(FormCTV param)
        {
            var User = _sessionManager.GetLoginAdminFromSessionAdmin();
            var Ctv = _ctvApiServices.GetCtv(User.Id);
            UploadImagesResponse temp = new UploadImagesResponse();
            var avatar = "";
            var avatar2 = "";
            var front = "";
            var back = "";
            var cv = "";
            if (param.filesAvatar1 != null)
            {
                List<IFormFile> Avt = new List<IFormFile> { param.filesAvatar1 };
                temp = _client.PostImgAndGetData(Avt, 1024, User.Id.ToString(), 11);

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
                    avatar = $"/uploads/SGOland/ctv/" + User.Id.ToString() + "/" + x;

                }
            }
            if (param.filesAvatar2 != null)
            {
                List<IFormFile> Avt2 = new List<IFormFile> { param.filesAvatar2 };
                temp = _client.PostImgAndGetData(Avt2, 1024, User.Id.ToString(), 11);

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
                    avatar2 = $"/uploads/SGOland/ctv/" + User.Id.ToString() + "/" + x;

                }
            }
            if (param.filesCMT1 != null)
            {
                List<IFormFile> FrontImg = new List<IFormFile> { param.filesCMT1 };
                temp = _client.PostImgAndGetData(FrontImg, 1024, User.Id.ToString(), 11);

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
                    front = $"/uploads/SGOland/ctv/" + User.Id.ToString() + "/" + x;

                }
            }
            if (param.filesCMT2 != null)
            {
                List<IFormFile> BackImg = new List<IFormFile> { param.filesCMT2 };
                temp = _client.PostImgAndGetData(BackImg, 1024, User.Id.ToString(), 11);

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
                    back = $"/uploads/SGOland/ctv/" + User.Id.ToString() + "/" + x;

                }
            }
            if (param.filesCV != null)
            {
                List<IFormFile> CvUrl = new List<IFormFile> { param.filesCV };
                var cvupload = _client.PostCvAndGetData(CvUrl, User.Id.ToString());

                string[] charsToRemove = new string[] { "@", "[", "]", "'" };
                foreach (var c in charsToRemove)
                {
                    cvupload.data = cvupload.data.Replace(c, string.Empty);
                    cvupload.size = cvupload.size.Replace(c, string.Empty);
                }
                var y = Path.GetFileName(cvupload.data).Replace("\"", "");
                cv = $"/uploads/SGOland/ctv/" + User.Id.ToString() + "/" + y;
            }
            var user = new UserInsertDataDto()
            {
                Id = User.Id,
                IdU = User.IdU,
                Fullname = param.Fullname,
                Password = User.Password,
                Email = User.Email,
                Mobile = User.Mobile,
                GioiTinh = param.GioiTinh,
                Address = param.Address,
                Phone = User.Phone,
                IdTt = User.IdTt,
                Kh = User.Kh,
                Weburl = User.Weburl,
                Doituong = User.Doituong,
                NickYahoo = User.NickYahoo,
                Ip = User.Ip,
                NickSkype = User.NickSkype,
                Avatar = User.Avatar,
                Dateupdateavatar = User.Dateupdateavatar,
            };

            var ctv = new CtvEditDto()
            {
                Id = User.Id,
                IdUser = User.Id,
                FullName = param.Fullname,
                Socmtnd = Ctv.Data.Socmtnd,
                Ngaysinh = param.NgaySinh,
                GioiTinh = param.GioiTinh,
                Diachi = param.Address,
                Mobile = Ctv.Data.Mobile,
                Email = User.Email,
                IdcongTy = Ctv.Data.IdcongTy,
                Refid = Ctv.Data.Refid,
                Avatar = param.Avatar1,
                Avatar2 = param.Avatar2,
                Frontimageid = param.FrontIDImg,
                Backimageid = param.BackIDImg,
                CvUrl = Ctv.Data.CvUrl,
                DepartmentId = Ctv.Data.DepartmentId,
                Status = Ctv.Data.Status,
                Position = Ctv.Data.Position,
            };
            if (avatar != null && avatar != "")
            {
                ctv.Avatar = avatar;
                param.Avatar1 = avatar;
            }
            if (avatar2 != null && avatar2 != "")
            {
                ctv.Avatar2 = avatar2;
                param.Avatar2 = avatar2;
            }
            if (front != null && front != "")
            {
                ctv.Frontimageid = front;
                param.FrontIDImg = front;
            }
            if (back != null && back != "")
            {
                ctv.Backimageid = back;
                param.BackIDImg = back;
            }
            if (cv != null && cv != "")
            {
                ctv.CvUrl = cv;
            }
            var updateuser = _usersApiServices.UpdateUser(user);
            var updatectv = _ctvApiServices.UpdateCtv(ctv);

            return PartialView("~/Areas/Partner/Views/Account/Index.cshtml", param);
        }
        [Route("account/changePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPut("change-password")]
        [Route("account/change-password")]
        public IActionResult UpdatePasswordUser(string oldpassword, string newpassword)
        {

            var User = _sessionManager.GetLoginAdminFromSessionAdmin();
            var pw = new UpdatePasswordUserParam()
            {
                IdUser = User.Id,
                OldPass = oldpassword,
                NewPass = newpassword
            };

            ResponseBase<string> changepassword = _usersApiServices.UpdatePasswordUser(pw);
            return PartialView("~/Areas/Partner/Views/Account/ChangePassword.cshtml", changepassword.Code.ToString());
        }








    }
}
