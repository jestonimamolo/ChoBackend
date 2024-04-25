using choapi.DAL;
using choapi.DTOs;
using choapi.Messages;
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
        public ActionResult<RestaurantResponse> Register(RestaurantDTO request)
        {
            var response = new RestaurantResponse();
            try
            {
                var restaurant = new Restaurants
                {
                    Name = request.Name,
                    Description = request.Description,
                    User_Id = request.User_Id,
                    Credits = request.Credits,
                    Plan = request.Plan,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                    Is_Promoted = request.Is_Promoted,
                    Address = request.Address,
                    Is_Active = true
                };

                var resultRestaurant = _restaurantDAL.Add(restaurant);

                var restaurantImages = new List<RestaurantImages>();

                if (request.Images != null)
                {
                    foreach (var image in request.Images)
                    {
                        var addRestaurantImages = new RestaurantImages();

                        addRestaurantImages.Restaurant_Id = resultRestaurant.Restaurant_Id;
                        addRestaurantImages.Image_Url = image.Image_Url;

                        restaurantImages.Add(addRestaurantImages);
                    }
                }

                var resultRestaurantImages = _restaurantDAL.AddImages(restaurantImages);

                response.Restaurant = resultRestaurant;
                response.Images = resultRestaurantImages;
                response.Message = "Successfully added.";

                return Ok(response);
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Failed";

                return BadRequest(response);
            }
            
        }

        [HttpPost("images/add"), Authorize()]
        public ActionResult<RestaurantImageResponse> AddImage(RestaurantImageDTO request)
        {
            var response = new RestaurantImageResponse();
            try
            {
                var image = new RestaurantImages
                {
                    Restaurant_Id = request.Restaurant_Id,
                    Image_Url = request.Image_Url
                };

                var result = _restaurantDAL.AddImage(image);

                response.Image = result;
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

        [HttpPost("images/update"), Authorize()]
        public ActionResult<RestaurantImageResponse> UpdateImage(RestaurantImageDTO request)
        {
            var response = new RestaurantImageResponse();
            try
            {
                var image = _restaurantDAL.GetRestaurantImage(request.RestaurantImages_Id);

                if (image != null)
                {
                    image.Restaurant_Id = request.Restaurant_Id;
                    image.Image_Url = request.Image_Url;

                    var result = _restaurantDAL.UpdateImage(image);

                    response.Image = result;
                    response.Message = "Successfully updated.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No restaurant images found by id: {request.RestaurantImages_Id}";
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

        [HttpPost("images/delete/{id}"), Authorize()]
        public ActionResult<RestaurantImageResponse> DeleteImage(int id)
        {
            var response = new RestaurantImageResponse();
            try
            {
                var image = _restaurantDAL.GetRestaurantImage(id);

                if (image != null)
                {
                    _restaurantDAL.DeleteImage(image);

                    response.Image = new RestaurantImages();
                    response.Message = "Successfully deleted.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No restaurant images found by id: {id}";
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

        [HttpGet("restaurants/{id}"), Authorize()]
        public ActionResult<RestaurantResponse> GetRestaurant(int id)
        {
            var response = new RestaurantResponse();

            try
            {
                var resultRestaurant = _restaurantDAL.GetRestaurant(id);

                if (resultRestaurant != null)
                {
                    response.Restaurant = resultRestaurant;

                    response.Images = _restaurantDAL.GetRestaurantImages(resultRestaurant.Restaurant_Id);

                    response.Message = $"Successfully get restaurant.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No restaurant found by restaurant id: {id}";
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

        [HttpGet("restaurants"), Authorize()]
        public ActionResult<RestuarantUserIdResponnse> GetRestaurants(int? userId)
        {
            var response = new RestuarantUserIdResponnse();

            try
            {
                var resultRestaurants = _restaurantDAL.GetRestaurants(userId);

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
                    response.Message = $"No restaurant found by user id: {userId}";
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

        [HttpGet("images/{id}"), Authorize()]
        public ActionResult<RestaurantImagesResponse> GetImages(int id)
        {
            var response = new RestaurantImagesResponse();

            try
            {
                var result = _restaurantDAL.GetRestaurantImages(id);

                if (result != null && result.Count > 0)
                {
                    response.Images = result;
                    response.Message = $"Successfully get Restaurant Images.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Restaurant I mages found by id: {id}";
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

        [HttpPost("menus/add"), Authorize()]
        public ActionResult<MenuResponse> MenuAdd(MenuDto request)
        {
            var response = new MenuResponse();
            try
            {
                var menu = new Menus
                {
                    Restaurant_Id = request.Restaurant_Id,
                    Type = request.Type,
                    Path = request.Path
                };

                var result = _restaurantDAL.Add(menu);

                response.Menu = result;

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

        [HttpPost("menus/update"), Authorize()]
        public ActionResult<MenuResponse> MenuUpdate(MenuDto request)
        {
            var response = new MenuResponse();
            try
            {
                var menu = _restaurantDAL.GetMenu(request.Menu_Id);

                if (menu != null)
                {
                    menu.Restaurant_Id = request.Restaurant_Id;
                    menu.Type = request.Type;
                    menu.Path = request.Path;

                    var result = _restaurantDAL.UpdateMenu(menu);

                    response.Menu = result;
                    response.Message = "Successfully updated.";
                    return Ok(response);
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = $"No found Menu id: {request.Menu_Id}";

                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Failed";

                return BadRequest(response);
            }
        }

        [HttpPost("menus/delete/{id}"), Authorize()]
        public ActionResult<MenuResponse> MenuDelete(int id)
        {
            var response = new MenuResponse();
            try
            {
                var menu = _restaurantDAL.GetMenu(id);

                if (menu != null)
                {
                    _restaurantDAL.DeleteMenu(menu);

                    response.Menu = new Menus();
                    response.Message = "Successfully deleted.";
                    return Ok(response);
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = $"No found Menu id: {id}";

                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Failed";

                return BadRequest(response);
            }
        }

        [HttpGet("menus"), Authorize()]
        public ActionResult<MenusResponse> GetMenus(int restaurantId)
        {
            var response = new MenusResponse();
            try
            {
                var result = _restaurantDAL.GetMenus(restaurantId);

                if (result != null && result.Count > 0)
                {
                    response.Menus = result;
                    response.Message = "Successfully get Restaurant Menus.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Restaurant Menus found by restaurant id: {restaurantId}";
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

        [HttpPost("availability/add"), Authorize()]
        public ActionResult<RestaurantAvailabilityResponse> AvailabilityAdd(RestaurantAvailabilityDTO request)
        {
            var response = new RestaurantAvailabilityResponse();
            try
            {
                var availability = new RestaurantAvailability
                {
                    Restaurant_Id = request.Restaurant_Id,
                    Day = request.Day,
                    Time_Start = request.Time_Start,
                    Time_End = request.Time_End
                };

                var result = _restaurantDAL.Add(availability);

                response.Availability = result;

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

        [HttpPost("availability/update"), Authorize()]
        public ActionResult<RestaurantAvailabilityResponse> AvailabilityUpdate(RestaurantAvailabilityDTO request)
        {
            var response = new RestaurantAvailabilityResponse();
            try
            {
                var availability = _restaurantDAL.GetAvailability(request.RestaurantAvailability_Id);

                if (availability != null)
                {
                    availability.Restaurant_Id = request.Restaurant_Id;
                    availability.Day = request.Day;
                    availability.Time_Start = request.Time_Start;
                    availability.Time_End = request.Time_End;

                    var result = _restaurantDAL.UpdateAvailability(availability);

                    response.Availability = result;
                    response.Message = "Successfully updated.";
                    return Ok(response);
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = $"No found Availability id: {request.RestaurantAvailability_Id}";

                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Failed";

                return BadRequest(response);
            }
        }

        [HttpPost("availability/delete/{id}"), Authorize()]
        public ActionResult<RestaurantAvailabilityResponse> AvailabilityDelete(int id)
        {
            var response = new RestaurantAvailabilityResponse();
            try
            {
                var availability = _restaurantDAL.GetAvailability(id);

                if (availability != null)
                {
                    _restaurantDAL.DeleteAvailability(availability);

                    response.Availability = new RestaurantAvailability();
                    response.Message = "Successfully deleted.";
                    return Ok(response);
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = $"No found Availability id: {id}";

                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Failed";

                return BadRequest(response);
            }
        }        

        [HttpGet("availabilities"), Authorize()]
        public ActionResult<RestaurantAvailabilitiesResponse> GetAvailability(int restaurantId)
        {
            var response = new RestaurantAvailabilitiesResponse();
            try
            {
                var result = _restaurantDAL.GetAvailabilities(restaurantId);

                if(result != null && result.Count > 0)
                {
                    response.Availabilities = result;

                    response.Message = "Successfully get Restaurant Availabilities.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Restaurant Availability found by restaurant id: {restaurantId}";
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

        [HttpPost("nonoperatinghours/add"), Authorize()]
        public ActionResult<NonOperatingHourResponse> Add(NonOperatingHoursDTO request)
        {
            var response = new NonOperatingHourResponse();
            try
            {
                var nonOperatingHours = new NonOperatingHours
                {
                    Restaurant_Id = request.Restaurant_Id,
                    Date = request.Date
                };

                var result = _restaurantDAL.Add(nonOperatingHours);

                response.NonOperatingHours = result;

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

        [HttpPost("nonoperatinghours/update"), Authorize()]
        public ActionResult<NonOperatingHourResponse> Update(NonOperatingHoursDTO request)
        {
            var response = new NonOperatingHourResponse();
            try
            {
                var nonOperatingHours = _restaurantDAL.GetNonOperatingHours(request.NonOperatingHours_Id);

                if (nonOperatingHours != null)
                {
                    nonOperatingHours.Restaurant_Id = request.Restaurant_Id;
                    nonOperatingHours.Date = request.Date;

                    var result = _restaurantDAL.Update(nonOperatingHours);

                    response.NonOperatingHours = result;
                    response.Message = "Successfully updated.";
                    return Ok(response);
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = $"No found Non Operating hours id: {request.NonOperatingHours_Id}";

                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Failed";

                return BadRequest(response);
            }
        }

        [HttpPost("nonoperatinghours/delete/{id}"), Authorize()]
        public ActionResult<NonOperatingHourResponse> Delete(int id)
        {
            var response = new NonOperatingHourResponse();
            try
            {
                var nonOperatingHours = _restaurantDAL.GetNonOperatingHours(id);

                if (nonOperatingHours != null)
                {
                    _restaurantDAL.Delete(nonOperatingHours);

                    response.NonOperatingHours = new NonOperatingHours();
                    response.Message = "Successfully deleted.";
                    return Ok(response);
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = $"No found Non Operating Hours id: {id}";

                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Failed";

                return BadRequest(response);
            }
        }

        [HttpGet("nonoperatinghours/{id}"), Authorize()]
        public ActionResult<NonOperatingHourResponse> GetNonOperatingHours(int id)
        {
            var response = new NonOperatingHourResponse();
            try
            {
                var result = _restaurantDAL.GetNonOperatingHours(id);

                if (result != null)
                {
                    response.NonOperatingHours = result;
                    response.Message = "Successfully get Non Operating Hours.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Non Operating Hours found by id: {id}";
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

        [HttpGet("nonoperatinghours"), Authorize()]
        public ActionResult<NonOperatingHoursResponse> GetNonOperatingHoursByRestaurantId(int restaurantId)
        {
            var response = new NonOperatingHoursResponse();
            try
            {
                var result = _restaurantDAL.GetNonOperatingHoursByRestaurantId(restaurantId);

                if (result != null && result.Count > 0)
                {
                    response.NonOperatingHours = result;
                    response.Message = "Successfully get Non Operating Hours.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Non Operating Hours found by restaurant id: {restaurantId}";
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

        [HttpPost("cuisines/add"), Authorize()]
        public ActionResult<RestaurantCuisineResponse> CuisinesAdd(RestaurantCuisineDTO request)
        {
            var response = new RestaurantCuisineResponse();
            try
            {
                var restaurantCuisine = new RestaurantCuisines
                {
                    Restaurant_Id = request.Restaurant_Id,
                    Name = request.Name
                };

                var result = _restaurantDAL.Add(restaurantCuisine);

                response.RestaurantCuisine = result;

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

        [HttpPost("cuisines/update"), Authorize()]
        public ActionResult<RestaurantCuisineResponse> CuisineUpdate(RestaurantCuisineDTO request)
        {
            var response = new RestaurantCuisineResponse();
            try
            {
                var cuisine = _restaurantDAL.GetRestaurantCuisine(request.RestaurantCuisine_Id);

                if (cuisine != null)
                {
                    cuisine.Restaurant_Id = request.Restaurant_Id;
                    cuisine.Name = request.Name;

                    var result = _restaurantDAL.UpdateCuisine(cuisine);

                    response.RestaurantCuisine = result;
                    response.Message = "Successfully updated.";
                    return Ok(response);
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = $"No found Cuisine id: {request.RestaurantCuisine_Id}";

                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Failed";

                return BadRequest(response);
            }
        }

        [HttpPost("cuisines/delete/{id}"), Authorize()]
        public ActionResult<RestaurantCuisineResponse> CuisineDelete(int id)
        {
            var response = new RestaurantCuisineResponse();
            try
            {
                var cuisine = _restaurantDAL.GetRestaurantCuisine(id);

                if (cuisine != null)
                {
                    _restaurantDAL.DeleteCuisine(cuisine);

                    response.RestaurantCuisine = new RestaurantCuisines();
                    response.Message = "Successfully deleted.";
                    return Ok(response);
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = $"No found Cuisine id: {id}";

                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Failed";

                return BadRequest(response);
            }
        }

        [HttpGet("cuicines"), Authorize()]
        public ActionResult<RestaurantCuisinesResponse> GetCuicines(int? restaurantId)
        {
            var response = new RestaurantCuisinesResponse();
            try
            {
                var result = _restaurantDAL.GetRestaurantCuisines(restaurantId);

                if (result != null && result.Count > 0)
                {
                    response.RestaurantCuisines = result;
                    response.Message = "Successfully get Restaurant Cuisines.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Restaurant Cuicine found by restaurant id: {restaurantId}";
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

        [HttpPost("booktype/add"), Authorize()]
        public ActionResult<RestaurantBookTypeResponse> BookTypeAdd(RestaurantBookTypeDTO request)
        {
            var response = new RestaurantBookTypeResponse();
            try
            {
                var bookType = new RestaurantBookType
                {
                    Restaurant_Id = request.Restaurant_Id,
                    Is_Payable = request.Is_Payable,
                    Name = request.Name,
                    Currency = request.Currency,
                    Price = request.Price,
                    Is_Deleted = false
                };

                var result = _restaurantDAL.Add(bookType);

                response.BookType = result;

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

        [HttpPost("booktype/update"), Authorize()]
        public ActionResult<RestaurantBookTypeResponse> BookTypeUpdate(RestaurantBookTypeDTO request)
        {
            var response = new RestaurantBookTypeResponse();
            try
            {
                var bookType = _restaurantDAL.GetBookType(request.RestaurantBookType_Id);

                if (bookType != null)
                {
                    bookType.Restaurant_Id = request.Restaurant_Id;
                    bookType.Is_Payable = request.Is_Payable;
                    bookType.Name = request.Name;
                    bookType.Currency = request.Currency;
                    bookType.Price = request.Price;

                    var result = _restaurantDAL.UpdateBookType(bookType);

                    response.BookType = result;
                    response.Message = "Successfully updated.";
                    return Ok(response);
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = $"No found BookType id: {request.RestaurantBookType_Id}";

                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Failed";

                return BadRequest(response);
            }
        }

        [HttpPost("booktype/delete/{id}"), Authorize()]
        public ActionResult<RestaurantBookTypeResponse> BookTypeDelete(int id)
        {
            var response = new RestaurantBookTypeResponse();
            try
            {
                var bookType = _restaurantDAL.GetBookType(id);

                if (bookType != null)
                {
                    bookType.Is_Deleted = true;

                    var result = _restaurantDAL.UpdateBookType(bookType);

                    response.BookType = result;
                    response.Message = "Successfully deleted.";
                    return Ok(response);
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = $"No found BookType id: {id}";

                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Failed";

                return BadRequest(response);
            }
        }

        [HttpGet("booktype"), Authorize()]
        public ActionResult<RestaurantBookTypesResponse> GetBookTypes(int? restaurantId)
        {
            var response = new RestaurantBookTypesResponse();
            try
            {
                var result = _restaurantDAL.GetBookTypes(restaurantId);

                if (result != null && result.Count > 0)
                {
                    response.BookTypes = result;
                    response.Message = "Successfully get Restaurant Book types.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Restaurant Book Type found by restaurant id: {restaurantId}";
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
