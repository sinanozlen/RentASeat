using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CarManager : ICarService
    {

        private readonly ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public void TAdd(Car entity)
        {
            _carDal.Add(entity);
        }

        public void TDelete(Car entity)
        {
            _carDal.Delete(entity);
        }

        public Car TGetbyID(int ID)
        {
            var values =_carDal.GetbyID(ID);
            return values;
        }

        public List<Car> TGetListAll()
        {
            var values = _carDal.GetListAll();
            return values;
        }

        public void TUpdate(Car entity)
        {
            _carDal.Update(entity);
        }
    }
}
