﻿using DtoLayer.CarDtos;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICarService:IGenericService<Car>
    {
        List<ResultCarWithBrandDto> TGetCarsWithBrand();
        List<Result5CarsWithBrandsDto> TGet5CarsWithBrandsDtos();
        ResultCarWithBrandDto TGetCarsWithBrand(int id);
    }
}
