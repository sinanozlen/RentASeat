using BusinessLayer.Abstract;
using DtoLayer.FeatureDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Http;
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
            return Ok(feature);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteFeature(int id)
        {
            var values = _featureService.TGetbyID(id);
            _featureService.TDelete(values);
            return Ok("Özellik Silme işlemi Başarı ile Gerçekleşti");
        }
        [HttpPost]
        public IActionResult CreateFeature(CreateFeatureDto createFeatureDto)
        {
            Feature feature = new Feature()
            {
                Name = createFeatureDto.Name,
            };
            _featureService.TAdd(feature);
            return Ok("Özellik Ekleme işlemi Başarı ile Gerçekleşti");
        }
        [HttpPut]
        public IActionResult UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            Feature feature = new Feature()
            {
                FeatureID = updateFeatureDto.FeatureID,
                Name = updateFeatureDto.Name,
            };
            _featureService.TUpdate(feature);
            return Ok("Özellik Güncelleme işlemi Başarı ile Gerçekleşti");
        }
    }
}
