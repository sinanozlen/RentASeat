using BusinessLayer.Abstract;
using DataAccessLayer.Validators.ReviewValidators;
using DtoLayer.AboutDtos;
using DtoLayer.ReviewDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly ILogger<ReviewsController> _logger;

        public ReviewsController(IReviewService reviewService, ILogger<ReviewsController> logger)
        {
            _reviewService = reviewService;
            _logger = logger;
        }

        [HttpGet("ReviewsListByCarID")]
        public IActionResult ReviewsListByCarID(int id)
        {
            try
            {
                var reviews = _reviewService.TGetReviewsByCarId(id);

                if (reviews == null || !reviews.Any())
                {
                    _logger.LogError($"Araba ID'si {id} olan için değerlendirme bulunamadı.");
                    return NotFound("İlgili araca ait değerlendirme bulunamadı");
                }

                return Ok(reviews);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Araba ID'si {id} olan değerlendirmeleri alınırken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }
        [HttpPost]
        public async  Task<IActionResult>CreateReview(CreateReviewDto createReviewDto)
        {
            CreateReviewValidator validator = new CreateReviewValidator();
            var validationResult = validator.Validate(createReviewDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                Review review = new Review
                {
                    CarID = createReviewDto.CarID,
                    Comment = createReviewDto.Comment,
                    CustomerName = createReviewDto.CustomerName,
                    CustomerImage = createReviewDto.CustomerImage,
                    RatingValue = createReviewDto.RatingValue,
                    ReviewDate =DateTime.Parse(DateTime.Now.ToShortDateString())

                  
                };
                _reviewService.TAdd(review);
                return StatusCode(StatusCodes.Status201Created, "Değerlendirme başarı ile oluşturuldu");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Değerlendirme oluşturulurken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateReview(UpdateReviewDto updateReviewDto)
        {
            UpdateReviewValidator validator = new UpdateReviewValidator();
            var validationResult = validator.Validate(updateReviewDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            try
            {
                var review =  _reviewService.TGetbyID(updateReviewDto.ReviewId);
                if (review == null)
                {
                    return NotFound("Değerlendirme bulunamadı");
                }

                review.CarID = updateReviewDto.CarID;
                review.Comment = updateReviewDto.Comment;
                review.CustomerName = updateReviewDto.CustomerName;
                review.CustomerImage = updateReviewDto.CustomerImage;
                review.RatingValue = updateReviewDto.RatingValue;
                review.ReviewDate = DateTime.Parse(DateTime.Now.ToShortDateString());

                _reviewService.TUpdate(review);
                return Ok("Değerlendirme başarı ile güncellendi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Değerlendirme güncellenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

    }
}
