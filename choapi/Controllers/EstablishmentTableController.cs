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
    public class EstablishmentTableController : ControllerBase
    {
        private readonly IEstablishmentTableDAL _establishmentTableDAL;

        private readonly ILogger<EstablishmentTableController> _logger;

        public EstablishmentTableController(ILogger<EstablishmentTableController> logger, IEstablishmentTableDAL establishmentTableDAL)
        {
            _logger = logger;
            _establishmentTableDAL = establishmentTableDAL;
        }

        [HttpPost("add"), Authorize()]
        public ActionResult<EstablishmentTableResponse> Add(EstablishmentTableDTO request)
        {
            var response = new EstablishmentTableResponse();
            try
            {
                if (request.Establishment_Id <= 0)
                {
                    response.Message = "Required Establishment_Id.";
                    response.Status = "Failed";

                    return BadRequest(response);
                }

                var establishementTable = new EstablishmentTable
                {
                    EstablishmentTable_Id = request.Establishment_Id,
                    Capacity = request.Capacity,
                    Time_Start = request.Time_Start
                };

                var result = _establishmentTableDAL.Add(establishementTable);

                response.EstablishmentTable_Id = result.EstablishmentTable_Id;
                response.Establishment_Id = result.Establishment_Id;
                response.Capacity = result.Capacity;
                response.Time_Start = result.Time_Start;

                response.Message = "Successfully added.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.EstablishmentTable_Id = 0;
                response.Establishment_Id = request.Establishment_Id;
                response.Capacity = request.Capacity;
                response.Time_Start = request.Time_Start;

                response.Message = ex.Message;
                response.Status = "Failed";

                return BadRequest(response);
            }
        }

        [HttpPost("update"), Authorize()]
        public ActionResult<EstablishmentTableResponse> Update(EstablishmentTableDTO request)
        {
            var response = new EstablishmentTableResponse();
            try
            {
                var establishmentTable = _establishmentTableDAL.GetEstablishmentTable(request.EstablishmentTable_Id);

                if (establishmentTable != null)
                {
                    establishmentTable.Establishment_Id = request.Establishment_Id;
                    establishmentTable.Capacity = request.Capacity;
                    establishmentTable.Time_Start = request.Time_Start;

                    var result = _establishmentTableDAL.Update(establishmentTable);

                    response.EstablishmentTable_Id = result.EstablishmentTable_Id;
                    response.Establishment_Id = result.Establishment_Id;
                    response.Capacity = result.Capacity;
                    response.Time_Start = result.Time_Start;

                    response.Message = "Successfully updated.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Establishment table found by id: {request.EstablishmentTable_Id}";
                    response.Status = "Failed";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.EstablishmentTable_Id = 0;
                response.Establishment_Id = request.Establishment_Id;
                response.Capacity = request.Capacity;
                response.Time_Start = request.Time_Start;

                response.Message = ex.Message;
                response.Status = "Failed";

                return BadRequest(response);
            }
        }

        [HttpPost("delete/{id}"), Authorize()]
        public ActionResult<EstablishmentTableResponse> delete(int id)
        {
            var response = new EstablishmentTableResponse();
            try
            {
                var establishmentTable = _establishmentTableDAL.GetEstablishmentTable(id);

                if (establishmentTable != null)
                {
                    _establishmentTableDAL.Delete(establishmentTable);

                    response.Message = "Successfully deleted.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No establishment table found by id: {id}";
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
        public ActionResult<EstablishmentTable> GetEstablishmentTable(int id)
        {
            var response = new EstablishmentTableResponse();
            try
            {
                var result = _establishmentTableDAL.GetEstablishmentTable(id);

                if (result != null)
                {
                    response.EstablishmentTable_Id = result.EstablishmentTable_Id;
                    response.Establishment_Id = result.Establishment_Id;
                    response.Capacity = result.Capacity;
                    response.Time_Start = result.Time_Start;

                    response.Message = "Successfully get Establishment Table.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Establishment table found by id: {id}";
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
        public ActionResult<List<EstablishmentTablesResponse>> GetEstablishmentTables(int id)
        {
            var response = new EstablishmentTablesResponse();
            try
            {
                var result = _establishmentTableDAL.GetEstablishmentTables(id);

                if (result != null && result.Count > 0)
                {
                    foreach(var resultTable in result)
                    {
                        var establishmentTable = new EstablishmentTableEstablishmentResponse();

                        establishmentTable.EstablishmentTable_Id = resultTable.EstablishmentTable_Id;
                        establishmentTable.Establishment_Id = resultTable.Establishment_Id;
                        establishmentTable.Capacity = resultTable.Capacity;
                        establishmentTable.Time_Start = resultTable.Time_Start;

                        response.EstablishmentTables.Add(establishmentTable);
                    }

                    response.Message = "Successfully get Establishment Table.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Establishment table found by establishment id: {id}";
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
