using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class StatisticManager : IStatisticService
    {
        private readonly IStatisticDal _statisticDal;

        public StatisticManager(IStatisticDal statisticDal)
        {
            _statisticDal = statisticDal;
        }

        public decimal TGetAvgRentPriceForDaily()
        {
            var values= _statisticDal.GetAvgRentPriceForDaily();
            return values;
           
        }

        public decimal TGetAvgRentPriceForMonthly()
        {
            var values = _statisticDal.GetAvgRentPriceForMonthly();
            return values;
        }

        public decimal TGetAvgRentPriceForWeekly()
        {
            var values = _statisticDal.GetAvgRentPriceForWeekly();
            return values;
        }

        public int TGetBrandCount()
        {
            var values = _statisticDal.GetBrandCount();
            return values;
        }

        public string TGetBrandNameByMaxCar()
        {
            var values = _statisticDal.GetBrandNameByMaxCar();
            return values;
        }

        public string TGetCarBrandAndModelByRentPriceDailyMax()
        {
            var values = _statisticDal.GetCarBrandAndModelByRentPriceDailyMax();
            return values;
        }

        public string TGetCarBrandAndModelByRentPriceDailyMin()
        {
            var values = _statisticDal.GetCarBrandAndModelByRentPriceDailyMin();
            return values;
        }

        public int TGetCarCount()
        {
            var values = _statisticDal.GetCarCount();
            return values;
        }

        public int TGetCarCountByFuelElectric()
        {
           var values = _statisticDal.GetCarCountByFuelElectric();
            return values;
        }

        public int TGetCarCountByFuelGasolineOrDiesel()
        {
            var values = _statisticDal.GetCarCountByFuelGasolineOrDiesel();
            return values;
        }

        public int TGetCarCountByKmSmallerThen1000()
        {
            var values = _statisticDal.GetCarCountByKmSmallerThen1000();
            return values;
        }

        public int TGetLocationCount()
        {
            var values = _statisticDal.GetLocationCount();
            return values;
        }
    }
}
