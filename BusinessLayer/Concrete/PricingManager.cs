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
    public class PricingManager : IPricingService
    {
        private readonly IPricingDal _pricingDal;

        public PricingManager(IPricingDal pricingDal)
        {
            _pricingDal = pricingDal;
        }

        public void TAdd(Pricing entity)
        {
            _pricingDal.Add(entity);
        }

        public void TDelete(Pricing entity)
        {
            _pricingDal.Delete(entity);
        }

        public Pricing TGetbyID(int ID)
        {
          var values= _pricingDal.GetbyID(ID);
            return values;
        }

        public List<Pricing> TGetListAll()
        {
            var values=_pricingDal.GetListAll();
            return  values;
        }

        public void TUpdate(Pricing entity)
        {
            _pricingDal.Update(entity);
        }
    }
}
