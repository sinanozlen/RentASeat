using DtoLayer.CarDtos;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICarDal:IGenericDal<Car>
    {
        List<ResultCarWithBrandDto> GetCarsWithBrand();
        ResultCarWithBrandDto GetCarsWithBrand(int id);

        List<Result5CarsWithBrandsDto> Get5CarsWithBrandsDtos();

    }
}
