using DtoLayer.AboutDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/About")]
    public class AboutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AboutController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin")] // Sadece Admin rolündeki kullanıcılar erişebilir
        [Route("Index")]
        public async Task< IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden"); // Forbidden Controller farklı area'da ise area belirtilmeli
            }

            ViewBag.UserName = User.Identity.Name;

            var token = User.Claims.FirstOrDefault(x => x.Type == "carbooktoken")?.Value;

            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseMessage = await client.GetAsync("https://localhost:7250/api/Abouts");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);
                    return View(values);
                }
                else
                {
                    ModelState.AddModelError("", "Hakkımızda Alanı getirme başarısız oldu");
                }
            }
            else
            {
                ModelState.AddModelError("", "Yetkilendirme token'ı bulunamadı");
            }

            return View();
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin")] // Sadece Admin rolündeki kullanıcılar erişebilir
        [HttpGet]
        [Route("CreateAbout")]
        public IActionResult CreateAbout()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden");
            }
            return View();
        }

        [AllowAnonymous]
        [Authorize(Roles = "Admin")] // Sadece Admin rolündeki kullanıcılar erişebilir
        [HttpPost]
        [Route("CreateAbout")]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
           if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden"); // Forbidden Controller farklı area'da ise area belirtilmeli
            }
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createAboutDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7250/api/Abouts", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [AllowAnonymous]
        [Authorize(Roles = "Admin")] // Sadece Admin rolündeki kullanıcılar erişebilir
        [HttpDelete]
        [Route("RemoveAbout/{id}")]
        public async Task<IActionResult> RemoveAbout(int id)
        {
           if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden"); // Forbidden Controller farklı area'da ise area belirtilmeli
            }
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7250/api/Abouts/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin")] // Sadece Admin rolündeki kullanıcılar erişebilir
        [HttpGet]
        [Route("UpdateAbout/{id}")]
        public async Task<IActionResult> UpdateAbout(int id)
        {
           if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden"); // Forbidden Controller farklı area'da ise area belirtilmeli
            }
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7250/api/Abouts/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateAboutDto>(jsonData);
                return View(value);
            }
            return View();
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin")] // Sadece Admin rolündeki kullanıcılar erişebilir
        [HttpPost]
        [Route("UpdateAbout/{id}")]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
           if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden"); // Forbidden Controller farklı area'da ise area belirtilmeli
            }
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateAboutDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync($"https://localhost:7250/api/Abouts/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}