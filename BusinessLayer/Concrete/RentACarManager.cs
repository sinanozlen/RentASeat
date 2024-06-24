using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DtoLayer.RentACarDtos;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class RentACarManager : IRentACarService
    {
        private readonly IRentACarDal _rentACarDal;

        public RentACarManager(IRentACarDal rentACarDal)
        {
            _rentACarDal = rentACarDal;
        }

        public void TAdd(RentACar entity)
        {
         _rentACarDal.Add(entity);   
        }

        public void TDelete(RentACar entity)
        {
            _rentACarDal.Delete(entity);
        }

        public Task<List<FilterRentACarDto>> TGetByFilterAsync(Expression<Func<RentACar, bool>> filter)
        {
            var values=_rentACarDal.GetByFilterAsync(filter);
            return values;
        }

        public RentACar TGetbyID(int ID)
        {
            var values=_rentACarDal.GetbyID(ID);
            return values;
        }

        public List<RentACar> TGetListAll()
        {
            var values=_rentACarDal.GetListAll();
            return values;
        }

        public void TUpdate(RentACar entity)
        {
            _rentACarDal.Update(entity);
        }
    }
}
