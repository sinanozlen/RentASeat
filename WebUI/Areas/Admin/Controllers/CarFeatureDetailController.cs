using DtoLayer.CarFeatureDtos;
using DtoLayer.FeatureDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/CarFeatureDetail")]
    public class CarFeatureDetailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CarFeatureDetailController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index/{id}")]
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7250/api/CarFeatures?carId=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCarFeatureByCarIdDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        [Route("Index/{id}")]
        public async Task<IActionResult> Index(int id, List<ResultCarFeatureByCarIdDto> resultCarFeatureByCarIdDto)
        {
            var client = _httpClientFactory.CreateClient();

            foreach (var item in resultCarFeatureByCarIdDto)
            {
                if (item.Available)
                {
                    await client.GetAsync("https://localhost:7250/api/CarFeatures/CarFeatureChangeAvailableToTrue?id=" + item.CarFeatureID);
                }
                else
                {
                    await client.GetAsync("https://localhost:7250/api/CarFeatures/CarFeatureChangeAvailableToFalse?id=" + item.CarFeatureID);
                }
            }
            return RedirectToAction("Index", "Car");
        }

        [Route("CreateFeatureByCarId/{id}")]
        [HttpGet]
        public async Task<IActionResult> CreateFeatureByCarId(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7250/api/Features");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(jsonData);
                ViewBag.CarId = id;
                return View(values);
            }
            return View();
        }

        [Route("CreateFeatureByCarId/{id}")]
        [HttpPost]
        public async Task<IActionResult> CreateFeatureByCarId(int id, List<CreateCarFeatureDto> createCarFeatureDtos)
        {
            var client = _httpClientFactory.CreateClient();

            foreach (var feature in createCarFeatureDtos)
            {
                feature.CarID = id;
                feature.Available = true;  // Tüm özellikler işaretlenmiş olarak gönderilecektir

                var jsonContent = JsonConvert.SerializeObject(feature);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                await client.PostAsync("https://localhost:7250/api/CarFeatures", content);
            }

            return RedirectToAction("Index", "Car");
        }
    }
}
