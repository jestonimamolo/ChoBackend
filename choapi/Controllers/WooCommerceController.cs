using choapi.DAL;
using choapi.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace choapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WooCommerceController : ControllerBase
    {
        private readonly IRestaurantDAL _restaurantDAL;

        private readonly ILogger<RestaurantController> _logger;

        public WooCommerceController(ILogger<RestaurantController> logger, IRestaurantDAL restaurantDAL)
        {
            _logger = logger;
            _restaurantDAL = restaurantDAL;
        }

        [HttpGet("restaurants")]
        public ActionResult<RestuarantUserIdResponnse> GetRestaurants()
        {
            var response = new RestuarantUserIdResponnse();

            try
            {
                var resultRestaurants = _restaurantDAL.GetRestaurants(null);

                if (resultRestaurants != null && resultRestaurants.Count > 0)
                {
                    foreach (var restaurant in resultRestaurants)
                    {
                        var resultRestaurant = new RestaurantsReponse();

                        resultRestaurant.Restaurant_Id = restaurant.Restaurant_Id;
                        resultRestaurant.Name = restaurant.Name;
                        resultRestaurant.Description = restaurant.Description;
                        resultRestaurant.User_Id = restaurant.User_Id;
                        resultRestaurant.Credits = restaurant.Credits;
                        resultRestaurant.Plan = restaurant.Plan;
                        resultRestaurant.Latitude = restaurant.Latitude;
                        resultRestaurant.Longitude = restaurant.Longitude;
                        resultRestaurant.Is_Promoted = restaurant.Is_Promoted;
                        resultRestaurant.Address = restaurant.Address;
                        resultRestaurant.Is_Active = restaurant.Is_Active;

                        resultRestaurant.Images = _restaurantDAL.GetRestaurantImages(restaurant.Restaurant_Id);

                        response.Restaurants.Add(resultRestaurant);
                    }
                    response.Message = $"Successfully get restaurants.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No restaurant found.";
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
