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
            using var context=new RenASeatContext();
            var values = context.CarPricings.Include(cp => cp.Car).ThenInclude(c => c.Brand).ToList();
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
    }
}
