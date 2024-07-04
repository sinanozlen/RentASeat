using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.ViewComponents.UILayoutViewComponents
{
    public class _NavbarUILayoutComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var isAuthenticated = HttpContext.User.Identity.IsAuthenticated;
            var name = isAuthenticated ? HttpContext.User.Identity.Name : string.Empty;

            var model = new NavbarViewModel
            {
                IsAuthenticated = isAuthenticated,
                Name = name
            };
            return View(model);
        }
    }
}
