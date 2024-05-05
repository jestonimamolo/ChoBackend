using Azure.Core;
using choapi.DAL;
using choapi.DTOs;
using choapi.Messages;
using choapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;

namespace choapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantTableController : ControllerBase
    {
        private readonly IRestaurantTableDAL _restaurantTableDAL;

        private readonly ILogger<RestaurantTableController> _logger;

        public RestaurantTableController(ILogger<RestaurantTableController> logger, IRestaurantTableDAL restaurantTableDAL)
        {
            _logger = logger;
            _restaurantTableDAL = restaurantTableDAL;
        }

        [HttpPost("add"), Authorize()]
        public ActionResult<RestaurantTableResponse> Add(RestaurantTableDTO request)
        {
            var response = new RestaurantTableResponse();
            try
            {
                if (request.Restaurant_Id <= 0)
                {
                    response.Message = "Required Restaurant_Id.";
                    response.Status = "Failed";

                    return BadRequest(response);
                }

                var restaurantTable = new RestaurantTable
                {
                    Restaurant_Id = request.Restaurant_Id,
                    Capacity = request.Capacity,
                    Time_Start = request.Time_Start
                };

                var result = _restaurantTableDAL.Add(restaurantTable);

                response.RestaurantTable_Id = result.RestaurantTable_Id;
                response.Restaurant_Id = result.Restaurant_Id;
                response.Capacity = result.Capacity;
                response.Time_Start = result.Time_Start;

                response.Message = "Successfully added.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.RestaurantTable_Id = 0;
                response.RestaurantTable_Id = request.Restaurant_Id;
                response.Capacity = request.Capacity;
                response.Time_Start = request.Time_Start;

                response.Message = ex.Message;
                response.Status = "Failed";

                return BadRequest(response);
            }
        }

        [HttpPost("update"), Authorize()]
        public ActionResult<RestaurantTableResponse> Update(RestaurantTableDTO request)
        {
            var response = new RestaurantTableResponse();
            try
            {
                var restaurantTable = _restaurantTableDAL.GetRestaurantTable(request.RestaurantTable_Id);

                if (restaurantTable != null)
                {
                    restaurantTable.Restaurant_Id = request.Restaurant_Id;
                    restaurantTable.Capacity = request.Capacity;
                    restaurantTable.Time_Start = request.Time_Start;

                    var result = _restaurantTableDAL.Update(restaurantTable);

                    response.RestaurantTable_Id = result.RestaurantTable_Id;
                    response.Restaurant_Id = result.Restaurant_Id;
                    response.Capacity = result.Capacity;
                    response.Time_Start = result.Time_Start;

                    response.Message = "Successfully updated.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No restaurant table found by id: {request.RestaurantTable_Id}";
                    response.Status = "Failed";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.RestaurantTable_Id = 0;
                response.RestaurantTable_Id = request.Restaurant_Id;
                response.Capacity = request.Capacity;
                response.Time_Start = request.Time_Start;

                response.Message = ex.Message;
                response.Status = "Failed";

                return BadRequest(response);
            }
        }

        [HttpPost("delete/{id}"), Authorize()]
        public ActionResult<RestaurantTableResponse> delete(int id)
        {
            var response = new RestaurantTableResponse();
            try
            {
                var restaurantTable = _restaurantTableDAL.GetRestaurantTable(id);

                if (restaurantTable != null)
                {
                    _restaurantTableDAL.Delete(restaurantTable);

                    response.Message = "Successfully deleted.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No restaurant table found by id: {id}";
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
        public ActionResult<RestaurantTable> GetRestaurantTable(int id)
        {
            var response = new RestaurantTableResponse();
            try
            {
                var result = _restaurantTableDAL.GetRestaurantTable(id);

                if (result != null)
                {
                    response.RestaurantTable_Id = result.RestaurantTable_Id;
                    response.Restaurant_Id = result.Restaurant_Id;
                    response.Capacity = result.Capacity;
                    response.Time_Start = result.Time_Start;

                    response.Message = "Successfully get Restaurant Table.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No restaurant table found by id: {id}";
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
        public ActionResult<List<RestaurantTablesResponse>> GetRestaurantTables(int id)
        {
            var response = new RestaurantTablesResponse();
            try
            {
                var result = _restaurantTableDAL.GetRestaurantTables(id);

                if (result != null && result.Count > 0)
                {
                    foreach(var resultTable in result)
                    {
                        var restaurantTable = new RestaurantTableRestaurantResponse();

                        restaurantTable.RestaurantTable_Id = restaurantTable.RestaurantTable_Id;
                        restaurantTable.Restaurant_Id = restaurantTable.Restaurant_Id;
                        restaurantTable.Capacity = resultTable.Capacity;
                        restaurantTable.Time_Start = resultTable.Time_Start;

                        response.RestaurantTables.Add(restaurantTable);
                    }

                    response.Message = "Successfully get Restaurant Table.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No restaurant table found by restaurant id: {id}";
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
