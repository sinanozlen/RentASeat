using Microsoft.AspNetCore.Mvc;

namespace WebUI.ViewsComponents.RentACarFilterComponents
{
    public class _RentACarFilterComponentPartial: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
