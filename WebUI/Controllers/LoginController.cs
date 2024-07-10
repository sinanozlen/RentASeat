using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using WebUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using DataAccessLayer.Concrete;
using DtoLayer.LoginDtos;
using System.IdentityModel.Tokens.Jwt;
using MimeKit;
using MailKit.Security;
using EntitityLayer.Entities;

namespace WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public LoginController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["IsAuthenticated"] = User.Identity.IsAuthenticated;
            ViewData["Name"] = User.Identity.Name;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateLoginDto createLoginDto)
        {
            using var _context = new RenASeatContext();
            var user = await _context.AppUsers.SingleOrDefaultAsync(u => u.Username == createLoginDto.Username);

            if (user != null && VerifyPassword(createLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                var client = _httpClientFactory.CreateClient();
                var content = new StringContent(JsonSerializer.Serialize(createLoginDto), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://api.rentaseat.com.tr/api/Logins", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var tokenModel = JsonSerializer.Deserialize<JwtResponseModel>(jsonData, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                    if (tokenModel != null)
                    {
                        var handler = new JwtSecurityTokenHandler();
                        var token = handler.ReadJwtToken(tokenModel.Token);
                        var claims = token.Claims.ToList();

                        if (tokenModel.Token != null)
                        {
                            // Kullanıcı adı talebini manuel olarak ekleyin
                            claims.Add(new Claim(ClaimTypes.Name, user.Name));
                            claims.Add(new Claim("carbooktoken", tokenModel.Token));

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var authProps = new AuthenticationProperties
                            {
                                ExpiresUtc = tokenModel.ExpireDate,
                                IsPersistent = true
                            };

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);

                            TempData["LoginSuccess"] = $"Hos Geldiniz, {user.Name}";

                            if (claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin"))
                            {
                                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                            }
                            else
                            {
                                return RedirectToAction("Index", "Default");
                            }
                        }
                    }
                }
            }

            TempData["LoginFailed"] = "Kullanıcı adı veya şifrenizi kontrol ediniz";
            return View();
        }





        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Default");
        }

        private (string hash, string salt) HashPassword(string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var salt = Convert.ToBase64String(hmac.Key);
                var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
                return (hash, salt);
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(Convert.FromBase64String(storedSalt)))
            {
                var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword)));
                return computedHash == storedHash;
            }
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                TempData["ForgetPasswordError"] = "Lütfen bir e-posta adresi girin.";
                return View();
            }

            using var _context = new RenASeatContext();
            var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                TempData["ForgetPasswordError"] = "Bu e-posta adresine sahip bir kullanıcı bulunamadı.";
                return View();
            }

            //Yeni bir reset token oluşturduk
            var resetToken = Guid.NewGuid().ToString();

            // Save the reset token to the user's ResetPasswordToken field
            user.ResetPasswordToken = resetToken;
            _context.Update(user);
            await _context.SaveChangesAsync();

            //E -posta gönderme işlemi
            await SendPasswordResetEmail(user, resetToken);

            TempData["ForgetPasswordSuccess"] = "Şifre sıfırlama bağlantısı e-posta adresinize gönderildi. Bu bağlantıyı kullanarak şifre sıfırlama işleminizi gerçekleştirebilirsiniz.";
            return RedirectToAction("ResetPasswordConfirmation");
        }


        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        private async Task SendPasswordResetEmail(AppUser user, string token)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");
            var server = smtpSettings["Server"];
            var port = int.Parse(smtpSettings["Port"]);
            var username = smtpSettings["Username"];
            var password = smtpSettings["Password"];

            var resetLink = Url.Action("ResetPassword", "Login", new { userId = user.AppUserId, token }, Request.Scheme);
            var body = $@"
    <html>
    <body>
    <p>Merhaba {user.Name},</p>
    <p>Şifrenizi mi unuttunuz?<br/>
    Hesabınızın şifresini sıfırlamak için bir talep aldık.</p>
    <p>Şifrenizi sıfırlamak için aşağıdaki butona tıklayabilirsiniz:</p>
    <a href='{resetLink}' style='padding: 10px 20px; background-color: #4CAF50; color: white; text-decoration: none;'>Şifreyi Sıfırla</a>
    <p>Veya aşağıdaki URL'yi tarayıcınıza kopyalayın ve yapıştırın:<br/>
    {resetLink}</p>
    <p>Teşekkürler,<br/>
    RentASeat</p>
    </body>
    </html>";

            using (var message = new MimeMessage())
            {
                message.From.Add(new MailboxAddress("RentASeat", username));
                message.To.Add(new MailboxAddress(user.Name, user.Email));
                message.Subject = "Şifre Sıfırlama";

                message.Body = new TextPart("html")
                {
                    Text = body
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync(server, port, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(username, password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
        }



        [HttpGet]
        public async Task<IActionResult> ResetPassword(int userId, string token)
        {
            if (userId <= 0 || string.IsNullOrEmpty(token))
            {
                TempData["ResetPasswordError"] = "Geçersiz şifre sıfırlama isteği.";
                return RedirectToAction("ResetPasswordConfirmation");
            }

            using var _context = new RenASeatContext();
            var user = await _context.AppUsers.FindAsync(userId);

            if (user == null || user.ResetPasswordToken != token)
            {
                TempData["ResetPasswordError"] = "Geçersiz şifre sıfırlama isteği.";
                return RedirectToAction("ResetPasswordConfirmation");
            }

            //Token doğruysa şifre sıfırlama sayfasını gösterdik
            return View(new ResetPasswordViewModel { UserId = userId, Token = token });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using var _context = new RenASeatContext();
            var user = await _context.AppUsers.FindAsync(model.UserId);

            if (user == null || user.ResetPasswordToken != model.Token)
            {
                TempData["ResetPasswordError"] = "Geçersiz şifre sıfırlama isteği.";
                return RedirectToAction("ResetPasswordConfirmation");
            }

            //Parolayı hashleyip kaydettik
            var (hash, salt) = HashPassword(model.NewPassword);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            //Parola sıfırlandıktan sonra tokenı sıfırladık
            user.ResetPasswordToken = null;

            _context.Update(user);
            await _context.SaveChangesAsync();

            TempData["ResetPasswordSuccess"] = "Şifreniz başarıyla sıfırlandı.";

            return RedirectToAction("Index", "Login");
        }
    }
}
