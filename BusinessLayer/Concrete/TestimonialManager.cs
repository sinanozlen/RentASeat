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
	public class TestimonialManager : ITestimonialService
	{
		private readonly ITestimonialDal _testimonialDal;

		public TestimonialManager(ITestimonialDal testimonialDal)
		{
			_testimonialDal = testimonialDal;
		}

		public void TAdd(Testimonial entity)
		{
			_testimonialDal.Add(entity);
		}

		public void TDelete(Testimonial entity)
		{
			_testimonialDal.Delete(entity);
		}

		public Testimonial TGetbyID(int ID)
		{
			var values=_testimonialDal.GetbyID(ID);
			return values;
		}

		public List<Testimonial> TGetListAll()
		{
			var values=_testimonialDal.GetListAll();
			return values;
		}

		public void TUpdate(Testimonial entity)
		{
			_testimonialDal.Update(entity);
		}
	}
}
