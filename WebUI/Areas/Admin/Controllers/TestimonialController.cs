using DtoLayer.TestimonialDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Testimonial")]
    public class TestimonialController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TestimonialController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [Route("Index")]
        public async Task< IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var client = _httpClientFactory.CreateClient();
            var responseMessage=await client.GetAsync("https://api.rentaseat.com.tr/api/Testimonials");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var testimonials = JsonConvert.DeserializeObject<List<ResultTestimonialDto>>(jsonData);
                return View(testimonials);
            }
            return View();
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("CreateTestimonial")]
        public IActionResult CreateTestimonial()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden");
            }
            return View();
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("CreateTestimonial")]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto createTestimonialDto)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createTestimonialDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://api.rentaseat.com.tr/api/Testimonials", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("RemoveTestimonial/{id}")]
        public async Task<IActionResult> RemoveTestimonial(int id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://api.rentaseat.com.tr/api/Testimonials/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("UpdateTestimonial/{id}")]
        public async Task<IActionResult> UpdateTestimonial(int id)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://api.rentaseat.com.tr/api/Testimonials/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var testimonial = JsonConvert.DeserializeObject<UpdateTestimonialDto>(jsonData);
                return View(testimonial);
            }
            return View();
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("UpdateTestimonial/{id}")]
        public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDto updateTestimonialDto)
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Forbidden");
            }
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateTestimonialDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync($"https://api.rentaseat.com.tr/api/Testimonials/", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
