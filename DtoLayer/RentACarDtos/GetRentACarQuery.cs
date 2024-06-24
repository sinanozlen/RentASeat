using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.RentACarDtos
{
    public class GetRentACarQuery 
    {
        public int LocationID { get; set; }
        public bool Available { get; set; }
    }
}
