using DataAccessLayer.Concrete;
using DataAccessLayer.Enums;
using DtoLayer.RegisterDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace WebUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public RegisterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult CreateAppUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAppUser(CreateRegisterDto model)
        {
            using var _context = new RenASeatContext();
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Şifreler eşleşmiyor.");
                    return View("CreateRegister", model);
                }

                // Hash the password
                var passwordHash = HashPassword(model.Password);

                // Create AppUser object
                var newUser = new AppUser
                {
                    Username = model.Username,
                    PasswordHash = passwordHash,
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    AppRoleId = (int)RolesType.User,
                    OAuthId = "0",
                    OAuthProvider = "Sistemden Kayıt"

                };

                _context.AppUsers.Add(newUser);
                _context.SaveChanges();

                return RedirectToAction("Index", "Login");
            }

            return View("CreateRegister", model);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
