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
	public class FooterAddressManager : IFooterAddressService
	{
		private readonly IFooterAddressDal _footerAddressDal;

		public FooterAddressManager(IFooterAddressDal footerAddressDal)
		{
			_footerAddressDal = footerAddressDal;
		}

		public void TAdd(FooterAddress entity)
		{
			_footerAddressDal.Add(entity);
		}

		public void TDelete(FooterAddress entity)
		{
			_footerAddressDal.Delete(entity);
		}

		public FooterAddress TGetbyID(int ID)
		{
			var values=_footerAddressDal.GetbyID(ID);
			return values;
		}

		public List<FooterAddress> TGetListAll()
		{
			var values=_footerAddressDal.GetListAll();
			return values;
		}

		public void TUpdate(FooterAddress entity)
		{
			_footerAddressDal.Update(entity);
		}
	}
}
