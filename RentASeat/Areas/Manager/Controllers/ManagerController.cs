using Microsoft.AspNetCore.Mvc;

namespace RentASeat.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
