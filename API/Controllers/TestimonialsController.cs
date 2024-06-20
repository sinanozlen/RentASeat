using BusinessLayer.Abstract;
using DtoLayer.TestimonialDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestimonialsController : ControllerBase
	{
		private readonly ITestimonialService _testimonialService;

		public TestimonialsController(ITestimonialService testimonialService)
		{
			_testimonialService = testimonialService;
		}
		[HttpGet]
		public IActionResult TestimonialList()
		{
			var testimonials = _testimonialService.TGetListAll();
			return Ok(testimonials);
		}
		[HttpGet("{id}")]
		public IActionResult TestimonialDetail(int id)
		{
			var testimonial = _testimonialService.TGetbyID(id);
			if (testimonial == null)
			{
				return NotFound();
			}
			return Ok(testimonial);
		}
		[HttpDelete("{id}")]
		public IActionResult Deletetestimonial(int id)
		{
			var testimonial = _testimonialService.TGetbyID(id);
			if (testimonial == null)
			{
				return NotFound();
			}
			_testimonialService.TDelete(testimonial);
			return Ok("Referans Silme İşlemi Başarı İle Gerçekleştirildi");
		}
		[HttpPost]
		public IActionResult Createtestimonial(CreateTestimonialDto createTestimonialDto)
		{
			var testimonial= new Testimonial()
			{
				Comment = createTestimonialDto.Comment,
				ImageUrl = createTestimonialDto.ImageUrl,
				Name = createTestimonialDto.Name,
				Title = createTestimonialDto.Title
			};
			_testimonialService.TAdd(testimonial);
			return Ok("Referans Ekleme İşlemi Başarı İle Gerçekleştirildi");
		}
		[HttpPut]
		public IActionResult Updatetestimonial(UpdateTestimonialDto updateTestimonialDto)
		{
			var testimonial = new Testimonial()
			{
				TestimonialID = updateTestimonialDto.TestimonialID,
				Comment = updateTestimonialDto.Comment,
				ImageUrl = updateTestimonialDto.ImageUrl,
				Name = updateTestimonialDto.Name,
				Title = updateTestimonialDto.Title
			};
			_testimonialService.TUpdate(testimonial);
			return Ok("Referans Güncelleme İşlemi Başarı İle Gerçekleştirildi");
		}
	}
}
