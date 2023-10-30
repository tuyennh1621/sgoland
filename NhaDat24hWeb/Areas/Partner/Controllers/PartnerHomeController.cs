using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NhaDat24h.Common.Configuration;
using NhaDat24h.Service;
using NhaDat24h.Service.Api.Ctv;
using NhaDat24h.Service.Api.Users;
using NhaDat24hWeb.Controllers;

namespace NhaDat24hWeb.Areas.Partner.Controllers
{
    [Area("Partner")]
    public class PartnerHomeController : BaseController
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IUsersApiServices _usersApiServices;
        private readonly ICtvApiServices _ctvApiServices;
        private readonly IMyTypedClientServices _client;
        private readonly IHttpContextAccessor _contx;
        public PartnerHomeController(ILogger<DashboardController> logger, IUsersApiServices usersApiServices,
            IMyTypedClientServices client, ICtvApiServices ctvApiServices, IHttpContextAccessor contx)
        {
            _logger = logger;
            _usersApiServices = usersApiServices;
            _client = client;
            _ctvApiServices = ctvApiServices;
            _contx = contx;
        }
        public IActionResult Index()
        {
            return View();
        }

        ////[HttpGet]
        //public IActionResult Login()
        //{
        //    RemoveCurrentUserCookieAdmin();
        //    return View();
        //}

        [HttpPost]
        [Route("partnerlogin")]
        public int Login(string email, string password, string Lat, string Lon)
        {

            var user = _usersApiServices.CheckUserByAccount(email, password);
            if (user == null)
            {
                return 1;
            }
            else
            {
                var ctv = _ctvApiServices.GetCtv(user.Id);
                if (ctv.Data.Status == 1)
                {
                    _contx.HttpContext.Session.SetString(AppConfigs.CurrentUserAdmin, JsonConvert.SerializeObject(user));
                    _contx.HttpContext.Session.SetString("Position", JsonConvert.SerializeObject(ctv.Data.Position));

                    CookieOptions options = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(-1)
                    };
                    string value = string.Empty;
                    Response.Cookies.Append("SearchREParam", value, options);
                    SetCurrentUserCookieAdmin(user);

                    _ctvApiServices.UpdateCtvLonLat(new NhaDat24h.DataDto.Ctv.CtvUpdateLonLatDataDto
                    {
                        Id = user.Id,
                        Lat = double.Parse(Lat),
                        Lon = double.Parse(Lon)
                    });

                    return 2;
                }
                else if (ctv.Data.Status == 0)
                {
                    return 3;
                }
                else if (ctv.Data.Status == 3)
                {
                    return 4;
                }
                else
                {
                    return 5;
                }
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            string? msg = RemoveSessionAndCookies();
            if (msg != "Removed Successfully!")
            {
                return RedirectToAction("Index", "Login");
            }
            return RedirectToAction("Login", "AdminAccount");
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
