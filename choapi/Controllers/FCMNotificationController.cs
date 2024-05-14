using choapi.DAL;
using choapi.DTOs;
using choapi.Messages;
using choapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace choapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FCMNotificationController : ControllerBase
    {
        private readonly IFCMNotificationDAL _modelDAL;

        private readonly ILogger<FCMNotificationController> _logger;

        private const string _entityName = "FCMNotication";

        public FCMNotificationController(ILogger<FCMNotificationController> logger, IFCMNotificationDAL modelDAL)
        {
            _logger = logger;
            _modelDAL = modelDAL;
        }

        [HttpPost("add"), Authorize()]
        public ActionResult<FCMNotificationResponse> Add(FCMNotificationDTO request)
        {
            var response = new FCMNotificationResponse();
            try
            {
                if (request.User_Id <= 0)
                {
                    response.Message = $"Required User Id.";
                    response.Status = "Failed";

                    return BadRequest(response);
                }

                var model = new FCMNotification
                {
                    User_Id = request.User_Id,
                    FCM_Id = request.FCM_Id,
                    Date_Addd = DateTime.Now
                };

                var result = _modelDAL.Add(model);

                response.FCMNotification = result;
                response.Message = "Successfully added.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Failed";
                return BadRequest(response);
            }
        }

        [HttpPost("update"), Authorize()]
        public ActionResult<FCMNotificationResponse> Update(FCMNotificationDTO request)
        {
            var response = new FCMNotificationResponse();
            try
            {
                var model = _modelDAL.Get(request.FCMNotification_Id);

                if (model != null)
                {
                    model.FCM_Id = request.FCM_Id;
                    model.User_Id = request.User_Id;

                    var result = _modelDAL.Update(model);

                    response.FCMNotification = result;
                    response.Message = "Successfully updated.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No {_entityName} found by id: {request.FCMNotification_Id}";
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
        public ActionResult<FCMNotificationResponse> Delete(int id)
        {
            var response = new FCMNotificationResponse();
            try
            {
                var model = _modelDAL.Get(id);

                if (model != null)
                {
                    model.Is_Active = false;

                    var result = _modelDAL.Delete(model);

                    response.Message = "Successfully deleted.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No {_entityName} found by id: {id}";
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

        [HttpGet("{id}"), Authorize()]
        public ActionResult<FCMNotificationResponse> Get(int id)
        {
            var response = new FCMNotificationResponse();
            try
            {
                var result = _modelDAL.Get(id);

                if (result != null)
                {
                    response.FCMNotification = result;
                    response.Message = $"Successfully get {_entityName}.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No {_entityName} found by id: {id}";
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

        [HttpGet("user/{id}"), Authorize()]
        public ActionResult<FCMNotificationsResponse> GetByUser(int id)
        {
            var response = new FCMNotificationsResponse();
            try
            {
                var result = _modelDAL.GetByUserId(id);

                if (result != null && result.Count > 0)
                {
                    response.FCMNotifications = result;
                    response.Message = $"Successfully get ${_entityName}s.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No ${_entityName} found by user id: {id}";
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

        [HttpGet(), Authorize()]
        public ActionResult<FCMNotificationResponse> GetFCMId(string fcmId)
        {
            var response = new FCMNotificationResponse();
            try
            {
                var result = _modelDAL.GetByFCMId(fcmId);

                if (result != null)
                {
                    response.FCMNotification = result;
                    response.Message = $"Successfully get ${_entityName}.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No ${_entityName} found by fcm id: {fcmId}";
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
