
using BusinessLayer.Abstract;
using DtoLayer.StatisticsDtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticsController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        [HttpGet("GetAllStatistics")]
        public async Task<IActionResult> GetAllStatistics()
        {
            var statisticsDto = new ResultStatisticsDto
            {
                CarCount = _statisticService.TGetCarCount(),
                BrandCount = _statisticService.TGetBrandCount(),
                BrandNameByMaxCar = _statisticService.TGetBrandNameByMaxCar(),
                CarBrandAndModelByRentPriceDailyMax = _statisticService.TGetCarBrandAndModelByRentPriceDailyMax(),
                CarBrandAndModelByRentPriceDailyMin = _statisticService.TGetCarBrandAndModelByRentPriceDailyMin(),
                AvgRentPriceForDaily = _statisticService.TGetAvgRentPriceForDaily(),
                AvgRentPriceForWeekly = _statisticService.TGetAvgRentPriceForWeekly(),
                AvgRentPriceForMonthly = _statisticService.TGetAvgRentPriceForMonthly(),
                CarCountByFuelElectric = _statisticService.TGetCarCountByFuelElectric(),
                CarCountByFuelGasolineOrDiesel = _statisticService.TGetCarCountByFuelGasolineOrDiesel(),
                CarCountByKmSmallerThen1000 = _statisticService.TGetCarCountByKmSmallerThen1000(),
                LocationCount = _statisticService.TGetLocationCount()
            };

            return Ok(statisticsDto);
        }
    }
}
