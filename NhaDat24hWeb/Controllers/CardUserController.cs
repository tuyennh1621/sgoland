using Microsoft.AspNetCore.Mvc;
using NhaDat24h.Service.Api.Ctv;

namespace NhaDat24hWeb.Controllers
{
    public class CardUserController : Controller
    {

        private ICtvApiServices _ctvApiServices;
        public CardUserController(ICtvApiServices ctvApiServices)
        {
            _ctvApiServices = ctvApiServices;
        }
        public IActionResult Index(int IdCtv)
        {
            var ctv = _ctvApiServices.GetCtv(IdCtv);
            return View(ctv.Data);
        }
    }
}
