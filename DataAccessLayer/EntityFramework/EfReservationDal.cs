using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Migrations;
using DataAccessLayer.Repositories;
using DtoLayer.ReservationDtos;
using EntitityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfReservationDal : GenericRepository<Reservation>, IReservationDal
    {
        private readonly RenASeatContext _context;
        public EfReservationDal(RenASeatContext renASeatContext, RenASeatContext context) : base(renASeatContext)
        {
            _context = context;
        }

        public void ChangeReservationStatusToConfirm(int id)
        {
            var values = _context.Reservation.Where(x => x.ReservationID == id).FirstOrDefault();
            if (values != null)
            {
                values.Status = "Onaylandı";
                _context.SaveChanges();
            }
        }

        public void ChangeReservationStatusToDecline(int id)
        {
            var values = _context.Reservation.Where(x => x.ReservationID == id).FirstOrDefault();
            if (values != null)
            {
                values.Status = "Reddedildi";
                _context.SaveChanges();
            }
        }

        public List<ResultReservationDto> GetReservationWithLocationAndCar()
        {
            var result = _context.Reservation
                                 .Include(x => x.Car)
                                 .ThenInclude(c => c.Brand) 
                                 .Include(y => y.PickUpLocation)
                                 .Include(z => z.DropOffLocation)
                                 .ToList();

            var reservationDtos = result.Select(x => new ResultReservationDto
            {
                ReservationID = x.ReservationID,
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email,
                Phone = x.Phone,
                PickUpLocationName = x.PickUpLocation.Name,
                DropOffLocationName = x.DropOffLocation.Name,
                CarName = x.Car.Brand.Name + " " + x.Car.Model,
                Age = x.Age,
                DriverLicenseYear = x.DriverLicenseYear,
                Description = x.Description,
                Status = x.Status
            }).ToList();

            return reservationDtos;
        }

        public ResultReservationDto GetReservationDetails(int reservationID)
        {
            using (var context = new RenASeatContext())
            {
                var reservation = context.Reservation
                                        .Include(x => x.Car)
                                            .ThenInclude(c => c.Brand)
                                        .Include(y => y.PickUpLocation)
                                        .Include(z => z.DropOffLocation)
                                        .FirstOrDefault(x => x.ReservationID == reservationID);

                if (reservation == null)
                {
                    return null;
                }

                var reservationDto = new ResultReservationDto
                {
                    ReservationID = reservation.ReservationID,
                    Name = reservation.Name,
                    Surname = reservation.Surname,
                    Email = reservation.Email,
                    Phone = reservation.Phone,
                    PickUpLocationName = reservation.PickUpLocation.Name,
                    DropOffLocationName = reservation.DropOffLocation.Name,
                    CarName = reservation.Car.Brand.Name + " " + reservation.Car.Model,
                    Age = reservation.Age,
                    DriverLicenseYear = reservation.DriverLicenseYear,
                    Description = reservation.Description,
                    Status = reservation.Status
                };

                return reservationDto;
            }
        }

    }
}
