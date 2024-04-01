using choapi.DAL;
using choapi.DTOs;
using choapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace choapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //Server=localhost\SQLEXPRESS01;Database=master;Trusted_Connection=True;
        private readonly IUserDAL _userDAL;

        public static User _user = new User();

        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration, IUserDAL userDAL)
        {
            _configuration = configuration;
            _userDAL = userDAL;
        }

        [HttpPost("register")]
        public ActionResult<User> Register(UserDto request)
        {
            try
            {
                string passwordinputed = request.Password;

                var existUser = _userDAL.GetUserByUsername(request.Username);

                if (existUser != null)
                {
                    return BadRequest("Username/email is already existed.");
                }

                string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                var user = new User
                {
                    Username = request.Username,
                    Password_Hash = passwordHash,
                    Role = request.Role,
                    Is_Active = true
                };

                var resultUser = _userDAL.Add(user);

                return Ok(resultUser);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("login")]
        public ActionResult<User> Login(UserDto request)
        {
            var user = _userDAL.GetUserByUsername(request.Username);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password_Hash))
            {
                return BadRequest("Incorrect password.");
            }

            string token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
