using DtoLayer.CarPricingDtos;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICarPricingService:IGenericService<CarPricing>
    {
        List<ResultCarPricingWithCarDto> TGetCarPricingWithCars();
        List<ResultCarPricingListWithModelDto> TGetCarPricingListWithModel();
    }
}
