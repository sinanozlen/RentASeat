using Microsoft.AspNetCore.Mvc;

namespace WebUI.ViewsComponents.AboutViewComponents
{
	public class _BecomeADriverComponentPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
