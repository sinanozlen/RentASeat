using DtoLayer.LocationDtos;
using DtoLayer.PricingDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Pricing")]

    public class PricingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PricingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task< IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage= await client.GetAsync("https://localhost:7250/api/Pricings");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var pricings = JsonConvert.DeserializeObject<List<ResultPricingDto>>(jsonData);
                return View(pricings);
            }
            return View();
        }
        [HttpGet]
        [Route("CreatePricing")]
        public IActionResult CreatePricing()
        {
            return View();
        }
        [HttpPost]
        [Route("CreatePricing")]
        public async Task<IActionResult> CreatePricing(CreatePricingDto createPricingDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createPricingDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7250/api/Pricings", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpDelete]
        [Route("RemovePricing/{id}")]
        public async Task<IActionResult> RemovePricing(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7250/api/Pricings/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        [Route("UpdatePricing/{id}")]
        public async Task<IActionResult> UpdatePricing(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7250/api/Pricings/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var location = JsonConvert.DeserializeObject<UpdatePricingDto>(jsonData);
                return View(location);
            }
            return View();
        }
        [HttpPost]
        [Route("UpdatePricing/{id}")]
        public async Task<IActionResult> UpdatePricing(UpdatePricingDto updatePricingDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updatePricingDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync($"https://localhost:7250/api/Pricings/", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
