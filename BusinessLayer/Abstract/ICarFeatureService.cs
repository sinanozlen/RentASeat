using DtoLayer.CarFeatureDtos;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICarFeatureService:IGenericService<CarFeature>
    {
        Task<List<ResultCarFeatureByCarIdDto>> TGetCarFeaturesByCarID(int carID);
        void TChangeCarFeatureAvailableToFalse(int id);
        void TChangeCarFeatureAvailableToTrue(int id);
        void TCreateCarFeatureByCar(CreateCarFeatureDto createCarFeatureDto);
    }
}
