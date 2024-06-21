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
    public class InviteController : ControllerBase
    {
        private readonly IInviteDAL _inviteDAL;

        private readonly ILogger<InviteController> _logger;

        public InviteController(ILogger<InviteController> logger, IInviteDAL inviteDAL)
        {
            _logger = logger;
            _inviteDAL = inviteDAL;
        }

        [HttpPost("add")]
        public ActionResult<InviteResponse> Add(InviteDTO request)
        {
            var response = new InviteResponse();
            try
            {
                if (request.User_Id <= 0)
                {
                    response.Message = "Required User_Id Id.";
                    response.Status = "Failed";

                    return BadRequest(response);
                }

                var model = new Invite
                {
                    User_Id = request.User_Id,
                    Invited_User = request.Invited_User,
                    Booking_Id = request.Booking_Id,
                    Status = request.Status,
                    Created_Date = request.Created_Date
                };

                var result = _inviteDAL.Add(model);

                response.Invite = result;
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
        public ActionResult<InviteResponse> Update(InviteDTO request)
        {
            var response = new InviteResponse();
            try
            {
                var model = _inviteDAL.Get(request.Invite_Id);

                if (model != null)
                {
                    model.User_Id = request.User_Id;
                    model.Invited_User = request.Invited_User;
                    model.Booking_Id = request.Booking_Id;
                    model.Status = request.Status;
                    model.Created_Date = request.Created_Date;

                    var result = _inviteDAL.Update(model);

                    response.Invite = result;
                    response.Message = "Successfully updated.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Invite found by id: {request.Invite_Id}";
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
        public ActionResult<InviteResponse> Delete(int id)
        {
            var response = new InviteResponse();
            try
            {
                var model = _inviteDAL.Get(id);

                if (model != null)
                {
                    model.Is_Deleted = true;

                    var result = _inviteDAL.Delete(model);

                    response.Message = "Successfully deleted.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Invite found by id: {id}";
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
        public ActionResult<InviteResponse> Get(int id)
        {
            var response = new InviteResponse();
            try
            {
                var result = _inviteDAL.Get(id);

                if (result != null)
                {
                    response.Invite = result;
                    response.Message = "Successfully get Invite.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Invite found by id: {id}";
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

        [HttpGet("bookings/{id}"), Authorize()]
        public ActionResult<InviteesResponse> GetByBooking(int id)
        {
            var response = new InviteesResponse();
            try
            {
                var result = _inviteDAL.GetByBooking(id);

                if (result != null && result.Count > 0)
                {
                    response.Invitees = result;
                    response.Message = "Successfully get Invitees.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Invite found by booking id: {id}";
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

        [HttpGet("users/{id}"), Authorize()]
        public ActionResult<InviteesResponse> GetByUser(int id)
        {
            var response = new InviteesResponse();
            try
            {
                var result = _inviteDAL.GetByUser(id);

                if (result != null && result.Count > 0)
                {
                    response.Invitees = result;
                    response.Message = "Successfully get Invitees.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Invite found by user id: {id}";
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
