using Microsoft.AspNetCore.Mvc;

namespace Traversal.Controllers
{
    [Route("About/Index")]
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
