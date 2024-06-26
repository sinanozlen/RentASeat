using DtoLayer.CarDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebUI.ViewComponents.ViewComponents.CarDetailViewComponents
{
	public class _CarDetailMainCarFeatureComponentPartial : ViewComponent
	{
		private readonly IHttpClientFactory _httpClientFactory;
		public _CarDetailMainCarFeatureComponentPartial(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}
		public async Task<IViewComponentResult> InvokeAsync(int id)
		{
			ViewBag.carid = id;
			var client = _httpClientFactory.CreateClient();
			var resposenMessage = await client.GetAsync($"https://localhost:7250/api/Cars/{id}");
			if (resposenMessage.IsSuccessStatusCode)
			{
				var jsonData = await resposenMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<ResultCarWithBrandDto>(jsonData);
				return View(values);
			}
			return View();
		}
	}
}
