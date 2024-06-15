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
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public void TAdd(Brand entity)
        {
            _brandDal.Add(entity);
        }

        public void TDelete(Brand entity)
        {
           _brandDal.Delete(entity);
        }

        public Brand TGetbyID(int ID)
        {
           var values= _brandDal.GetbyID(ID);
            return values;
        }

        public List<Brand> TGetListAll()
        {
           var values= _brandDal.GetListAll();
            return values;
        }

        public void TUpdate(Brand entity)
        {
            _brandDal.Update(entity);
        }
    }
}
