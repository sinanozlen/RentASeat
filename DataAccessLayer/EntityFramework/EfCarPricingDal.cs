using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using DtoLayer.CarPricingDtos;
using EntitityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfCarPricingDal : GenericRepository<CarPricing>, ICarPricingDal
    {
        public EfCarPricingDal(RenASeatContext renASeatContext) : base(renASeatContext)
        {
        }

        public List<ResultCarPricingWithCarDto> GetCarPricingWithCars()
        {
            using var context = new RenASeatContext();
            var values = context.CarPricings
                                .Include(cp => cp.Car)
                                .ThenInclude(c => c.Brand)
                                .GroupBy(cp => cp.CarID)
                                .Select(g => g.First())
                                .ToList();

            return values.Select(x => new ResultCarPricingWithCarDto
            {
                Amount = x.Amount,
                CarPricingId = x.CarPricingID,
                Brand = x.Car.Brand.Name,
                CoverImageUrl = x.Car.CoverImageUrl,
                Model = x.Car.Model,
                CarId = x.CarID
            }).ToList();
        }

        public List<ResultCarPricingListWithModelDto> GetCarPricingListWithModel()
        {
            using var context = new RenASeatContext();
            var values = context.CarPricings
                       .Include(cp => cp.Car)
                       .ThenInclude(c => c.Brand)
                       .Include(cp => cp.Pricing)
                       .ToList();

            var result = new List<ResultCarPricingListWithModelDto>();

            var groupedPricings = values.GroupBy(x => x.CarID);

            foreach (var group in groupedPricings)
            {
                var carPricingList = group.ToList();
                var carPricing = carPricingList.First();

                decimal dailyAmount = 0m;
                decimal weeklyAmount = 0m;
                decimal monthlyAmount = 0m;

                var dailyPricing = carPricingList.FirstOrDefault(x => x.Pricing.Name == "Günlük Kiralama Ücreti");
                if (dailyPricing != null)
                {
                    dailyAmount = dailyPricing.Amount;
                }

                var weeklyPricing = carPricingList.FirstOrDefault(x => x.Pricing.Name == "Haftalık Kiralama Ücreti");
                if (weeklyPricing != null)
                {
                    weeklyAmount = weeklyPricing.Amount;
                }

                var monthlyPricing = carPricingList.FirstOrDefault(x => x.Pricing.Name == "Aylık Kiralama Ücreti");
                if (monthlyPricing != null)
                {
                    monthlyAmount = monthlyPricing.Amount;
                }

                result.Add(new ResultCarPricingListWithModelDto
                {
                    Brand = carPricing.Car.Brand.Name,
                    Model = carPricing.Car.Model,
                    CoverImageUrl = carPricing.Car.CoverImageUrl,
                    DailyAmount = dailyAmount,
                    WeeklyAmount = weeklyAmount,
                    MonthlyAmount = monthlyAmount
                });
            }

            return result;
        }

    }
}
