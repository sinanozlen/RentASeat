using DtoLayer.RentACarDtos;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
        public interface IRentACarDal : IGenericDal<RentACar>
        {
           public Task<List<FilterRentACarDto>> GetByFilterAsync(Expression<Func<RentACar, bool>> filter);
        }
}
