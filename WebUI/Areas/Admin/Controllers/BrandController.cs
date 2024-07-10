using DtoLayer.BrandDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Brand")]
    public class BrandController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BrandController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://api.rentaseat.com.tr/api/Brands");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBrandDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("CreateBrand")]
        public IActionResult CreateBrand()
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
        [Route("CreateBrand")]
        public async Task<IActionResult> CreateBrand(CreateBrandDto createBrandDto)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createBrandDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://api.rentaseat.com.tr/api/Brands", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("RemoveBrand/{id}")]
        public async Task<IActionResult> RemoveBrand(int id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://api.rentaseat.com.tr/api/Brands/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("UpdateBrand/{id}")]
        public async Task<IActionResult> UpdateBrand(int id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var client = _httpClientFactory.CreateClient();
            var resposenMessage = await client.GetAsync($"https://api.rentaseat.com.tr/api/Brands/{id}");
            if (resposenMessage.IsSuccessStatusCode)
            {
                var jsonData = await resposenMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateBrandDto>(jsonData);
                return View(values);
            }
            return View();
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("UpdateBrand/{id}")]
        public async Task<IActionResult> UpdateBrand(UpdateBrandDto updateBrandDto)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateBrandDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://api.rentaseat.com.tr/api/Brands/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
