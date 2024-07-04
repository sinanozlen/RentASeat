using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DataAccessLayer.Concrete;
using DataAccessLayer.Enums;
using DtoLayer.RegisterDtos;
using EntitityLayer.Entities;

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
                    return View(model);
                }

                // Check if the username or email is already registered
                var existingUser = _context.AppUsers
                    .FirstOrDefault(u => u.Username == model.Username || u.Email == model.Email);

                if (existingUser != null)
                {
                    // Show SweetAlert notification for existing user
                    TempData["SweetAlertMessage"] = "error";
                    TempData["SweetAlertTitle"] = "Hata!";
                    TempData["SweetAlertText"] = "Kullanıcı adı veya e-posta adresi sistemde zaten kayıtlı.";
                    return View(model);
                }

                // Hash the password and generate salt
                var (passwordHash, passwordSalt) = HashPassword(model.Password);

                // Create AppUser object
                var newUser = new AppUser
                {
                    Username = model.Username,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
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

            return View(model);
        }

        private (string hash, string salt) HashPassword(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                var salt = Convert.ToBase64String(hmac.Key);
                var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
                return (hash, salt);
            }
        }
    }
}
