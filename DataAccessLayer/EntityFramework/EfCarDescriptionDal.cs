﻿using DataAccessLayer.Abstract;
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
    public class EfCarDescriptionDal : GenericRepository<CarDescription>, ICarDescriptionDal
    {
        public EfCarDescriptionDal(RenASeatContext renASeatContext) : base(renASeatContext)
        {
        }
    }
}
