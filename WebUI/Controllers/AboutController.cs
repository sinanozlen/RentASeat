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
			return View();
		}

	}
}

