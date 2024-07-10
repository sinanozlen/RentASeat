using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Layout")]
    public class LayoutController : Controller
    {
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [Route("Index")]
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden");
            }
            return View();
        }

        public PartialViewResult AdminHeaderPartial()
        {
            return PartialView("/Views/Layout/AdminHeaderPartial.cshtml");
        }

        public PartialViewResult AdminNavBarPartial()
        {
            return PartialView("/Views/Layout/AdminNavBarPartial.cshtml");
        }

        public PartialViewResult AdminSideBarPartial()
        {
            return PartialView("/Views/Layout/AdminSideBarPartial.cshtml");
        }

        public PartialViewResult AdminFooterPartial()
        {
            return PartialView("/Views/Layout/AdminFooterPartial.cshtml");
        }

        public PartialViewResult AdminScriptPartial()
        {
            return PartialView("/Views/Layout/AdminScriptPartial.cshtml");
        }
    }
}
