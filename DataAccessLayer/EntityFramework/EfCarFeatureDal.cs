using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntitityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfCarFeatureDal : GenericRepository<CarFeature>, ICarFeatureDal
    {
        public EfCarFeatureDal(RenASeatContext renASeatContext) : base(renASeatContext)
        {
        }
    }
}
