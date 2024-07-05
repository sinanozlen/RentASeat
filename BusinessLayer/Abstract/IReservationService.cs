using DtoLayer.ReservationDtos;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IReservationService:IGenericService<Reservation>
    {
        void TChangeReservationStatusToConfirm(int id);
        void TChangeReservationStatusToDecline(int id);
        List<ResultReservationDto> TGetReservationWithLocationAndCar();
        ResultReservationDto TGetReservationDetails(int reservationID);
    }
}
