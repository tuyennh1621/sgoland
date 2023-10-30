using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NhaDat24h.Common.Configuration;
using NhaDat24h.DataAccess.Entities;
using System.Text;

namespace NhaDat24hWeb.Controllers
{
    public class BaseController : Controller
    {
        private readonly string _keyCurrentUser = AppConfigs.CurrentUserCK;
        private string secret = AppConfigs.SecretKey;
        protected void SetCurrentUserCookieAdmin(TbUser userData)
        {
            userData.Password = "";
            var result = EncryptPassword(userData.IdU);
            //userData.UserId = 2;
            userData.UserVerify = result;
            var userDataJson = JsonConvert.SerializeObject(userData);
            CookieOptions options = new CookieOptions
            {
                Expires = new DateTimeOffset(2038, 1, 1, 0, 0, 0, TimeSpan.FromHours(3))
            };

            Response.Cookies.Append(AppConfigs.CurrentUserAdmin, userDataJson, options);

        }
        protected void SetCurrentUserCookie(TbUser userData)
        {
            userData.Password = "";
            var result = EncryptPassword(userData.IdU);
            //userData.UserId = 2;
            userData.UserVerify = result;
            var userDataJson = JsonConvert.SerializeObject(userData);
            CookieOptions options = new CookieOptions
            {
                Expires = new DateTimeOffset(2038, 1, 1, 0, 0, 0, TimeSpan.FromHours(24))
            };



            Response.Cookies.Append(_keyCurrentUser, userDataJson, options);

        }
        protected TbUser GetCurrentUser()
        {
            TbUser user = null;
            try
            {
                var cookieValue = Request.Cookies[_keyCurrentUser];
                if (!string.IsNullOrEmpty(cookieValue))
                {
                    user = JsonConvert.DeserializeObject<TbUser>(cookieValue);

                }
            }
            catch
            {
                throw;
            }
            return user;
        }
        protected void RemoveCurrentUserCookie()
        {
            string value = string.Empty;
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            Response.Cookies.Append(_keyCurrentUser, value, options);
        }
        protected void RemoveCurrentUserCookieAdmin()
        {
            string value = string.Empty;
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            Response.Cookies.Append(AppConfigs.CurrentUserAdmin, value, options);
        }
        protected IActionResult HomePage()
        {
            return RedirectToAction("Index", "Home");
        }
        protected IActionResult Dashboard()
        {
            return RedirectToAction("Home", "Admin"); ;
        }
        public string EncryptPassword(string strId)
        {
            if (string.IsNullOrEmpty(strId))
            {
                return "";
            }
            else
            {
                try
                {
                    strId = secret + "_" + strId;
                    byte[] data = ASCIIEncoding.ASCII.GetBytes(strId);
                    string uId = Convert.ToBase64String(data);
                    return uId;
                }
                catch (Exception)
                {

                    return "";
                }

            }

        }
    }
}
