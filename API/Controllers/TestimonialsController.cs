using BusinessLayer.Abstract;
using DtoLayer.TestimonialDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly ITestimonialService _testimonialService;
        private readonly ILogger<TestimonialsController> _logger;

        public TestimonialsController(ITestimonialService testimonialService, ILogger<TestimonialsController> logger)
        {
            _testimonialService = testimonialService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult TestimonialList()
        {
            try
            {
                var testimonials = _testimonialService.TGetListAll();
                return Ok(testimonials);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving testimonials");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult TestimonialDetail(int id)
        {
            try
            {
                var testimonial = _testimonialService.TGetbyID(id);
                if (testimonial == null)
                    return NotFound();

                return Ok(testimonial);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving testimonial with id: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletetestimonial(int id)
        {
            try
            {
                var testimonial = _testimonialService.TGetbyID(id);
                if (testimonial == null)
                    return NotFound();

                _testimonialService.TDelete(testimonial);
                return Ok("Referans Silme İşlemi Başarı İle Gerçekleştirildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting testimonial with id: {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult Createtestimonial(CreateTestimonialDto createTestimonialDto)
        {
            try
            {
                var testimonial = new Testimonial()
                {
                    Comment = createTestimonialDto.Comment,
                    ImageUrl = createTestimonialDto.ImageUrl,
                    Name = createTestimonialDto.Name,
                    Title = createTestimonialDto.Title
                };

                _testimonialService.TAdd(testimonial);
                return Ok("Referans Ekleme İşlemi Başarı İle Gerçekleştirildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new testimonial");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPut]
        public IActionResult Updatetestimonial(UpdateTestimonialDto updateTestimonialDto)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating testimonial");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
