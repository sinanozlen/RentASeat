using DtoLayer.StatisticsDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultStatisticsComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DefaultStatisticsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://api.rentaseat.com.tr/api/Statistics/GetAllStatistics");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var statistics = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);

                ViewBag.CarCount = statistics.CarCount;
                ViewBag.BrandCount = statistics.BrandCount;
                ViewBag.CarCountByFuelElectric = statistics.CarCountByFuelElectric;
                ViewBag.LocationCount = statistics.LocationCount;
            }

            return View();
        }
    }
}
