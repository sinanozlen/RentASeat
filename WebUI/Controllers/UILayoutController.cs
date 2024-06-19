using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class UILayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult HeaderPartial()
        {
            return PartialView();
        }
        public PartialViewResult NavBarPartial() {
            return PartialView();
        }
        public PartialViewResult MainCoverPartial() {
            return PartialView();
        }
        public PartialViewResult FooterPartial() {
            return PartialView();
        }
        public PartialViewResult ScriptPartial() {
            return PartialView();
        }
       

    }
}
