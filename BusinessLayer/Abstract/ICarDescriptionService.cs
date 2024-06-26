﻿using DtoLayer.CarDescriptionDtos;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICarDescriptionService:IGenericService<CarDescription>
    {
        ResultCarDescriptionByCarIdDto TGetCarDescriptionWithCarID(int carID);
    }
}
