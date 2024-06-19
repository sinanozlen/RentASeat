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
	public class AboutManager : IAboutService
	{
		private readonly IAboutDal _aboutDal;

		public AboutManager(IAboutDal aboutDal)
		{
			_aboutDal = aboutDal;
		}

		public void TAdd(About entity)
		{
			_aboutDal.Add(entity);
		}

		public void TDelete(About entity)
		{
			_aboutDal.Delete(entity);
		}

		public About TGetbyID(int ID)
		{
			var values = _aboutDal.GetbyID(ID);
			return values;
		}

		public List<About> TGetListAll()
		{
			var values = _aboutDal.GetListAll();
			return values;
		}

		public void TUpdate(About entity)
		{
			_aboutDal.Update(entity);
		}
	}
}
