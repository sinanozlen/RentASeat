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
	public class BannerManager : IBannerService
	{
		private readonly IBannerDal _bannerDal;

		public BannerManager(IBannerDal bannerDal)
		{
			_bannerDal = bannerDal;
		}

		public void TAdd(Banner entity)
		{
			_bannerDal.Add(entity);
			
		}

		public void TDelete(Banner entity)
		{
			_bannerDal.Delete(entity);	
		}

		public Banner TGetbyID(int ID)
		{
			var values = _bannerDal.GetbyID(ID);
			return values;
		}

		public List<Banner> TGetListAll()
		{
			var values= _bannerDal.GetListAll();
			return values;
		}

		public void TUpdate(Banner entity)
		{
			_bannerDal.Update(entity);
		}
	}
}
