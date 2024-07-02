using DataAccessLayer.Concrete;
using DataAccessLayer.Enums;
using DtoLayer.LoginDtos;
using EntitityLayer.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using WebUI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography;

namespace WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly RenASeatContext _context;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
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
                var response = await client.PostAsync("https://localhost:7250/api/Logins", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var tokenModel = JsonSerializer.Deserialize<JwtResponseModel>(jsonData, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                    if (tokenModel != null)
                    {
                        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                        var token = handler.ReadJwtToken(tokenModel.Token);
                        var claims = token.Claims.ToList();

                        if (tokenModel.Token != null)
                        {
                            claims.Add(new Claim("carbooktoken", tokenModel.Token));
                            var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                            var authProps = new AuthenticationProperties
                            {
                                ExpiresUtc = tokenModel.ExpireDate,
                                IsPersistent = true
                            };

                            await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);
                            return RedirectToAction("Index", "Default");
                        }
                    }
                }
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View();
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

        private bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            using (var hmac = new HMACSHA512(Convert.FromBase64String(storedSalt)))
            {
                var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword)));
                return computedHash == storedHash;
            }
        }

        public async Task<IActionResult> LoginWithGoogle()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action(nameof(ExternalLoginCallback)) };
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, properties);
            return new EmptyResult();
        }

        public IActionResult LoginWithTwitter(string returnUrl = "/")
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Login", new { ReturnUrl = returnUrl });
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, "Twitter");
        }

        public IActionResult LoginWithInstagram(string returnUrl = "/")
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Login", new { ReturnUrl = returnUrl });
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, "Instagram");
        }

        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = "/")
        {
            using var _context = new RenASeatContext();
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (result?.Principal == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var claims = result.Principal.Identities.FirstOrDefault()?.Claims.Select(claim => new
            {
                claim.Type,
                claim.Value
            }).ToList();

            var email = claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            var name = claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.GivenName)?.Value;
            var surname = claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Surname)?.Value;
            var providerKey = claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            var loginProvider = result.Properties.Items[".AuthScheme"]; // OAuth sağlayıcısını belirlemek için

            if (providerKey == null || loginProvider == null)
            {
                return BadRequest();
            }

            if (loginProvider == "Google")
            {
                var user = await _context.AppUsers.SingleOrDefaultAsync(u => u.OAuthProvider == loginProvider && u.OAuthId == providerKey);
                if (user == null)
                {
                    var (passwordHash, passwordSalt) = HashPassword(providerKey);
                    user = new AppUser
                    {
                        OAuthProvider = loginProvider,
                        OAuthId = providerKey,
                        Email = email,
                        Name = name,
                        Surname = surname,
                        AppRoleId = (int)RolesType.User,
                        Username = email,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt
                    };

                    _context.AppUsers.Add(user);
                    await _context.SaveChangesAsync();
                }
                return LocalRedirect(returnUrl);
            }

            if (loginProvider == "Twitter")
            {
                var user = await _context.AppUsers.SingleOrDefaultAsync(u => u.OAuthProvider == loginProvider && u.OAuthId == providerKey);
                if (user == null)
                {
                    var username = result.Principal.Identity.Name ?? "TwitterUser"; // Principal'den kullanıcı adını al, yoksa varsayılan olarak "TwitterUser" kullan
                    // Twitter'dan gelen kullanıcı adı
                    var twitterScreenName = claims?.FirstOrDefault(claim => claim.Type == "urn:twitter:screen_name")?.Value;
                    // Twitter'dan gelen ad ve soyad ayrı olarak dönmediği için displayName'den ad ve soyadı ayırabilirsiniz
                    var displayName = claims?.FirstOrDefault(claim => claim.Type == "urn:twitter:name")?.Value;
                    var nameParts = displayName?.Split(' ');
                    name = nameParts?.FirstOrDefault() ?? twitterScreenName; // Kullanıcı adını varsayılan isim olarak kullanabilirsiniz
                    surname = nameParts?.Skip(1).FirstOrDefault() ?? string.Empty; // Soyadı boş olarak ayarlayabilirsiniz

                    var (passwordHash, passwordSalt) = HashPassword(providerKey);
                    user = new AppUser
                    {
                        OAuthProvider = loginProvider,
                        OAuthId = providerKey,
                        Email = username,
                        Name = name,
                        Surname = surname,
                        AppRoleId = (int)RolesType.User,
                        Username = username,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt
                    };

                    _context.AppUsers.Add(user);
                    await _context.SaveChangesAsync();
                }
                return LocalRedirect(returnUrl);
            }

            if (loginProvider == "Instagram")
            {
                var user = await _context.AppUsers.SingleOrDefaultAsync(u => u.OAuthProvider == loginProvider && u.OAuthId == providerKey);
                if (user == null)
                {
                    var username = result.Principal.Identity.Name ?? "InstagramUser"; // Principal'den kullanıcı adını al, yoksa varsayılan olarak "InstagramUser" kullan
                    var (passwordHash, passwordSalt) = HashPassword(providerKey);
                    user = new AppUser
                    {
                        OAuthProvider = loginProvider,
                        OAuthId = providerKey,
                        Email = username,
                        Name = name,
                        Surname = surname,
                        AppRoleId = (int)RolesType.User,
                        Username = username,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt
                    };

                    _context.AppUsers.Add(user);
                    await _context.SaveChangesAsync();
                }
                return LocalRedirect(returnUrl);
            }

            return BadRequest();
        }
    }
}
