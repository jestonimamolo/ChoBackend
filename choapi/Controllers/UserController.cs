using choapi.DAL;
using choapi.DTOs;
using choapi.Helper;
using choapi.Messages;
using choapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace choapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserDAL _userDAL;

        private readonly ILogger<BookingController> _logger;
        private const string _fromUsers = "users";

        public UserController(ILogger<BookingController> logger, IUserDAL userDAL)
        {
            _logger = logger;
            _userDAL = userDAL;
        }

        [HttpPost("location/update"), Authorize()]
        public ActionResult<UserUpdateResponse> UpdateLocation(LocationDTO request)
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

        [HttpPost("profile/update"), Authorize()]
        public ActionResult<UserUpdateResponse> UpdateProfile(UserProfileDTO request)
        {
            var response = new UserUpdateResponse();
            try
            {
                var user = _userDAL.GetUser(request.User_id);

                if (user != null)
                {
                    user.Email = request.Email;
                    user.Phone = request.Phone;
                    user.Role_Id = request.Role_Id;
                    user.Display_Name = request.Display_Name;
                    user.Latitude = request.Latitude;
                    user.Longitude = request.Longitude;

                    var updateResult = _userDAL.Update(user);

                    response.User.User_Id = updateResult.User_Id;
                    response.User.Username = updateResult.Username;
                    response.User.Email = updateResult.Email;
                    response.User.Phone = updateResult.Phone;
                    response.User.Role_Id = updateResult.Role_Id;
                    response.User.Is_Active = updateResult.Is_Active;
                    response.User.Display_Name = updateResult.Display_Name;
                    response.User.Photo_Url = updateResult.Photo_Url;
                    response.User.Latitude = updateResult.Latitude;
                    response.User.Longitude = updateResult.Longitude;

                    response.Message = "Successfully updated.";
                    return Ok(response);
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = $"No found User id: {request.User_id}";

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

        [HttpPost("update"), Authorize()]
        public async Task<ActionResult<UserUpdateResponse>> Update(UserUpdateDTO request)
        {
            var response = new UserUpdateResponse();
            try
            {
                var user = _userDAL.GetUser(request.User_id);

                if (user != null)
                {
                    string deleteFileResult = string.Empty;
                    if (!string.IsNullOrEmpty(user.Photo_Url))
                    {
                        deleteFileResult = UploadHelper.DeleteFile(user.Photo_Url);
                    }

                    var path = await UploadHelper.SaveFile(request.File, user.User_Id, _fromUsers);

                    if (!path.Contains("Error:"))
                    {
                        if (!string.IsNullOrEmpty(path))
                        {
                            var host = Request.Host;
                            var scheme = Request.Scheme;

                            user.Photo_Url = $"{scheme}://{host}/{path}";
                        }
                        user.Email = request.Email;
                        user.Phone = request.Phone;
                        user.Role_Id = request.Role_Id;
                        user.Display_Name = request.Display_Name;
                        user.Latitude = request.Latitude;
                        user.Longitude = request.Longitude;

                        var updateResult = _userDAL.Update(user);

                        response.User.User_Id = updateResult.User_Id;
                        response.User.Username = updateResult.Username;
                        response.User.Email = updateResult.Email;
                        response.User.Phone = updateResult.Phone;
                        response.User.Role_Id = updateResult.Role_Id;
                        response.User.Is_Active = updateResult.Is_Active;
                        response.User.Display_Name = updateResult.Display_Name;
                        response.User.Photo_Url = updateResult.Photo_Url;
                        response.User.Latitude = updateResult.Latitude;
                        response.User.Longitude = updateResult.Longitude;

                        response.Message = "Successfully updated.";
                        return Ok(response);
                    }
                    else
                    {
                        response.User.User_Id = user.User_Id;
                        response.User.Username = user.Username;
                        response.User.Email = request.Email;
                        response.User.Phone = request.Phone;
                        response.User.Role_Id = request.Role_Id;
                        response.User.Display_Name = request.Display_Name;
                        response.User.Photo_Url = user.Photo_Url;
                        response.User.Latitude = request.Latitude;
                        response.User.Longitude = request.Longitude;

                        response.Status = "Failed";
                        response.Message = path;
                        return BadRequest(response);
                    }
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = $"No found User id: {request.User_id}";

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

        [HttpGet("{id}"), Authorize()]
        public ActionResult<UserByIdResponse> GetUser(int id)
        {
            var response = new UserByIdResponse();
            try
            {
                var result = _userDAL.GetUser(id);

                if (result != null)
                {
                    var user = new UserResponse();

                    user.User_Id = result.User_Id;
                    user.Username = result.Username;
                    user.Email = result.Email;
                    user.Phone = result.Phone;
                    user.Role_Id = result.Role_Id;
                    user.Is_Active = result.Is_Active;
                    user.Display_Name = result.Display_Name;
                    user.Photo_Url = result.Photo_Url;
                    user.Latitude = result.Latitude;
                    user.Longitude = result.Longitude;

                    response.User = user;
                    response.Message = "Successfully get User.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No User found by id: {id}";
                    response.Status = "Failed";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Failed";

                return BadRequest(response);
            }
        }

        [HttpPost("delete/{id}"), Authorize()]
        public ActionResult<UserByIdResponse> Delete(int id)
        {
            var response = new UserByIdResponse();
            try
            {
                var user = _userDAL.GetUser(id);

                if (user != null)
                {
                    user.Is_Active = false;

                    var result = _userDAL.Update(user);

                    response.User = new UserResponse();
                    response.Message = "Successfully deleted.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No User found by id: {id}";
                    response.Status = "Failed";
                    return BadRequest(response);
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
