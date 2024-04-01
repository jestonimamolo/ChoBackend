using choapi.DAL;
using choapi.DTOs;
using choapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<RestaurantTable> Add(RestaurantTableDto request)
        {
            var restaurantTable = new RestaurantTable
            {
                Restaurant_Id = request.Restaurant_Id,
                Table_Number = request.Table_Number,
                Capacity = request.Capacity,
                Is_Reserved = false
            };

            var resultRestaurant = _restaurantTableDAL.Add(restaurantTable);

            return Ok(resultRestaurant);
        }

        [HttpGet("tables/{id}"), Authorize()]
        public ActionResult<RestaurantTable> GetRestaurantTable(int id)
        {
            var result = _restaurantTableDAL.GetRestaurantTable(id);

            return Ok(result);
        }

        [HttpGet("tables"), Authorize()]
        public ActionResult<List<RestaurantTable>> GetRestaurantTables(int restaurantId)
        {
            var result = _restaurantTableDAL.GetRestaurantTables(restaurantId);

            return Ok(result);
        }
    }
}
