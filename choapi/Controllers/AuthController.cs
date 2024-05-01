using choapi.DAL;
using choapi.DTOs;
using choapi.Helper;
using choapi.Messages;
using choapi.Models;
using Microsoft.AspNetCore.Identity;
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

        private readonly IConfiguration _configuration;

        private const string _fromUsers = "users";
        public AuthController(IConfiguration configuration, IUserDAL userDAL)
        {
            _configuration = configuration;
            _userDAL = userDAL;
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterReponse>> Register(UserDTO request)
        {
            var response = new RegisterReponse();
            try
            {
                if (string.IsNullOrEmpty(request.Password))
                {
                    response.Message = "Password is required";
                    response.Status = "Failed";
                    return BadRequest(response);
                }

                if (string.IsNullOrEmpty(request.Username))
                {
                    response.Message = "Username is required";
                    response.Status = "Failed";
                    return BadRequest(response);
                }

                string passwordinputed = request.Password;

                var existUser = _userDAL.GetUserByUsername(request.Username);

                if (existUser != null)
                {
                    response.Message = "Username is already existed";
                    response.Status = "Failed";
                    return BadRequest(response);
                }

                string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                var user = new Users
                {
                    Username = request.Username,
                    Password_Hash = passwordHash,
                    Role_Id = request.Role_Id,
                    Email = request.Email,
                    Phone = request.Phone,
                    Display_Name = request.Display_Name,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                    Is_Active = true
                };

                var userResult = _userDAL.Add(user);

                var path = await UploadHelper.SaveFile(request.File, userResult.User_Id, _fromUsers);

                if (!path.Contains("Error:"))
                {
                    if (!string.IsNullOrEmpty(path))
                    {
                        var host = Request.Host;
                        var scheme = Request.Scheme;

                        user.Photo_Url = $"{scheme}://{host}/{path}";
                        var updateResult = _userDAL.Update(userResult);
                    }

                    response.User.User_Id = userResult.User_Id;
                    response.User.Username = userResult.Username;
                    response.User.Email = user.Email;
                    response.User.Phone = user.Phone;
                    response.User.Role_Id = userResult.Role_Id;
                    response.User.Is_Active = userResult.Is_Active;
                    response.User.Display_Name = userResult.Display_Name;
                    response.User.Photo_Url = userResult.Photo_Url;
                    response.User.Latitude = userResult.Latitude;
                    response.User.Longitude = userResult.Longitude;

                    response.Message = "Successfully registered.";

                    return Ok(response);
                }
                else
                {
                    _userDAL.Delete(userResult);

                    response.User.User_Id = 0;
                    response.User.Username = request.Username;
                    response.User.Email = request.Email;
                    response.User.Phone = request.Phone;
                    response.User.Role_Id = request.Role_Id;
                    response.User.Display_Name = request.Display_Name;
                    response.User.Latitude = request.Latitude;
                    response.User.Longitude = request.Longitude;

                    response.Status = "Failed";
                    response.Message = path;
                    return BadRequest(response);
                }                
            }
            catch(Exception ex)
            {
                response.User.User_Id = 0;
                response.User.Username = request.Username;
                response.User.Email = request.Email;
                response.User.Phone = request.Phone;
                response.User.Role_Id = request.Role_Id;
                response.User.Display_Name = request.Display_Name;
                response.User.Latitude = request.Latitude;
                response.User.Longitude = request.Longitude;

                response.Message = ex.Message;
                response.Status = "Failed";

                return BadRequest(response);
            }            
        }

        [HttpPost("login")]
        public ActionResult<LoginResponse> Login(LoginDTO request)
        {
            var response = new LoginResponse();
            try
            {
                var user = _userDAL.GetUserByUsername(request.Username);
                if (user == null)
                {
                    response.Message = "Username not found.";
                    response.Status = "Failed";

                    return BadRequest(response);
                }

                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password_Hash))
                {
                    response.Message = "Incorrect password.";
                    response.Status = "Failed";

                    return BadRequest(response);
                }                

                response.User.User_Id = user.User_Id;
                response.User.Username = user.Username;
                response.User.Email = user.Email;
                response.User.Phone = user.Phone;
                response.User.Role_Id = user.Role_Id;
                response.User.Is_Active = user.Is_Active;
                response.User.Display_Name = user.Display_Name;
                response.User.Photo_Url = user.Photo_Url;
                response.User.Latitude = user.Latitude;
                response.User.Longitude = user.Longitude;

                response.Message = "Successfully login.";

                string token = CreateToken(user);

                response.Token = token;

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Failed";

                return BadRequest(response);
            }            
        }

        private string CreateToken(Users user)
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
