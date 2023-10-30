using Google.Api;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using NhaDat24h.Api.Configurations;
using NhaDat24h.Common.Configuration;
using NhaDat24h.CommonCode;
using NhaDat24h.DataAccess.DBContext;
using NhaDat24h.DataAccess.Interface;
using NhaDat24h.DataAccess.Repositories;
using NhaDat24h.DataAccess.UnitOfWork;
using NhaDat24h.DataDto.Users;
using NhaDat24h.MiddleWare;
using NhaDat24h.Providers;
using NhaDat24h.Service;
using NhaDat24h.Service.Api.Ctv;
using Q101.ServiceCollectionExtensions.ServiceCollectionExtensions;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();
// Add services to the container.
builder.Services.AddControllersWithViews();
AppConfigs.LoadAll(config);
builder.Services.AddHttpContextAccessor();
//--register CommonDBContext
//builder.Services
//    .AddControllersWithViews()
//    // Maintain property names during serialization. See:
//    // https://github.com/aspnet/Announcements/issues/194
//    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());
builder.Services.AddDbContext<CommonDBContext>(options =>
            options.UseSqlServer(AppConfigs.SqlConnection, options => { }),
            ServiceLifetime.Scoped
            );
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
    .AddCookie()
    .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
    {
        options.ClientId = AppConfigs.GoogleClientId;
        options.ClientSecret = AppConfigs.GoogleClientSecret;
        options.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
    })
    .AddFacebook(FacebookDefaults.AuthenticationScheme, options =>
     {
         options.ClientId = AppConfigs.FacebookClientId;
         options.ClientSecret = AppConfigs.FacebookClientSecret;
     });
    //.AddJwtBearer(options => {
    //    options.TokenValidationParameters = new TokenValidationParameters
    //    {
    //        ValidateIssuer = true,
    //        ValidateAudience = false,
    //        ValidateLifetime = true,
    //        ValidateIssuerSigningKey = true,
    //        ValidIssuer = builder.Configuration["Jwt:Issuer"],
    //        ValidAudience = builder.Configuration["Jwt:Audience"],
    //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    //    };
    //});
builder.Services.AddTransient(typeof(ICommonRepository<>), typeof(CommonRepository<>));
builder.Services.AddTransient(typeof(ICommonUoW), typeof(CommonUoW));
builder.Services.AddKendo();
builder.Services.AddSingleton<ISessionManager, SessionManager>();
//--register Service
builder.Services.RegisterAssemblyTypesByName(typeof(ICtvApiServices).Assembly,
     name => name.EndsWith("Services")) // Condition for name of type
.AsScoped()
.AsImplementedInterfaces()
     .Bind();
builder.Services.AddCommonServices();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.Configure<AppSettings>(config.GetSection("AppSettings"));

//builder.Services.AddScoped<AdminAuthorization>();
// Add services to the container.
builder.Services.AddHttpClient<IMyTypedClientServices, MyTypedClientServices>();
builder.Services.AddTransient(typeof(ICommonRepository<>), typeof(CommonRepository<>));
builder.Services.AddTransient(typeof(ICommonUoW), typeof(CommonUoW));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(24);
});
builder.Environment.IsDevelopment();
var app = builder.Build();

//using (StreamReader iisUrlRewriteStreamReader =
//    File.OpenText("IISUrlRewrite.xml"))
//{
//    var options = new RewriteOptions()
//        .AddRedirect("redirect-rule/(.*)", "redirected/$1")
//        .AddRewrite(@"^rewrite-rule/(\d+)/(\d+)", "rewritten?var1=$1&var2=$2", skipRemainingRules: true)
//        .AddIISUrlRewrite(iisUrlRewriteStreamReader)
//        .Add(MethodRules.RedirectXmlFileRequests)
//        .Add(MethodRules.RewriteTextFileRequests);
//    //.Add(new RedirectImageRequests(".png", "/png-images"))
//    //.Add(new RedirectImageRequests(".jpg", "/jpg-images"));

//    app.UseRewriter(options);
//}

    StaticServiceProvider.Provider = app.Services;
    //UpdateTimer.Init();

//app.UseMiddleware<JwtMiddleware>();
app.UseSession();
app.Use(async (context, next) =>
{
    if (context.Request.Query.Count() > 0 &&
    context.Request.Query["culture"].ToString() != "")
    {

        System.Threading.Thread.CurrentThread.CurrentCulture =
        System.Threading.Thread.CurrentThread.CurrentUICulture =
        new CultureInfo(context.Request.Query["culture"].ToString());
        // Save current culture cookie
        context.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue
                (new RequestCulture(context.Request.Query["culture"].ToString())),
                new CookieOptions() { Expires = DateTime.Now.AddYears(1) }
            );
    }
    await next.Invoke();
});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapAreaControllerRoute(
//    name: "Dashboard",
//    areaName: "Partner",
//    pattern: "Partner/{controller=Dashboard}/{action=Index}/{id?}");
app.Run();
