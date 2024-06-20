using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DtoLayer.CarPricingDtos;
using DtoLayer.ServiceDtos; // Adjust namespaces based on your DTOs
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebUI.Controllers
{
    public class PricingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PricingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7250/api/Pricing"); // Adjust endpoint URL as needed
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var pricings = JsonConvert.DeserializeObject<List<ResultCarPricingWithCarDto>>(jsonData);
                return View(pricings);
            }
            // Handle unsuccessful API call here
            return View();
        }
    }
}
