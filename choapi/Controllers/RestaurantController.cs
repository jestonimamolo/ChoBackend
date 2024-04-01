using choapi.DAL;
using choapi.DTOs;
using choapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace choapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantDAL _restaurantDAL;        

        private readonly ILogger<RestaurantController> _logger;

        public RestaurantController(ILogger<RestaurantController> logger, IRestaurantDAL restaurantDAL)
        {
            _logger = logger;
            _restaurantDAL = restaurantDAL;
        }

        #region commented

        //private static readonly string[] Names =
        //{
        //    "Mang Inasal", "Jollibee", "McDonalds", "Chowking", "Greenwich", "Sols", "Angel Pizza", "Pizza Hot", "Snr Kimchi", "Vikings"
        //};

        //private static readonly string[] Address =
        //{
        //    "Cebu city", "Mandaue city", "Cebu city", "Mandaue city", "Lapu-Lapu city", "Lapu city", "Talisay city", "Cebu city", "Mandaue city", "Cebu city"
        //};

        //[HttpGet(Name = "GetRestaurant"), Authorize()]
        //public IEnumerable<Restaurant> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new Restaurant
        //    {
        //        Name = Names[Random.Shared.Next(Names.Length)],
        //        Address = Address[Random.Shared.Next(Address.Length)]
        //    })
        //    .ToArray();
        //}

        #endregion

        [HttpPost("register"), Authorize()]
        public ActionResult<Restaurant> Register(RestaurantDto request)
        {
            var restaurant = new Restaurant
            {
                Name = request.Name,
                Address = request.Address,
                Contact_Number = request.Contact_Number,
                Opening_Hours = request.Opening_Hours,
                Cousine_Type = request.Cousine_Type,
                Registration_Fee = request.Registration_Fee,
                User_Id_Manager = request.User_Id_Manager,
                Is_Registered = true
            };

            var resultRestaurant = _restaurantDAL.Add(restaurant);

            return Ok(resultRestaurant);
        }

        [HttpGet("restaurants/{id}"), Authorize()]
        public ActionResult<Restaurant> GetRestaurant(int id)
        {
            var restaurant = _restaurantDAL.GetRestaurant(id);

            return Ok(restaurant);
        }

        [HttpGet("restaurants"), Authorize()]
        public ActionResult<List<Restaurant>> GetRestaurants(int userIdManager)
        {
            var restaurants = _restaurantDAL.GetRestaurants(userIdManager);

            return Ok(restaurants);
        }
    }
}
