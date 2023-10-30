using Microsoft.AspNetCore.Mvc;

namespace NhaDat24hWeb.Controllers
{

    public class BlogController : Controller
    {
        [Route("blog")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("blog/blog-detail")]
        public IActionResult BlogDetail()
        {
            return View();
        }

        [Route("blog/blog-detail1")]
        public IActionResult BlogDetail1()
        {
            return View();
        }

        [Route("blog/blog-detail2")]
        public IActionResult BlogDetail2()
        {
            return View();
        }

        [Route("blog/tin-noi-bo")]
        public IActionResult InterNews()
        {
            return View();
        }

        [Route("blog/tin-thi-truong")]
        public IActionResult MarketNews()
        {
            return View();
        }

        [Route("blog/tin-du-an")]
        public IActionResult ProjectNews()
        {
            return View();
        }

    }
}
