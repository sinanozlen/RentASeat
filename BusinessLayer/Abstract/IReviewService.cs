using DtoLayer.ReviewDtos;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IReviewService:IGenericService<Review>
    {
        List<ResultReviewByCarIdDto> TGetReviewsByCarId(int carId);
    }
}
