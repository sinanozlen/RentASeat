using DtoLayer.ReviewDtos;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IReviewDal:IGenericDal<Review>
    {
        List<ResultReviewByCarIdDto> GetReviewsByCarId(int carId);
    }
}
