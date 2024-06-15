using BusinessLayer.Abstract;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    internal class LocationManager : ILocationService
    {
        private readonly ILocationService _locationService;

        public LocationManager(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public void TAdd(Location entity)
        {
            _locationService.TAdd(entity);
        }

        public void TDelete(Location entity)
        {
            _locationService.TDelete(entity);
        }

        public Location TGetbyID(int ID)
        {
           var values= _locationService.TGetbyID(ID);
            return values;
        }

        public List<Location> TGetListAll()
        {
          var values= _locationService.TGetListAll();
            return values;
        }

        public void TUpdate(Location entity)
        {
           _locationService.TUpdate(entity);
        }
    }
}
