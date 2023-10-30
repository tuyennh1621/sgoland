using Microsoft.AspNetCore.Mvc;

namespace NhaDat24hWeb.Controllers
{
    public class ProjectController : Controller
    {
        [Route("project")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("project/project-detail")]
        public IActionResult ProjectDetail()
        {
            return View();
        }
    }
}
