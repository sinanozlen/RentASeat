using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DtoLayer.CarDtos;
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

        public List<Result5CarsWithBrandsDto> TGet5CarsWithBrandsDtos()
        {
            var values = _carDal.Get5CarsWithBrandsDtos();
            return values;
        }

        public Car TGetbyID(int ID)
        {
            var values =_carDal.GetbyID(ID);
            return values;
        }

        public List<ResultCarWithBrandDto> TGetCarsWithBrand()
        {
           var values= _carDal.GetCarsWithBrand();
            return values;
        }

        public ResultCarWithBrandDto TGetCarsWithBrand(int id)
        {
            var values = _carDal.GetCarsWithBrand(id);
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
