using choapi.DAL;
using choapi.DTOs;
using choapi.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace choapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserDAL _userDAL;

        private readonly ILogger<BookingController> _logger;

        public UserController(ILogger<BookingController> logger, IUserDAL userDAL)
        {
            _logger = logger;
            _userDAL = userDAL;
        }

        [HttpPost("update"), Authorize()]
        public ActionResult<UserUpdateResponse> Update(LocationDTO request)
        {
            var response = new UserUpdateResponse();
            try
            {
                var user = _userDAL.GetUser(request.Id);

                if (user != null)
                {
                    user.Longitude = request.Longitude;
                    user.Latitude = request.Latitude;

                    var result = _userDAL.Update(user);

                    response.User.User_Id = result.User_Id;
                    response.User.Username = result.Username;
                    response.User.Email = user.Email;
                    response.User.Phone = user.Phone;
                    response.User.Role_Id = result.Role_Id;
                    response.User.Is_Active = result.Is_Active;
                    response.User.Display_Name = result.Display_Name;
                    response.User.Photo_Url = result.Photo_Url;
                    response.User.Latitude = result.Latitude;
                    response.User.Longitude = result.Longitude;

                    response.Message = "Successfully updated.";
                    return Ok(response);
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = $"No found User id: { request.Id }";

                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Failed";

                return BadRequest(response);
            }
        }
    }
}
