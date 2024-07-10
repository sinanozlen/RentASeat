using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly IAppUserService _appUserService;

        public AppUsersController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [HttpGet]
        public IActionResult GetAllAppUsersWithRoles()
        {
            var values = _appUserService.TGetAllAppUsersWithRoles();
            return Ok(values);
        }

        [HttpPost("ChangeUserRole")]
        public async Task<IActionResult> ChangeUserRole(string userName, int newRoleId)
        {
            try
            {
                var updateSuccessful = await _appUserService.TUpdateAppUserRole(userName, newRoleId);

                if (updateSuccessful)
                {
                    return StatusCode(204); // 204 No Content 
                }
                else
                {
                    return BadRequest("Kullanıcı adı veya rol geçersiz."); // 400 Bad Request
                }
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Rol güncelleme işlemi sırasında bir hata oluştu.");
            }
        }

    }
}