using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IStatisticService
    {
        int TGetCarCount();
        int TGetBrandCount();
        string TGetBrandNameByMaxCar();
        string TGetCarBrandAndModelByRentPriceDailyMax();
        string TGetCarBrandAndModelByRentPriceDailyMin();
        decimal TGetAvgRentPriceForDaily();
        decimal TGetAvgRentPriceForWeekly();
        decimal TGetAvgRentPriceForMonthly();
        int TGetCarCountByFuelElectric();
        int TGetCarCountByFuelGasolineOrDiesel();
        int TGetCarCountByKmSmallerThen1000();
        int TGetLocationCount();
    }
}
