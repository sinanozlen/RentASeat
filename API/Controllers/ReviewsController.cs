using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

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
    }
}
