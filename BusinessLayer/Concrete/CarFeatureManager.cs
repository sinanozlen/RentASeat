using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DtoLayer.CarFeatureDtos;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CarFeatureManager : ICarFeatureService
    {
        private readonly ICarFeatureDal _carFeatureDal;

        public CarFeatureManager(ICarFeatureDal carFeatureDal)
        {
            _carFeatureDal = carFeatureDal;
        }

        public void TAdd(CarFeature entity)
        {
            _carFeatureDal.Add(entity);
        }

        public void TChangeCarFeatureAvailableToFalse(int id)
        {
            _carFeatureDal.ChangeCarFeatureAvailableToFalse(id);
        }

        public void TChangeCarFeatureAvailableToTrue(int id)
        {
            _carFeatureDal.ChangeCarFeatureAvailableToTrue(id);
        }

        public void TCreateCarFeatureByCar(CarFeature carFeature)
        {
            _carFeatureDal.CreateCarFeatureByCar(carFeature);
        }

        public void TDelete(CarFeature entity)
        {
            _carFeatureDal.Delete(entity);
        }

        public CarFeature TGetbyID(int ID)
        {
            var values =_carFeatureDal.GetbyID(ID);
            return values;
        }

        public Task<List<ResultCarFeatureByCarIdDto>> TGetCarFeaturesByCarID(int carID)
        {
            var values= _carFeatureDal.GetCarFeaturesByCarID(carID);
            return values;
        }

        public List<CarFeature> TGetListAll()
        {
            var values = _carFeatureDal.GetListAll();
            return values;
        }

        public void TUpdate(CarFeature entity)
        {
            _carFeatureDal.Update(entity);
        }
    }
}
