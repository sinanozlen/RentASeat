using DtoLayer.LocationDtos;
using DtoLayer.PricingDtos;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [Route("Index")]
        public async Task< IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var client = _httpClientFactory.CreateClient();
            var responseMessage= await client.GetAsync("https://api.rentaseat.com.tr/api/Pricings");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var pricings = JsonConvert.DeserializeObject<List<ResultPricingDto>>(jsonData);
                return View(pricings);
            }
            return View();
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("CreatePricing")]
        public IActionResult CreatePricing()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden");
            }
            return View();
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("CreatePricing")]
        public async Task<IActionResult> CreatePricing(CreatePricingDto createPricingDto)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createPricingDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://api.rentaseat.com.tr/api/Pricings", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("RemovePricing/{id}")]
        public async Task<IActionResult> RemovePricing(int id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://api.rentaseat.com.tr/api/Pricings/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("UpdatePricing/{id}")]
        public async Task<IActionResult> UpdatePricing(int id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://api.rentaseat.com.tr/api/Pricings/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var location = JsonConvert.DeserializeObject<UpdatePricingDto>(jsonData);
                return View(location);
            }
            return View();
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("UpdatePricing/{id}")]
        public async Task<IActionResult> UpdatePricing(UpdatePricingDto updatePricingDto)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updatePricingDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync($"https://api.rentaseat.com.tr/api/Pricings/", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
