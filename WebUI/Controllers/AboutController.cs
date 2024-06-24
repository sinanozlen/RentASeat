using DtoLayer.AboutDtos;
using DtoLayer.FooterAddressDtos;
using DtoLayer.ServiceDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace WebUI.Controllers
{
	public class AboutController : Controller
	{
		public IActionResult Index()
		{
			ViewBag.v1 = "Hakkımızda";
            ViewBag.v2 = "Vizyonumuz & Misyonumuz";
            return View();
		}

	}
}

