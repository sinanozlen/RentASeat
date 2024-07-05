using DtoLayer.ReservationDtos;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IReservationDal:IGenericDal<Reservation>
    {
        void ChangeReservationStatusToConfirm(int id);
        void ChangeReservationStatusToDecline(int id);
        List<ResultReservationDto> GetReservationWithLocationAndCar();
        ResultReservationDto GetReservationDetails(int reservationID);

    }
}
