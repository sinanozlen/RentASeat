using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class ForbiddenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
