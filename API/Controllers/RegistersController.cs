using BusinessLayer.Abstract;
using DtoLayer.AppUserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private readonly IAppUserService _appUserService;

        public RegistersController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateAppUserDto createAppUserDto)
        {
            var result = await _appUserService.TCreateAppUser(createAppUserDto);
            return Ok("Kullanıcı Başarılı bir şekilde Oluşturuldu.");
        }
    }
}
