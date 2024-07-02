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

namespace WebUI.Controllers
{

    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

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
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(createLoginDto), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7060/api/Login", content);
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

            return View();
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

            var _context = new RenASeatContext();

            if (loginProvider == "Google")
            {
                var user = await _context.AppUsers.SingleOrDefaultAsync(u => u.OAuthProvider == loginProvider && u.OAuthId == providerKey);
                if (user == null)
                {
                    user = new AppUser
                    {
                        OAuthProvider = loginProvider,
                        OAuthId = providerKey,
                        Email = email,
                        Name = name,
                        Surname = surname,
                        AppRoleId = (int)RolesType.User,
                        Username = email,
                        PasswordHash = HashPassword(providerKey)
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
                    user = new AppUser
                    {
                        OAuthProvider = loginProvider,
                        OAuthId = providerKey,
                        Email = username,
                        Name = name,
                        Surname = surname,
                        AppRoleId = (int)RolesType.User,
                        Username = username,
                        PasswordHash = HashPassword(providerKey)
                    };

                    _context.AppUsers.Add(user);
                    await _context.SaveChangesAsync();
                }
                return LocalRedirect(returnUrl);
            }

            // Diğer durumlar için hata yönetimi veya yönlendirme yapabilirsiniz
            return BadRequest();
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Index));
        }
        private string HashPassword(string providerKey)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(providerKey));
                return Convert.ToBase64String(hash);
            }
        }
    }

}
