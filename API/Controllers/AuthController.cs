using Microsoft.AspNetCore.Mvc;
using YourNamespace.Models;
using YourNamespace.Helpers;
using YourNamespace.Services;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using YourNamespace.DTO;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtAuthenticationManager _jwtAuthenticationManager;

        public AuthController(UserService userService, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _userService = userService;
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpGet("users")]
        [Authorize(Roles = Roles.Admin)] // Sadece Admin rolüne sahip kullanıcılar erişebilir
        public IActionResult GetUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            var user = _userService.Authenticate(login.Username, login.Password);
            if (user == null)
                return Unauthorized(new { message = "Username or password is incorrect" });

            var token = _jwtAuthenticationManager.Authenticate(user);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (_userService.GetUserByUsername(user.Username) != null)
                return Conflict(new { message = "Username already exists" });

            _userService.AddUser(user);
            return Ok(new { message = "User registered successfully" });
        }

        [HttpDelete("remove/{username}")]
        public IActionResult RemoveUser(string username)
        {
            var user = _userService.GetUserByUsername(username);
            if (user == null)
                return NotFound(new { message = "User not found" });

            _userService.RemoveUser(username);
            return Ok(new { message = "User removed successfully" });
        }
    }
}
