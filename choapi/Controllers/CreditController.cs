using choapi.DAL;
using choapi.DAL.Credit;
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
    public class CreditController : ControllerBase
    {
        private readonly ICreditDAL _creditDAL;

        private readonly ILogger<CreditController> _logger;

        public CreditController(ILogger<CreditController> logger, ICreditDAL creditDAL)
        {
            _logger = logger;
            _creditDAL = creditDAL;
        }

        [HttpPost("add"), Authorize()]
        public ActionResult<CreditResponse> Add(CreditDTO request)
        {
            var response = new CreditResponse();
            try
            {
                if (request.Restaurant_Id <= 0)
                {
                    response.Message = "Required Restaurant_Id.";
                    response.Status = "Failed";

                    return BadRequest(response);
                }

                var credit = new Credits
                {
                    Restaurant_Id = request.Restaurant_Id,
                    Amount = request.Amount,
                    Transaction_Type = request.Transaction_Type,
                    Transaction_Date = request.Transaction_Date
                };

                var result = _creditDAL.Add(credit);

                response.Credit = result;
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
        public ActionResult<CreditResponse> Update(CreditDTO request)
        {
            var response = new CreditResponse();
            try
            {
                var credit = _creditDAL.Get(request.Credit_Id);

                if (credit != null)
                {
                    credit.Restaurant_Id = request.Restaurant_Id;
                    credit.Amount = request.Amount;
                    credit.Transaction_Type = request.Transaction_Type;
                    credit.Transaction_Date = request.Transaction_Date;

                    var result = _creditDAL.Update(credit);

                    response.Credit = result;
                    response.Message = "Successfully updated.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No credit found by id: {request.Credit_Id}";
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
        public ActionResult<CreditResponse> delete(int id)
        {
            var response = new CreditResponse();
            try
            {
                var credit = _creditDAL.Get(id);

                if (credit != null)
                {
                    credit.Is_Deleted = true;

                    var result =  _creditDAL.Update(credit);

                    response.Message = "Successfully deleted.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No credit found by id: {id}";
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
        public ActionResult<CreditResponse> GetCredit(int id)
        {
            var response = new CreditResponse();
            try
            {
                var result = _creditDAL.Get(id);

                if (result != null)
                {
                    response.Credit = result;
                    response.Message = "Successfully get Credit.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No credit found by id: {id}";
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

        [HttpGet("restaurant/{id}"), Authorize()]
        public ActionResult<CreditsResponse> GetRestaurantCredits(int id)
        {
            var response = new CreditsResponse();
            try
            {
                var result = _creditDAL.GetCredits(id);

                if (result != null && result.Count > 0)
                {
                    response.Credits = result;
                    response.Message = "Successfully get Credits.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Credit found by restaurant id: {id}";
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
