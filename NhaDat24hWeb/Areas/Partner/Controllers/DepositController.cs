using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using NhaDat24h.CommonCode;
using NhaDat24h.DataDto.RealEstates;
using NhaDat24h.Service;
using NhaDat24h.Service.Api.Company;
using NhaDat24h.Service.Api.Ctv;
using NhaDat24h.Service.Api.RealEstates;
using NhaDat24h.Service.Api.Users;

namespace NhaDat24hWeb.Areas.Partner.Controllers
{
    [Area("Partner")]
    public class DepositController : Controller
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IUsersApiServices _usersApiServices;
        private ICtvApiServices _ctvApiServices;
        private ICompanyApiServices _companyApiServices;
        private IRealEstateApiServices _reApiServices;
        private readonly IMyTypedClientServices _upload;
        private ISessionManager _sessionManager;
        private FirestoreDb _firestore;
        private string projectId;
        public DepositController(IHttpContextAccessor httpContextAccessor, IUsersApiServices usersApiServices, IRealEstateApiServices reApiServices,
            ISessionManager sessionManager, ICtvApiServices ctvApiServices, ICompanyApiServices companyApiServices, IMyTypedClientServices client)
        {
            _httpContextAccessor = httpContextAccessor;
            _usersApiServices = usersApiServices;
            _sessionManager = sessionManager;
            _ctvApiServices = ctvApiServices;
            _companyApiServices = companyApiServices;
            _reApiServices = reApiServices;
            _upload = client;
            projectId = "gostay-1ae07";
            _firestore = FirestoreDb.Create(projectId);
        }
        [Route("deposit")]
        public IActionResult Index()
        {
            var re = _reApiServices.GetListREForDeposit();
            var deposit = _reApiServices.SearchListDepositRE(null, null, null, null, null, 1, 5);

            var model = new DepositIndexModel()
            {
                ListDeposit = deposit.Data,
                ListRE = re.Data,
            };
            return View(model);
        }
        [Route("deposit/search")]
        public IActionResult SearchDeposit(string? Name, byte? IdType, byte? Status,
                                            DateTime? StartDate, DateTime? EndDate, int PageIndex, int PageSize)
        {
            var deposit = _reApiServices.SearchListDepositRE(Name, IdType, Status, StartDate?.ToString("yyyy-MM-dd"), EndDate?.ToString("yyyy-MM-dd"), PageIndex, PageSize);
            ModelDeposit model = new ModelDeposit()
            {
                ListDeposit = deposit.Data,
                PageSize = PageSize,
                PageIndex = PageIndex,
            };
            return PartialView("~/Areas/Partner/Views/Deposit/ListDepositPartial.cshtml", model);
        }
        [Route("deposit/insert-defaul")]
        public IActionResult InsertDefaulDeposit()
        {
            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;

            var deposit = _reApiServices.InsertDefaulDepositRE(UserId);
            var model = new AddDepositForm()
            {
                IdDeposit = deposit.Data,
                ListRE = _reApiServices.GetListREForDeposit().Data,
                DepositDate = DateTime.Now,
                PaymentDeadline = DateTime.Now.AddDays(1),
            };
            return PartialView("~/Areas/Partner/Views/Deposit/AddDepositPartial.cshtml", model);
        }

        [Route("deposit/submit")]
        public IActionResult SubmitDeposit(AddDepositForm param)
        {
            param.DepositValue = param.DepositValue / 1000000;
            param.TotalValue = param.TotalValue / 1000000;
            string[] charsToRemove = new string[] { "@", "[", "]", "'" };
            string strMess = "";
            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var imgs = "";
            if (param.Contract != null)
            {
                var upload = _upload.PostFileGeneralEndGetData(param.Contract
                                        , $"/upload/SGOland/ctv/{UserId}/deposit/{param.IdDeposit}/");
                foreach (var c in charsToRemove)
                {
                    upload.data = upload.data.Replace(c, string.Empty);
                    upload.size = upload.size.Replace(c, string.Empty);
                }

                var url = upload.data.Split(",");
                string[] urlimg = new string[url.Length];
                for (int i = 0; i < url.Length; i++)
                {

                    var x = Path.GetFileNameWithoutExtension(url[i]) + Path.GetExtension(url[i]).Replace("\"", "");
                    urlimg[i] = $"/upload/SGOland/ctv/{UserId}/deposit/{param.IdDeposit}/" + x;

                }
                imgs = string.Join(";", urlimg);
            }
            var inputUp = new DepositREUpdateParam()
            {
                IdDeposit = param.IdDeposit,
                Name = param.Name,
                IdRE = param.IdRE,
                DepositDate = param.DepositDate,
                PaymentDeadline = param.PaymentDeadline,
                DepositValue = param.DepositValue,
                TotalValue = param.TotalValue,
                Detail = param.Detail,
                IdUserRequest = UserId,
                Status = param.Status,
                Bonus = param.Bonus,
            };
            if (imgs != null && imgs != "")
            {
                inputUp.ContractImg = imgs;
            }
            else
            {
                inputUp.ContractImg = param.ContractImg;
            }
            var deposit = _reApiServices.UpdateDepositRE(inputUp);
            strMess = deposit.Message;

            return Json(strMess);
        }

        [Route("deposit/get-deposit")]
        public IActionResult GetDeposit(int IdDeposit)
        {
            var UserID = _sessionManager.GetLoginAdminFromSessionAdmin();
            var dps = _reApiServices.GetDeposit(IdDeposit).Data;
            dps.DepositValue = dps.DepositValue * 1000000;
            dps.TotalValue = dps.TotalValue * 1000000;
            var model = new AddDepositForm()
            {
                IdUser = UserID.Id,
                ListRE = _reApiServices.GetListREForDeposit().Data,
                IdDeposit = dps.IdDeposit,
                Name = dps.Name,
                IdRE = dps.IdRE,
                DepositDate = dps.DepositDate,
                DepositValue = dps.DepositValue,
                PaymentDeadline = dps.PaymentDeadline,
                Status = dps.Status,
                TotalValue = dps.TotalValue,
                Detail = dps.Detail,
                IdTypeRE = dps.IdTypeRE,
                ContractImg = dps.ContractImg,
                Bonus = dps.Bonus,

            };

            return PartialView("~/Areas/Partner/Views/Deposit/AddDepositPartial.cshtml", model);
        }

        [Route("deposit/update-Status-Deponsit")]
        [HttpGet]
        public IActionResult UpdateStatusDepositRE(int Id, byte Status)
        {
            var UserID = _sessionManager.GetLoginAdminFromSessionAdmin();
            var result = _reApiServices.UpdateStatusDeposit(new UpdateStatusDeponsitParam()
            {
                Id = Id,
                Status = Status,
                IdUserRequest = UserID.Id
            });


            return Json("1");
        }

        [Route("delete-deposit")]
        [HttpGet]
        public IActionResult DeleteDepositRE(int IdDeposit)
        {
            var User = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var result = _reApiServices.DeleteRE(User, IdDeposit);

            return Json(result.Message);
        }

    }
}
