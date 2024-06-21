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
    public class NotificationController : ControllerBase
    {
        private readonly INotificationDAL _notificationDAL;
        private readonly IUserDAL _userDAL;

        private readonly ILogger<NotificationController> _logger;

        public NotificationController(ILogger<NotificationController> logger, INotificationDAL notificationDAL, IUserDAL userDAL)
        {
            _logger = logger;
            _notificationDAL = notificationDAL;
            _userDAL = userDAL;
        }

        [HttpPost("add")]
        public ActionResult<NotificationResponse> Add(NotificationDTO request)
        {
            var response = new NotificationResponse();
            try
            {
                if (request.Sender_Id <= 0)
                {
                    response.Message = "Required Sender_Id Id.";
                    response.Status = "Failed";

                    return BadRequest(response);
                }

                if (request.Receiver_Id <= 0)
                {
                    response.Message = "Required Receiver_Id Id.";
                    response.Status = "Failed";

                    return BadRequest(response);
                }

                var model = new Notification
                {
                    Sender_Id = request.Sender_Id,
                    Receiver_Id = request.Receiver_Id,
                    Title = request.Title,
                    Message = request.Message,
                    Created_Date = request.Created_Date
                };

                var result = _notificationDAL.Add(model);

                response.Notification = result;
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
        public ActionResult<NotificationResponse> Update(NotificationDTO request)
        {
            var response = new NotificationResponse();
            try
            {
                var model = _notificationDAL.Get(request.Notification_Id);

                if (model != null)
                {
                    model.Sender_Id = request.Sender_Id;
                    model.Receiver_Id = request.Receiver_Id;
                    model.Title = request.Title;
                    model.Message = request.Message;
                    model.Created_Date = request.Created_Date;

                    var result = _notificationDAL.Update(model);

                    response.Notification = result;
                    response.Message = "Successfully updated.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Notification found by id: {request.Notification_Id}";
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
        public ActionResult<NotificationResponse> Delete(int id)
        {
            var response = new NotificationResponse();
            try
            {
                var model = _notificationDAL.Get(id);

                if (model != null)
                {
                    model.Is_Deleted = true;

                    var result = _notificationDAL.Delete(model);

                    response.Message = "Successfully deleted.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Notification found by id: {id}";
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
        public ActionResult<NotificationResponse> GetNotification(int id)
        {
            var response = new NotificationResponse();
            try
            {
                var result = _notificationDAL.Get(id);

                if (result != null)
                {
                    response.Notification = result;
                    response.Message = "Successfully get Notification.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Notification found by id: {id}";
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

        [HttpGet("notificatoins/user/{id}"), Authorize()]
        public ActionResult<NotificationsResponse> GetNotificationByUser(int id)
        {
            var response = new NotificationsResponse();
            try
            {
                var result = _notificationDAL.GetByUser(id);

                if (result != null)
                {
                    response.Notifications = result;
                    response.Message = "Successfully get Notifications.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Notification found by User id: {id}";
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
