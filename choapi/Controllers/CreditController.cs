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
    public class CreditController : ControllerBase
    {
        private readonly ICreditDAL _creditDAL;
        private readonly IRestaurantDAL _restaurantDAL;

        private readonly ILogger<CreditController> _logger;

        private const string _entityName = "Credit";

        public CreditController(ILogger<CreditController> logger, ICreditDAL creditDAL, IRestaurantDAL restaurantDAL)
        {
            _logger = logger;
            _creditDAL = creditDAL;
            _restaurantDAL = restaurantDAL;
        }

        [HttpPost("add"), Authorize()]
        public ActionResult<CreditResponse> Add(CreditDTO request)
        {
            var response = new CreditResponse();
            try
            {
                if (request.Establishment_Id <= 0)
                {
                    response.Message = "Required Establishment Id.";
                    response.Status = "Failed";

                    return BadRequest(response);
                }

                var credit = new Credits
                {
                    Establishment_Id = request.Establishment_Id,
                    Amount = request.Amount,
                    Transaction_Type = request.Transaction_Type,
                    Transaction_Date = request.Transaction_Date
                };

                var result = _creditDAL.Add(credit);

                // upate credit of establishment
                var restaurant = _restaurantDAL.GetRestaurant(credit.Establishment_Id);
                if (restaurant != null && credit.Amount != null)
                {
                    if (restaurant.Credits != null)
                        restaurant.Credits += credit.Amount;
                    else
                        restaurant.Credits = credit.Amount;

                    // save changes
                    var resultUpdateCredit = _restaurantDAL.Update(restaurant);
                }

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
                    // undo first the added credit
                    var restaurant = _restaurantDAL.GetRestaurant(credit.Establishment_Id);
                    if (restaurant != null && request.Amount != null)
                    {
                        if (restaurant.Credits != null)
                            restaurant.Credits = restaurant.Credits - credit.Amount;
                        else
                            restaurant.Credits = 0 - credit.Amount;

                        // save changes
                        var resultUpdateCredit = _restaurantDAL.Update(restaurant);
                    }

                    credit.Establishment_Id = request.Establishment_Id;
                    credit.Amount = request.Amount;
                    credit.Transaction_Type = request.Transaction_Type;
                    credit.Transaction_Date = request.Transaction_Date;

                    var result = _creditDAL.Update(credit);

                    // update credit of establishment
                    var updatedRestaurant = _restaurantDAL.GetRestaurant(credit.Establishment_Id);
                    if (updatedRestaurant != null && credit.Amount != null)
                    {
                        if (updatedRestaurant.Credits != null)
                            updatedRestaurant.Credits += credit.Amount;
                        else
                            updatedRestaurant.Credits = credit.Amount;

                        // save changes
                        var resultUpdateCredit = _restaurantDAL.Update(updatedRestaurant);
                    }

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

                    // upate credit of establishment
                    var restaurant = _restaurantDAL.GetRestaurant(credit.Establishment_Id);
                    if (restaurant != null && credit.Amount != null)
                    {
                        if (restaurant.Credits != null)
                            restaurant.Credits = restaurant.Credits - credit.Amount;
                        else
                            restaurant.Credits = 0 - credit.Amount;

                        // save changes
                        var resultUpdateCredit = _restaurantDAL.Update(restaurant);
                    }


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

        [HttpGet("establishment/{id}"), Authorize()]
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
                    response.Message = $"No Credit found by establishment id: {id}";
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
