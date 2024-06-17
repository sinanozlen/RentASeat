using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Layout")]
    public class LayoutController : Controller
    {
        [Route("Index")]
        public IActionResult Index()
        {
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
