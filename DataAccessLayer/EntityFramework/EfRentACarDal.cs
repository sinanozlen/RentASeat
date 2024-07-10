using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using DtoLayer.RentACarDtos;
using EntitityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfRentACarDal : GenericRepository<RentACar>, IRentACarDal
    {
        public EfRentACarDal(RenASeatContext renASeatContext) : base(renASeatContext)
        {

        }

        public async Task<List<FilterRentACarDto>> GetByFilterAsync(Expression<Func<RentACar, bool>> filter)
        {
            using var _context = new RenASeatContext();

            var query = _context.RentACar
                                .Where(filter)
                                .Include(x => x.Car)
                                    .ThenInclude(y => y.Brand)
                                .Include(x => x.Car)
                                    .ThenInclude(y => y.CarPricings);

            var results = await query.Select(y => new
            {
                y.CarID,
                BrandName = y.Car.Brand.Name,
                y.Car.Model,
                y.Car.CoverImageUrl,
                Amount = y.Car.CarPricings.OrderBy(cp => cp.CarPricingID).FirstOrDefault().Amount
            }).ToListAsync();

            var filterRentACarDtos = results.Select(y => new FilterRentACarDto
            {
                carID = y.CarID,
                Brand = y.BrandName,
                Model = y.Model,
                CoverImageUrl = y.CoverImageUrl,
                Amount = y.Amount
            }).ToList();

            return filterRentACarDtos;
        }


    }
}