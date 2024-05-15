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
    public class SaveEstablishmentController : ControllerBase
    {
        private readonly ISaveEstablishmentDAL _modelDAL;

        private readonly ILogger<SaveEstablishmentController> _logger;

        private const string _entityName = "Save Establishment";

        public SaveEstablishmentController(ILogger<SaveEstablishmentController> logger, ISaveEstablishmentDAL modelDAL)
        {
            _logger = logger;
            _modelDAL = modelDAL;
        }

        [HttpPost("add"), Authorize()]
        public ActionResult<SaveEstablishmentResponse> Add(SaveEstablishmentDTO request)
        {
            var response = new SaveEstablishmentResponse();
            try
            {
                if (request.User_Id <= 0)
                {
                    response.Message = $"Required User Id.";
                    response.Status = "Failed";

                    return BadRequest(response);
                }

                if (request.Establishment_Id <= 0)
                {
                    response.Message = $"Required Establishment Id.";
                    response.Status = "Failed";

                    return BadRequest(response);
                }

                var model = new SaveEstablishment
                {
                    Establishment_Id = request.Establishment_Id,
                    User_Id = request.User_Id,
                    Date_Added = request.Date_Added
                };

                var result = _modelDAL.Add(model);

                response.SaveEstablishment = result;
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
        public ActionResult<SaveEstablishmentResponse> Update(SaveEstablishmentDTO request)
        {
            var response = new SaveEstablishmentResponse();
            try
            {
                var model = _modelDAL.Get(request.SaveEstablishment_Id);

                if (model != null)
                {
                    model.Establishment_Id = request.Establishment_Id;
                    model.User_Id = request.User_Id;

                    var result = _modelDAL.Update(model);

                    response.SaveEstablishment = result;
                    response.Message = "Successfully updated.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No {_entityName} found by id: {request.SaveEstablishment_Id}";
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
        public ActionResult<SaveEstablishmentResponse> Delete(int id)
        {
            var response = new SaveEstablishmentResponse();
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
        public ActionResult<SaveEstablishmentResponse> Get(int id)
        {
            var response = new SaveEstablishmentResponse();
            try
            {
                var result = _modelDAL.Get(id);

                if (result != null)
                {
                    response.SaveEstablishment = result;
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
        public ActionResult<SaveEstablishmentsResponse> GetByUser(int id)
        {
            var response = new SaveEstablishmentsResponse();
            try
            {
                var result = _modelDAL.GetByUserId(id);

                if (result != null && result.Count > 0)
                {
                    response.SaveEstablishments = result;
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
    }
}
