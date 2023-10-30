
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NhaDat24h.Service.Api.Users;
using NhaDat24h.CommonCode;
using NhaDat24h.Providers;
using System.IdentityModel.Tokens.Jwt;
using NhaDat24h.DataDto.Users;

namespace NhaDat24h.Attributes
{
    public class RoleAttribute : Attribute, IActionFilter
    {
        public int[] Permision { get; set; }
        public RoleAttribute(params int[] permision)
        {
            Permision = permision;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // không thể khởi tạo service thông qua contructor như bình thường 
            // phải tạo ra 1 cái là provider, đây là 1 cách để lấy service đã được khởi tạo ra dùng

            var httpContext = (IHttpContextAccessor)StaticServiceProvider.Provider.GetService(typeof(IHttpContextAccessor));
            var authorityService = (IUsersApiServices)httpContext.HttpContext.RequestServices.GetService(typeof(IUsersApiServices));
            var _SessionManag = (ISessionManager )httpContext.HttpContext.RequestServices.GetService(typeof(ISessionManager));
            var UserId = _SessionManag.GetLoginAdminFromSessionAdmin().Id;
            var isCheked = authorityService.CheckUserPermision(new CheckPermisionParam { Permission= Permision, IdUser = UserId });

            if (!isCheked.Data)
            {
                context.Result = new JsonResult("NoPermission")
                {
                    StatusCode = 405,
                    Value = new
                    {
                        Status = "Error",
                        Message = "Sorry, You don't have permission for the acction."
                    },
                };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("OnActionExecuted");
        }
    }
}
