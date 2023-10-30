using AutoMapper.Execution;
using Microsoft.AspNetCore.Mvc;
using NhaDat24h.CommonCode;
using NhaDat24h.DataDto.Users;
using NhaDat24h.Service.Api.Common;
using NhaDat24h.Service.Api.Company;
using NhaDat24h.Service.Api.Ctv;
using NhaDat24h.Service.Api.RealEstates;
using static Google.Api.ResourceDescriptor.Types;

namespace NhaDat24hWeb.Areas.Partner.Controllers
{
    [Area("Partner")]
    public class DashboardController : Controller
    {
        private ICtvApiServices _ctvApiServices;
        private IRealEstateApiServices _reApiServices;
        private ISessionManager _sessionManager;
        private ICompanyApiServices _companyServices;
        private ICommonApiServices _commonServices;

        public DashboardController(ICtvApiServices ctvApiServices, IRealEstateApiServices reApiServices, ISessionManager sessionManager,
            ICompanyApiServices companyServices, ICommonApiServices commonServices)
        {
            _ctvApiServices = ctvApiServices;
            _reApiServices = reApiServices;
            _sessionManager = sessionManager;
            _companyServices = companyServices;
            _commonServices = commonServices;
        }

        [Route("dashboard")]
        public IActionResult Index()
        {
            var User = _sessionManager.GetLoginAdminFromSessionAdmin();
            var model = _commonServices.GetDashBoard(User.Id).Data;
            if (model.ListSaveRE!=null)
            {
                model.ListSaveRE.ForEach(x => x.isYeuthich = true);
            }
            model.ListTopHN = _reApiServices.GetTopREinProvince(5, 1, 6, 1).Data;
            model.ListTopQN = _reApiServices.GetTopREinProvince(23, 1, 6,1).Data;
            model.ListTopPY = _reApiServices.GetTopREinProvince(42, 1, 6,1).Data;
            model.ListTopHP = _reApiServices.GetTopREinProvince(37, 1, 6,1).Data;
            model.ListTopBD = _reApiServices.GetTopREinProvince(30, 1, 6,1).Data;
            return View(model);
        }

        [Route("welcome")]
        public IActionResult Welcome()
        {
            return View();
        }

        [Route("viewiframe")]
        public IActionResult viewIframe(string a)
        {
            return PartialView("~/Areas/Partner/Views/Dashboard/viewIframe.cshtml", a);
        }

        [Route("viewimage")]
        public IActionResult viewImage(string a)
        {
            return PartialView("~/Areas/Partner/Views/Dashboard/viewImage.cshtml", a);
        }

        [Route("show-chat")]
        public IActionResult showChat(string idtopic)
        {
            return PartialView("~/Areas/Partner/Views/Dashboard/showchat.cshtml", idtopic);
        }
    }
}
