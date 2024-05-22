using choapi.DAL;
using choapi.DTOs;
using choapi.Messages;
using choapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace choapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardDetailsController : ControllerBase
    {
        private readonly ICardDetailsDAL _modelDAL;

        private readonly ILogger<CardDetailsController> _logger;

        public CardDetailsController(ILogger<CardDetailsController> logger, ICardDetailsDAL modelDAL)
        {
            _logger = logger;
            _modelDAL = modelDAL;
        }

        [HttpPost("add"), Authorize()]
        public ActionResult<CardDetailsResponse> Add(CardDetailsDTO request)
        {
            var response = new CardDetailsResponse();
            try
            {
                if (string.IsNullOrEmpty(request.Card_Token))
                {
                    response.Message = "Required Card Token.";
                    response.Status = "Failed";

                    return BadRequest(response);
                }

                var model = new CardDetails
                {
                    Card_Token = request.Card_Token,
                    User_Id = request.User_Id,
                    Establishment_Id = request.Establishment_Id,
                    Is_Active = request.Is_Active,
                };

                var result = _modelDAL.Add(model);

                response.CardDetails = result;
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
        public ActionResult<CardDetailsResponse> Update(CardDetailsDTO request)
        {
            var response = new CardDetailsResponse();
            try
            {
                var model = _modelDAL.Get(request.CardDetails_Id);

                if (model != null)
                {
                    model.Card_Token = request.Card_Token;
                    model.User_Id = request.User_Id;
                    model.Establishment_Id = request.Establishment_Id;
                    model.Is_Active = request.Is_Active;

                    var result = _modelDAL.Update(model);

                    response.CardDetails = result;
                    response.Message = "Successfully updated.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Card details found by id: {request.CardDetails_Id}";
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
        public ActionResult<CardDetailsResponse> Delete(int id)
        {
            var response = new CardDetailsResponse();
            try
            {
                var model = _modelDAL.Get(id);

                if (model != null)
                {
                    model.Is_Deleted = true;

                    var result = _modelDAL.Delete(model);

                    response.Message = "Successfully deleted.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Card details found by id: {id}";
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

        [HttpPost("deactivate/{id}"), Authorize()]
        public ActionResult<CardDetailsResponse> Deactivate(int id)
        {
            var response = new CardDetailsResponse();
            try
            {
                var model = _modelDAL.Get(id);

                if (model != null)
                {
                    model.Is_Active = false;

                    var result = _modelDAL.Update(model);

                    response.Message = "Successfully deactivated.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Card details found by id: {id}";
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
        public ActionResult<CardDetailsResponse> GetCardDetails(int id)
        {
            var response = new CardDetailsResponse();
            try
            {
                var result = _modelDAL.Get(id);

                if (result != null)
                {
                    response.CardDetails = result;
                    response.Message = "Successfully get Card details.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Card details found by id: {id}";
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
        public ActionResult<CardDetailsResponse> GetCardDetailsByUserId(int id)
        {
            var response = new CardDetailsResponse();
            try
            {
                var result = _modelDAL.GetByUserId(id);

                if (result != null)
                {
                    response.CardDetails = result;
                    response.Message = "Successfully get Card details.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Card details found by id: {id}";
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

        [HttpGet("establishment/{id}"), Authorize()]
        public ActionResult<CardDetailsResponse> GetCardDetailsByEstablishmentId(int id)
        {
            var response = new CardDetailsResponse();
            try
            {
                var result = _modelDAL.GetByEstablishmentId(id);

                if (result != null)
                {
                    response.CardDetails = result;
                    response.Message = "Successfully get Card details.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Card details found by id: {id}";
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
