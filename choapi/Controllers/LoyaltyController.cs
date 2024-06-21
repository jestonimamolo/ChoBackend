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
    public class LoyaltyController : ControllerBase
    {
        private readonly ILoyaltyDAL _loyaltyDAL;
        private readonly IUserDAL _userDAL;

        private readonly ILogger<LoyaltyController> _logger;

        public LoyaltyController(ILogger<LoyaltyController> logger, ILoyaltyDAL loyaltyDAL, IUserDAL userDAL)
        {
            _logger = logger;
            _loyaltyDAL = loyaltyDAL;
            _userDAL = userDAL;
        }

        [HttpPost("add")]
        public ActionResult<LoyaltyResponse> Add(LoyaltyDTO request)
        {
            var response = new LoyaltyResponse();
            try
            {
                if (request.User_Id <= 0)
                {
                    response.Message = "Required User_Id Id.";
                    response.Status = "Failed";

                    return BadRequest(response);
                }

                var model = new Loyalty
                {
                    User_Id = request.User_Id,
                    Points = request.Points,
                    Type = request.Type,
                    Invited_Id = request.Invited_Id,
                    Created_Date = request.Created_Date
                };

                var result = _loyaltyDAL.Add(model);

                response.Loyalty = result;
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
        public ActionResult<LoyaltyResponse> Update(LoyaltyDTO request)
        {
            var response = new LoyaltyResponse();
            try
            {
                var model = _loyaltyDAL.Get(request.Loyalty_Id);

                if (model != null)
                {
                    model.User_Id = request.User_Id;
                    model.Points = request.Points;
                    model.Type = request.Type;
                    model.Invited_Id = request.Invited_Id;
                    model.Created_Date = request.Created_Date;

                    var result = _loyaltyDAL.Update(model);

                    response.Loyalty = result;
                    response.Message = "Successfully updated.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Loyalty found by id: {request.Loyalty_Id}";
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
        public ActionResult<LoyaltyResponse> Delete(int id)
        {
            var response = new LoyaltyResponse();
            try
            {
                var model = _loyaltyDAL.Get(id);

                if (model != null)
                {
                    model.Is_Deleted = true;

                    var result = _loyaltyDAL.Delete(model);

                    response.Message = "Successfully deleted.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Loyalty found by id: {id}";
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
        public ActionResult<LoyaltyResponse> GetLoyalty(int id)
        {
            var response = new LoyaltyResponse();
            try
            {
                var result = _loyaltyDAL.Get(id);

                if (result != null)
                {
                    response.Loyalty = result;
                    response.Message = "Successfully get Loyalty.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Loyalty found by id: {id}";
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
        public ActionResult<LoyaltiesResponse> GetLoyalties(int id)
        {
            var response = new LoyaltiesResponse();
            try
            {
                var result = _loyaltyDAL.GetByUserId(id);

                if (result != null && result.Count > 0)
                {
                    response.loyalties = result;
                    response.Message = "Successfully get Loyalties.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Loyalty found by user id: {id}";
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

        [HttpGet("points/update"), Authorize()]
        public ActionResult<LoyaltyResponse> GetLoyalties(int loyaltyId, double points)
        {
            var response = new LoyaltyResponse();
            try
            {
                var model = _loyaltyDAL.Get(loyaltyId);

                if (model != null)
                {
                    model.Points = points;

                    var result = _loyaltyDAL.Update(model);

                    response.Loyalty = result;
                    response.Message = "Successfully get Loyalties.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Loyalty found by id: {loyaltyId}";
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
