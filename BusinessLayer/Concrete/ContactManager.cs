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
	public class ContactManager : IContactService
	{
		private readonly IContactDal _contactDal;

		public ContactManager(IContactDal contactDal)
		{
			_contactDal = contactDal;
		}

		public void TAdd(Contact entity)
		{
			_contactDal.Add(entity);
		}

		public void TDelete(Contact entity)
		{
			_contactDal.Delete(entity);
		}

		public Contact TGetbyID(int ID)
		{
			var values= _contactDal.GetbyID(ID);
			return values;
		}

		public List<Contact> TGetListAll()
		{
			var values= _contactDal.GetListAll();
			return values;
		}

		public void TUpdate(Contact entity)
		{
			_contactDal.Update(entity);
		}
	}
}
