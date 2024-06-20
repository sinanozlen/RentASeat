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
		private readonly IHttpClientFactory _httpClientFactory;

		public AboutController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Index()
		{
			var client =  _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7250/api/Abouts");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var services = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);
				return View(services);
			}
			return View();
			
		}
		public PartialViewResult AboutUsPartial()
        {
            return PartialView();
        }
		public PartialViewResult BecomeADriverPartial()
        {
            return PartialView();
        }
		public PartialViewResult TestimonialPartial()
        {
            return PartialView();
        }
		public async Task< PartialViewResult> FooterAddressPartial()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7250/api/FooterAddresses");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFooterAddressDto>>(jsonData);
                return PartialView(values);
            }
            return PartialView();
        }
	}
}

