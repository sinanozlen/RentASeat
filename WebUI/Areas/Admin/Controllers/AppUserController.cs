using DtoLayer.AppUserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AppUser")]
     // Sadece "Admin" rolündeki kullanıcılar erişebilir
    public class AppUserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AppUserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden");
            }
            ViewBag.UserName = User.Identity.Name;

            var token = User.Claims.FirstOrDefault(x => x.Type == "carbooktoken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.GetAsync("https://localhost:7250/api/AppUsers");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = await responseMessage.Content.ReadAsStringAsync();
                    var appUsers = JsonConvert.DeserializeObject<List<AppUserByRoleNameDto>>(responseData);
                    return View(appUsers);
                }
                else
                {
                    ModelState.AddModelError("", "Hakkımızda Alanı getirme başarısız oldu");
                }
            }
                return View();
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [Route("ChangeUserRole")]
        [HttpPost]
        public async Task<IActionResult> ChangeUserRole(string userName, int newRoleId)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden");
            }
            try
            {
                var client = _httpClientFactory.CreateClient();
                var content = new StringContent("", Encoding.UTF8, "application/json");
                var responseMessage = await client.PostAsync($"https://localhost:7250/api/AppUsers/ChangeUserRole?userName={userName}&newRoleId={newRoleId}", content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();

                    if (responseContent.Contains("\"success\":true"))
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        var errorResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
                        var errorMessage = errorResponse?.message ?? "Rol değiştirme işlemi başarısız oldu.";
                        return Json(new { success = false, message = errorMessage });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Rol değiştirme işlemi başarısız oldu." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}