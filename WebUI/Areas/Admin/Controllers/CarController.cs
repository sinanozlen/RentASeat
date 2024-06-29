using DtoLayer.BrandDtos;
using DtoLayer.CarDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Car")]
    public class CarController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CarController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task< IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7250/api/Cars/GetCarsWithBrand");
            if(responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var cars = JsonConvert.DeserializeObject<List<ResultCarWithBrandDto>>(jsonData);
                return View(cars);
            }
            return View();
        }
        
        [HttpGet]
        [Route("CreateCar")]
        public async Task<IActionResult> CreateCar()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7250/api/Brands");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultBrandDto>>(jsonData);
            List<SelectListItem> brandValues = (from x in values
                                                select new SelectListItem
                                                {
                                                    Text = x.name,
                                                    Value = x.brandID.ToString()
                                                }).ToList();
            ViewBag.BrandValues = brandValues;
            return View();
        }
        [HttpPost]
        [Route("CreateCar")]
        public async Task<IActionResult> CreateCar(CreateCarDto createCarDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCarDto);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7250/api/Cars/", content);
            if(responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpDelete]
        [Route("RemoveCar/{id}")]
        public async Task<IActionResult> RemoveCar(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7250/api/Cars/{id}");
            if(responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        [Route("UpdateCar/{id}")]
        public async Task<IActionResult> UpdateCar(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var responseMessage1 = await client.GetAsync("https://localhost:7250/api/Brands");
            var jsonData1 = await responseMessage1.Content.ReadAsStringAsync();
            var values1 = JsonConvert.DeserializeObject<List<ResultBrandDto>>(jsonData1);
            List<SelectListItem> brandValues = (from x in values1
                                                select new SelectListItem
                                                {
                                                    Text = x.name,
                                                    Value = x.brandID.ToString()
                                                }).ToList();
            ViewBag.BrandValues = brandValues;

            var responseMessage = await client.GetAsync($"https://localhost:7250/api/Cars/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateCarDto>(jsonData);
                return View(values);
            }
            return View();
        }
        //[HttpPost]
        //[Route("UpdateCar/{id}")]
        //public async Task<IActionResult> UpdateCar(UpdateCarDto updateCarDto)
        //{
        //    var client = _httpClientFactory.CreateClient();
        //    var jsonData = JsonConvert.SerializeObject(updateCarDto);
        //    var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
        //    var responseMessage = await client.PutAsync($"https://localhost:7250/api/Cars/", content);
        //    if(responseMessage.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}



    }
}
