using DtoLayer.CarPricingDtos;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICarPricingDal:IGenericDal<CarPricing>
    {
        List<ResultCarPricingWithCarDto> GetCarPricingWithCars();
        List<ResultCarPricingListWithModelDto> GetCarPricingListWithModel();
    }
}
