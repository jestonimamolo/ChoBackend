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
        private readonly IEstablishmentDAL _establishmentDAL;

        private readonly ILogger<CreditController> _logger;

        private const string _entityName = "Credit";

        public CreditController(ILogger<CreditController> logger, ICreditDAL creditDAL, IEstablishmentDAL establishmentDAL)
        {
            _logger = logger;
            _creditDAL = creditDAL;
            _establishmentDAL = establishmentDAL;
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
                var establishment = _establishmentDAL.GetEstablishment(credit.Establishment_Id);
                if (establishment != null && credit.Amount != null)
                {
                    if (establishment.Credits != null)
                        establishment.Credits += credit.Amount;
                    else
                        establishment.Credits = credit.Amount;

                    // save changes
                    var resultUpdateCredit = _establishmentDAL.Update(establishment);
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
                    var establishment = _establishmentDAL.GetEstablishment(credit.Establishment_Id);
                    if (establishment != null && request.Amount != null)
                    {
                        if (establishment.Credits != null)
                            establishment.Credits = establishment.Credits - credit.Amount;
                        else
                            establishment.Credits = 0 - credit.Amount;

                        // save changes
                        var resultUpdateCredit = _establishmentDAL.Update(establishment);
                    }

                    credit.Establishment_Id = request.Establishment_Id;
                    credit.Amount = request.Amount;
                    credit.Transaction_Type = request.Transaction_Type;
                    credit.Transaction_Date = request.Transaction_Date;

                    var result = _creditDAL.Update(credit);

                    // update credit of establishment
                    var updatedEstablishment = _establishmentDAL.GetEstablishment(credit.Establishment_Id);
                    if (updatedEstablishment != null && credit.Amount != null)
                    {
                        if (updatedEstablishment.Credits != null)
                            updatedEstablishment.Credits += credit.Amount;
                        else
                            updatedEstablishment.Credits = credit.Amount;

                        // save changes
                        var resultUpdateCredit = _establishmentDAL.Update(updatedEstablishment);
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
                    var establishment = _establishmentDAL.GetEstablishment(credit.Establishment_Id);
                    if (establishment != null && credit.Amount != null)
                    {
                        if (establishment.Credits != null)
                            establishment.Credits = establishment.Credits - credit.Amount;
                        else
                            establishment.Credits = 0 - credit.Amount;

                        // save changes
                        var resultUpdateCredit = _establishmentDAL.Update(establishment);
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
        public ActionResult<CreditsResponse> GetEstablishmentCredits(int id)
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
