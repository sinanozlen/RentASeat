using DtoLayer.RentACarDtos;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IRentACarService:IGenericService<RentACar>
    {
        Task<List<FilterRentACarDto>> TGetByFilterAsync(Expression<Func<RentACar, bool>> filter);
    }
}
