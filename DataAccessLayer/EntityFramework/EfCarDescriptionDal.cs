using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using DtoLayer.CarDescriptionDtos;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfCarDescriptionDal : GenericRepository<CarDescription>, ICarDescriptionDal
    {
       
        public EfCarDescriptionDal(RenASeatContext renASeatContext) : base(renASeatContext)
        {
        }



        public ResultCarDescriptionByCarIdDto GetCarDescriptionWithCarID(int carID)
        {
            using var _renASeatContext = new RenASeatContext();
            var values = _renASeatContext.CarDescriptions.FirstOrDefault(x => x.CarID == carID);

            if (values == null)
            {
                // Eğer kayıt bulunamazsa, boş bir DTO döndürüyoruz veya istediğiniz şekilde yanıt verebilirsiniz.
                return new ResultCarDescriptionByCarIdDto
                {
                    CarID = carID,
                    Details = "Açıklama bulunamadı",
                    CarDescriptionID = 0
                };
            }

            ResultCarDescriptionByCarIdDto result = new ResultCarDescriptionByCarIdDto
            {
                CarID = values.CarID,
                Details = values.Details,
                CarDescriptionID = values.CarDescriptionID
            };
            return result;
        }

    }
}
