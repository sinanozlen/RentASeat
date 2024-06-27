using DtoLayer.CarFeatureDtos;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICarFeatureDal:IGenericDal<CarFeature>
    {
        Task< List<ResultCarFeatureByCarIdDto>> GetCarFeaturesByCarID(int carID);
        void ChangeCarFeatureAvailableToFalse(int id);
        void ChangeCarFeatureAvailableToTrue(int id);
        void CreateCarFeatureByCar(CreateCarFeatureDto createCarFeatureDto);
    }
}
