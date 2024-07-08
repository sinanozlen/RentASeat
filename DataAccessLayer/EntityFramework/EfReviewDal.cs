using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using DtoLayer.ReviewDtos;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfReviewDal : GenericRepository<Review>, IReviewDal
    {
        public EfReviewDal(RenASeatContext renASeatContext) : base(renASeatContext)
        {
        }

        public List<ResultReviewByCarIdDto> GetReviewsByCarId(int carId)
        {
            using var context = new RenASeatContext();
            var values=context.Reviews.Where(x=> x.CarID == carId).Select(x => new ResultReviewByCarIdDto
            {
                CarID = x.CarID,
                Comment = x.Comment,
                CustomerImage = x.CustomerImage,
                CustomerName = x.CustomerName,
                RatingValue = x.RatingValue,
                ReviewDate=x.ReviewDate
                
            }).ToList();
            return values;
        }
    }
}
