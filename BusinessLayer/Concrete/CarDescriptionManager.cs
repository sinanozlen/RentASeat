using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CarDescriptionManager : ICarDescriptionService
    {
        private readonly ICarDescriptionDal _carDescriptionService;

        public CarDescriptionManager(ICarDescriptionDal carDescriptionService)
        {
            _carDescriptionService = carDescriptionService;
        }

        public void TAdd(CarDescription entity)
        {
            _carDescriptionService.Add(entity);
        }

        public void TDelete(CarDescription entity)
        {
            _carDescriptionService.Delete(entity);
        }

        public CarDescription TGetbyID(int ID)
        {
           var values= _carDescriptionService.GetbyID(ID);
            return values;
        }

        public List<CarDescription> TGetListAll()
        {
           var values= _carDescriptionService.GetListAll();
            return values;
        }

        public void TUpdate(CarDescription entity)
        {
            _carDescriptionService.Update(entity);
        }
    }
}
