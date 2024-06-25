using DtoLayer.FooterAddressDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/FooterAddress")]
    public class FooterAddressController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FooterAddressController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7250/api/FooterAddresses");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var footerAddresses = JsonConvert.DeserializeObject<List<ResultFooterAddressDto>>(jsonData);
                return View(footerAddresses);
            }
            return View();
        }
        [HttpGet]
        [Route("CreateFooterAddress")]
        public IActionResult CreateFooterAddress()
        {
            return View();
        }
        [HttpPost]
        [Route("CreateFooterAddress")]
        public async Task<IActionResult> CreateFooterAddress(CreateFooterAddressDto createFooterAddressDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createFooterAddressDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7250/api/FooterAddresses", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(createFooterAddressDto);
        }
        [HttpDelete]
        [Route("RemoveFooterAddress/{id}")]
        public async Task<IActionResult> RemoveFooterAddress(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7250/api/FooterAddresses/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        [Route("UpdateFooterAddress/{id}")]
        public async Task<IActionResult> UpdateFooterAddress(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7250/api/FooterAddresses/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var footerAddress = JsonConvert.DeserializeObject<UpdateFooterAddressDto>(jsonData);
                return View(footerAddress);
            }
            return View();
        }
        [HttpPost]
        [Route("UpdateFooterAddress/{id}")]
        public async Task<IActionResult> UpdateFooterAddress(UpdateFooterAddressDto updateFooterAddressDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateFooterAddressDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync($"https://localhost:7250/api/FooterAddresses/", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(updateFooterAddressDto);
        }

    }
}
