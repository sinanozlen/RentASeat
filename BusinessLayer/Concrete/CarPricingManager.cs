using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    internal class CarPricingManager : ICarPricingService
    {
        private readonly ICarPricingDal _carPricingDal;

        public CarPricingManager(ICarPricingDal carPricingDal)
        {
            _carPricingDal = carPricingDal;
        }

        public void TAdd(CarPricing entity)
        {
            _carPricingDal.Add(entity);
        }

        public void TDelete(CarPricing entity)
        {
            _carPricingDal.Delete(entity);
        }

        public CarPricing TGetbyID(int ID)
        {
           var values=_carPricingDal.GetbyID(ID);
            return values;
        }

        public List<CarPricing> TGetListAll()
        {
            var values=_carPricingDal.GetListAll();
            return values;
        }

        public void TUpdate(CarPricing entity)
        {
            _carPricingDal.Update(entity);
        }
    }
}
