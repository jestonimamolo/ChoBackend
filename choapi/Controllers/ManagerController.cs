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
    public class ManagerController : ControllerBase
    {
        private readonly IManagerDAL _managerDAL;

        private readonly ILogger<ManagerController> _logger;

        public ManagerController(ILogger<ManagerController> logger, IManagerDAL managerDAL)
        {
            _logger = logger;
            _managerDAL = managerDAL;
        }

        [HttpPost("add"), Authorize()]
        public ActionResult<ManagerResponse> Add(ManagerDTO request)
        {
            var response = new ManagerResponse();
            try
            {
                if (request.Establishment_Id <= 0)
                {
                    response.Message = "Required Establishment Id.";
                    response.Status = "Failed";

                    return BadRequest(response);
                }

                var manager = new Manager
                {
                    Establishment_Id = request.Establishment_Id,
                    Created_By = request.Created_By,
                    Created_Date = request.Created_Date
                };

                var result = _managerDAL.Add(manager);

                response.Manager = result;
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
        public ActionResult<ManagerResponse> Update(ManagerDTO request)
        {
            var response = new ManagerResponse();
            try
            {
                var manager = _managerDAL.Get(request.Manager_Id);

                if (manager != null)
                {
                    manager.Establishment_Id = request.Establishment_Id;
                    manager.Created_By = request.Created_By;
                    manager.Created_Date = request.Created_Date;

                    var result = _managerDAL.Update(manager);

                    response.Manager = result;
                    response.Message = "Successfully updated.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No manager found by id: {request.Manager_Id}";
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
        public ActionResult<ManagerResponse> Delete(int id)
        {
            var response = new ManagerResponse();
            try
            {
                var manager = _managerDAL.Get(id);

                if (manager != null)
                {
                    manager.Is_Deleted = true;

                    var result = _managerDAL.Delete(manager);

                    response.Message = "Successfully deleted.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No manager found by id: {id}";
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
        public ActionResult<ManagerResponse> GetManager(int id)
        {
            var response = new ManagerResponse();
            try
            {
                var result = _managerDAL.Get(id);

                if (result != null)
                {
                    response.Manager = result;
                    response.Message = "Successfully get manager.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No manager found by id: {id}";
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
        public ActionResult<ManagersResponse> GetManagerByEstablishmentId(int id)
        {
            var response = new ManagersResponse();
            try
            {
                var result = _managerDAL.GetByEstablishmentId(id);

                if (result != null && result.Count > 0)
                {
                    response.Managers = result;
                    response.Message = "Successfully get Managers.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Manager found by establishment id: {id}";
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
