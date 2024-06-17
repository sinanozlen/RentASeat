using BusinessLayer.Abstract;
using DtoLayer.FeatureDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureService _featureService;

        public FeaturesController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [HttpGet]
        public IActionResult FeatureList()
        {
            var features = _featureService.TGetListAll();
            return Ok(features);
        }

        [HttpGet("{id}")]
        public IActionResult GetFeature(int id)
        {
            var feature = _featureService.TGetbyID(id);
            if (feature == null)
                return NotFound("Özellik bulunamadı");
            return Ok(feature);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFeature(int id)
        {
            var feature = _featureService.TGetbyID(id);
            if (feature == null)
                return NotFound("Özellik bulunamadı");

            _featureService.TDelete(feature);
            return Ok("Özellik silme işlemi başarı ile gerçekleşti");
        }

        [HttpPost]
        public IActionResult CreateFeature(CreateFeatureDto createFeatureDto)
        {
            var feature = new Feature
            {
                Name = createFeatureDto.Name
            };

            _featureService.TAdd(feature);
            return Ok("Özellik ekleme işlemi başarı ile gerçekleşti");
        }

        [HttpPut]
        public IActionResult UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            var feature = _featureService.TGetbyID(updateFeatureDto.FeatureID);
            if (feature == null)
                return NotFound("Özellik bulunamadı");

            feature.Name = updateFeatureDto.Name;
            _featureService.TUpdate(feature);
            return Ok("Özellik güncelleme işlemi başarı ile gerçekleşti");
        }
    }
}
