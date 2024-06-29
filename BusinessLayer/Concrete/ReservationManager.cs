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
    public class ReservationManager : IReservationService
    {
        private readonly IReservationDal _reservationDal;

        public ReservationManager(IReservationDal reservationDal)
        {
            _reservationDal = reservationDal;
        }

        public void TAdd(Reservation entity)
        {
           _reservationDal.Add(entity);

        }

        public void TDelete(Reservation entity)
        {
            _reservationDal.Delete(entity);
        }

        public Reservation TGetbyID(int ID)
        {
            var values  = _reservationDal.GetbyID(ID);
            return values;
        }

        public List<Reservation> TGetListAll()
        {
            var values = _reservationDal.GetListAll();
            return values;
        }

        public void TUpdate(Reservation entity)
        {
          _reservationDal.Update(entity);
        }
    }
}
