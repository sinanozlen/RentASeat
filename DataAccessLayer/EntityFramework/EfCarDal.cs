using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using DtoLayer.CarDtos;
using EntitityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfCarDal : GenericRepository<Car>, ICarDal
    {

        public EfCarDal(RenASeatContext renASeatContext) : base(renASeatContext)
        {
        }

        public List<ResultCarWithBrandDto> GetCarsWithBrand()
        {
            using var context = new RenASeatContext();
            var carsWithBrand = context.Cars
           .Include(x => x.Brand)
           .Select(x => new ResultCarWithBrandDto
           {
               BrandName = x.Brand.Name,
               BrandID = x.BrandID,
               BigImageUrl = x.BigImageUrl,
               CarID = x.CarID,
               CoverImageUrl = x.CoverImageUrl,
               Fuel = x.Fuel,
               Km = x.Km,
               Luggage = x.Luggage,
               Model = x.Model,
               Seat = x.Seat,
               Transmission = x.Transmission
           })
           .ToList();
            return carsWithBrand;
        }
    }
}
