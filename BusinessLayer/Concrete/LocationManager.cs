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
    public class LocationManager : ILocationService
    {
        private readonly ILocationDal _locationDal;

        public LocationManager(ILocationDal locationDal)
        {
            _locationDal = locationDal;
        }


        public void TAdd(Location entity)
        {
            _locationDal.Add(entity);
        }

        public void TDelete(Location entity)
        {
            _locationDal.Delete(entity);
        }

        public Location TGetbyID(int ID)
        {
           var values= _locationDal.GetbyID(ID);
            return values;
        }

        public List<Location> TGetListAll()
        {
          var values= _locationDal.GetListAll();
            return values;
        }

        public void TUpdate(Location entity)
        {
            _locationDal.Update(entity);
        }
    }
}
