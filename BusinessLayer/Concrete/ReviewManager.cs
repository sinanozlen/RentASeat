using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DtoLayer.ReviewDtos;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ReviewManager : IReviewService
    {
        private readonly IReviewDal _reviewDal;

        public ReviewManager(IReviewDal reviewDal)
        {
            _reviewDal = reviewDal;
        }

        public void TAdd(Review entity)
        {
            _reviewDal.Add(entity);
            
        }

        public void TDelete(Review entity)
        {
            _reviewDal.Delete(entity);
        }

        public Review TGetbyID(int ID)
        {
            var values=_reviewDal.GetbyID(ID);
            return values;
        }

        public List<Review> TGetListAll()
        {
           var values= _reviewDal.GetListAll();
            return values;
        }

        public List<ResultReviewByCarIdDto> TGetReviewsByCarId(int carId)
        {
            var values = _reviewDal.GetReviewsByCarId(carId);
            return values;
        }

        public void TUpdate(Review entity)
        {
            _reviewDal.Update(entity);
        }
    }
}
