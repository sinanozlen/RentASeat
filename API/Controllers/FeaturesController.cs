using BusinessLayer.Abstract;
using DtoLayer.FeatureDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureService _featureService;
        private readonly ILogger<FeaturesController> _logger;

        public FeaturesController(IFeatureService featureService, ILogger<FeaturesController> logger)
        {
            _featureService = featureService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult FeatureList()
        {
            try
            {
                var features = _featureService.TGetListAll();
                return Ok(features);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Özellikler getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetFeature(int id)
        {
            try
            {
                var feature = _featureService.TGetbyID(id);
                if (feature == null)
                {
                    return NotFound("Özellik bulunamadı");
                }
                return Ok(feature);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {id} olan özellik getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFeature(int id)
        {
            try
            {
                var feature = _featureService.TGetbyID(id);
                if (feature == null)
                {
                    return NotFound("Özellik bulunamadı");
                }

                _featureService.TDelete(feature);
                return Ok("Özellik silme işlemi başarı ile gerçekleştirildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {id} olan özellik silinirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpPost]
        public IActionResult CreateFeature(CreateFeatureDto createFeatureDto)
        {
            try
            {
                Feature feature = new Feature
                {
                    Name = createFeatureDto.Name,
                };

                _featureService.TAdd(feature);
                return Ok("Özellik ekleme işlemi başarı ile gerçekleştirildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Özellik eklenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }

        [HttpPut]
        public IActionResult UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            try
            {
                Feature feature = new Feature
                {
                    FeatureID = updateFeatureDto.FeatureID,
                    Name = updateFeatureDto.Name,
                };

                _featureService.TUpdate(feature);
                return Ok("Özellik güncelleme işlemi başarı ile gerçekleştirildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID {updateFeatureDto.FeatureID} olan özellik güncellenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası");
            }
        }
    }
}
