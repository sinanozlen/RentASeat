using DtoLayer.StatisticsDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace UdemyCarBook.WebUI.ViewComponents.DashboardComponents
{
    public class _AdminDashboardStatisticsComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _AdminDashboardStatisticsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Random random = new Random();
            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync("https://api.rentaseat.com.tr/api/Statistics/GetAllStatistics");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);

                ViewBag.CarCount = values.CarCount;
                ViewBag.BrandCount = values.BrandCount;
                ViewBag.AvgRentPriceForDaily = values.AvgRentPriceForDaily.ToString("0.00");
                ViewBag.LocationCount = values.LocationCount;

                // Random values
                ViewBag.CarCountRandom = random.Next(0, 101);
                ViewBag.BrandCountRandom = random.Next(0, 101);
                ViewBag.LocationCountRandom = random.Next(0, 101);
                ViewBag.AvgRentPriceForDailyRandom = random.Next(0, 101);
            }

            return View();
        }
    }
}
