using DtoLayer.StatisticsDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Statistic")]
    public class StatisticController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StatisticController(IHttpClientFactory httpClientFactory)
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
            var responseMessage = await client.GetAsync("https://api.rentaseat.com.tr/api/Statistics/GetAllStatistics");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var statistics = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);

                ViewBag.CarCount = statistics.CarCount;
                ViewBag.BrandCount = statistics.BrandCount;
                ViewBag.BrandNameByMaxCar = statistics.BrandNameByMaxCar;
                ViewBag.CarBrandAndModelByRentPriceDailyMax = statistics.CarBrandAndModelByRentPriceDailyMax;
                ViewBag.CarBrandAndModelByRentPriceDailyMin = statistics.CarBrandAndModelByRentPriceDailyMin;
                ViewBag.AvgRentPriceForDaily = statistics.AvgRentPriceForDaily.ToString("0.00");
                ViewBag.AvgRentPriceForWeekly = statistics.AvgRentPriceForWeekly.ToString("0.00");
                ViewBag.AvgRentPriceForMonthly = statistics.AvgRentPriceForMonthly.ToString("0.00");
                ViewBag.CarCountByFuelElectric = statistics.CarCountByFuelElectric;
                ViewBag.CarCountByFuelGasolineOrDiesel = statistics.CarCountByFuelGasolineOrDiesel;
                ViewBag.CarCountByKmSmallerThen1000 = statistics.CarCountByKmSmallerThen1000;
                ViewBag.LocationCount = statistics.LocationCount;
                // Random values for illustration purposes
                Random random = new Random();
                ViewBag.v1 = random.Next(0, 101);
                ViewBag.locationCountRandom = random.Next(0, 101);
                ViewBag.brandCountRandom = random.Next(0, 101);
                ViewBag.avgRentPriceForDailyRandom = random.Next(0, 101);
                ViewBag.avgRentPriceForWeeklyRandom = random.Next(0, 101);
                ViewBag.avgRentPriceForMonthlyRandom = random.Next(0, 101);
                ViewBag.brandNameByMaxCarRandom = random.Next(0, 101);
                ViewBag.carCountByKmSmallerThen1000Random = random.Next(0, 101);
                ViewBag.carCountByFuelGasolineOrDieselRandom = random.Next(0, 101);
                ViewBag.carCountByFuelElectricRandom = random.Next(0, 101);
                ViewBag.carBrandAndModelByRentPriceDailyMaxRandom = random.Next(0, 101);
                ViewBag.carBrandAndModelByRentPriceDailyMinRandom = random.Next(0, 101);
            }

            return View();
        }
    }
}
