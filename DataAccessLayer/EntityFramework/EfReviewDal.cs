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

            var reviews = context.Reviews
                .Where(x => x.CarID == carId)
                .Select(x => new ResultReviewByCarIdDto
                {
                    CarID = x.CarID,
                    CustomerName = x.CustomerName,
                    CustomerImage = x.CustomerImage,
                    Comment = x.Comment,
                    RatingValue = x.RatingValue,
                    ReviewDate = x.ReviewDate
                })
                .ToList();

            if (reviews == null || reviews.Count == 0)
            {
                // Yorum bulunamadı durumunda "Yorum bulunamadı" mesajını döndür
                return new List<ResultReviewByCarIdDto>
        {
            new ResultReviewByCarIdDto
            {
                CarID = carId,
                CustomerName = "Yorum bulunamadı",
                CustomerImage = null, // veya isteğe bağlı olarak bir varsayılan resim
                Comment = "",
                RatingValue = 0,
                ReviewDate = DateTime.MinValue
            }
        };
            }

            return reviews;
        }


    }
}
