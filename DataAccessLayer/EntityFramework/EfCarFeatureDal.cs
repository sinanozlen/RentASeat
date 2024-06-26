using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using DtoLayer.CarFeatureDtos;
using EntitityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfCarFeatureDal : GenericRepository<CarFeature>, ICarFeatureDal
    {
        private readonly RenASeatContext _context;
        public EfCarFeatureDal(RenASeatContext renASeatContext, RenASeatContext context) : base(renASeatContext)
        {
            _context = context;
        }

        public async Task< List<ResultCarFeatureByCarIdDto>> GetCarFeaturesByCarID(int carID)
        {
            var values= _context.CarFeatures.Where(x => x.CarID == carID).ToList();
            return values.Select(x=> new ResultCarFeatureByCarIdDto
            {
                CarFeatureID = x.CarFeatureID,
                
                FeatureID = x.FeatureID,
                Available = x.Available
            }).ToList();
        }
        public void ChangeCarFeatureAvailableToFalse(int id)
        {
            var values = _context.CarFeatures.Where(x => x.CarFeatureID == id).FirstOrDefault();
            values.Available = false;
            _context.SaveChanges();
        }

        public async void ChangeCarFeatureAvailableToTrue(int id)
        {
            var values = _context.CarFeatures.Where(x => x.CarFeatureID == id).FirstOrDefault();
            values.Available = true;
            _context.SaveChanges();
        }

        public void CreateCarFeatureByCar(CarFeature carFeature)
        {
            _context.CarFeatures.Add(carFeature);
            _context.SaveChanges();
        }

    }
}
