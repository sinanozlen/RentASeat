using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IStatisticDal
    {
        int GetCarCount();
        int GetBrandCount();
        string GetBrandNameByMaxCar();
        string GetCarBrandAndModelByRentPriceDailyMax();
        string GetCarBrandAndModelByRentPriceDailyMin();
        decimal GetAvgRentPriceForDaily();
        decimal GetAvgRentPriceForWeekly();
        decimal GetAvgRentPriceForMonthly();
        int GetCarCountByFuelElectric();
        int GetCarCountByFuelGasolineOrDiesel();
        int GetCarCountByKmSmallerThen1000();
        int GetLocationCount();
    }
}
