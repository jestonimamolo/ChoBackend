using choapi.DAL;
using choapi.DTOs;
using choapi.Helper;
using choapi.Messages;
using choapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Numerics;

namespace choapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantDAL _restaurantDAL;

        private readonly ILogger<RestaurantController> _logger;

        private const string _fromMenus = "restaurant-menus";
        private const string _fromImages = "restaurant-images";

        public RestaurantController(ILogger<RestaurantController> logger, IRestaurantDAL restaurantDAL)
        {
            _logger = logger;
            _restaurantDAL = restaurantDAL;
        }

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

                //var restaurantImages = new List<RestaurantImages>();

                //if (request.Images != null)
                //{
                //    foreach (var image in request.Images)
                //    {
                //        var addRestaurantImages = new RestaurantImages();

                //        addRestaurantImages.Restaurant_Id = resultRestaurant.Restaurant_Id;
                //        addRestaurantImages.Image_Url = image.Image_Url;

                //        restaurantImages.Add(addRestaurantImages);
                //    }
                //}

                //var resultRestaurantImages = _restaurantDAL.AddImages(restaurantImages);

                response.Restaurant = resultRestaurant;
                //response.Images = resultRestaurantImages;
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

        [HttpPost("update"), Authorize()]
        public ActionResult<RestaurantResponse> Update(RestaurantDTO request)
        {
            var response = new RestaurantResponse();
            try
            {
                var restaurant = _restaurantDAL.GetRestaurant(request.Restaurant_Id);

                if (restaurant != null)
                {
                    restaurant.Name = request.Name;
                    restaurant.Description = request.Description;
                    restaurant.User_Id = request.User_Id;
                    restaurant.Credits = request.Credits;
                    restaurant.Plan = request.Plan;
                    restaurant.Latitude = request.Latitude;
                    restaurant.Longitude = request.Longitude;
                    restaurant.Is_Promoted = request.Is_Promoted;
                    restaurant.Address = request.Address;

                    var result = _restaurantDAL.Update(restaurant);

                    response.Restaurant = result;
                    //response.Images = _restaurantDAL.GetRestaurantImages(request.Restaurant_Id);
                    response.Message = "Successfully updated.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No restaurant found by id: {request.Restaurant_Id}";
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
        public ActionResult<RestaurantResponse> Delete(int id)
        {
            var response = new RestaurantResponse();
            try
            {
                var restaurant = _restaurantDAL.GetRestaurant(id);

                if (restaurant != null)
                {
                    restaurant.Is_Active = false;

                    var result = _restaurantDAL.Update(restaurant);

                    response.Restaurant = new Restaurants();
                    //response.Images = new List<RestaurantImages>();
                    response.Message = "Successfully deleted.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No restaurant found by id: {id}";
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
        public ActionResult<RestaurantByIdResponse> GetRestaurant(int id)
        {
            var response = new RestaurantByIdResponse();

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

        [HttpPost("images/add"), Authorize()]
        public async Task<ActionResult<RestaurantImageResponse>> AddImage(RestaurantImageDTO request)
        {
            var response = new RestaurantImageResponse();
            try
            {
                var image = new RestaurantImages
                {
                    Restaurant_Id = request.Restaurant_Id
                };

                var result = _restaurantDAL.AddImage(image);

                var path = await UploadHelper.SaveFile(request.File, result.RestaurantImages_Id, _fromImages);

                if (!path.Contains("Error:"))
                {
                    if (!string.IsNullOrEmpty(path))
                    {
                        var host = Request.Host;
                        var scheme = Request.Scheme;

                        result.Image_Url = $"{scheme}://{host}/{path}";
                        var updateResult = _restaurantDAL.UpdateImage(result);
                    }

                    response.Image = result;
                    response.Message = "Successfully added.";
                    return Ok(response);
                }
                else
                {
                    _restaurantDAL.DeleteImage(result);
                    response.Status = "Failed";
                    response.Image = new RestaurantImages();
                    response.Message = path;
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

        [HttpPost("images/update"), Authorize()]
        public async Task<ActionResult<RestaurantImageResponse>> UpdateImage(RestaurantImageDTO request)
        {
            var response = new RestaurantImageResponse();
            try
            {
                var image = _restaurantDAL.GetRestaurantImage(request.RestaurantImages_Id);

                if (image != null)
                {
                    string deleteFileResult = string.Empty;
                    if (!string.IsNullOrEmpty(image.Image_Url))
                    {
                        deleteFileResult = UploadHelper.DeleteFile(image.Image_Url);
                    }

                    var path = await UploadHelper.SaveFile(request.File, image.RestaurantImages_Id, _fromImages);

                    if (!path.Contains("Error:"))
                    {
                        if (!string.IsNullOrEmpty(path))
                        {
                            var host = Request.Host;
                            var scheme = Request.Scheme;

                            image.Image_Url = $"{scheme}://{host}/{path}";
                        }

                        image.Restaurant_Id = request.Restaurant_Id;

                        var updateResult = _restaurantDAL.UpdateImage(image);

                        response.Image = updateResult;
                        response.Message = "Successfully updated.";
                        return Ok(response);
                    }
                    else
                    {
                        response.Image = image;
                        response.Status = "Failed";
                        response.Message = path;
                        return BadRequest(response);
                    }
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
                    string deleteFileResult = string.Empty;
                    if (!string.IsNullOrEmpty(image.Image_Url))
                    {
                        deleteFileResult = UploadHelper.DeleteFile(image.Image_Url);
                    }

                    if (!deleteFileResult.Contains("Error:"))
                    {
                        _restaurantDAL.DeleteImage(image);

                        response.Image = new RestaurantImages();
                        response.Message = "Successfully deleted.";
                        return Ok(response);
                    }
                    else
                    {
                        response.Image = image;
                        response.Status = "Failed";
                        response.Message = deleteFileResult;
                        return BadRequest(response);
                    }
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

        [HttpGet("images"), Authorize()]
        public ActionResult<RestaurantImagesResponse> GetImagesByRestaurantId(int restaurantId)
        {
            var response = new RestaurantImagesResponse();

            try
            {
                var result = _restaurantDAL.GetRestaurantImages(restaurantId);

                if (result != null && result.Count > 0)
                {
                    response.Images = result;
                    response.Message = $"Successfully get Restaurant Images.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Restaurant Images found by restaurant id: {restaurantId}";
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
        public ActionResult<RestaurantImageResponse> GetImage(int id)
        {
            var response = new RestaurantImageResponse();

            try
            {
                var result = _restaurantDAL.GetRestaurantImage(id);

                if (result != null)
                {
                    response.Image = result;
                    response.Message = $"Successfully get Restaurant Image.";

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
        public async Task<ActionResult<MenuResponse>> MenuAdd(MenuDto request)
        {
            var response = new MenuResponse();
            try
            {
                var menu = new Menus
                {
                    Establishment_Id = request.Establishment_Id,
                    Type = request.Type
                };

                var result = _restaurantDAL.Add(menu);

                var path = await UploadHelper.SaveFile(request.File, result.Menu_Id, _fromMenus);

                if (!path.Contains("Error:"))
                {
                    if (!string.IsNullOrEmpty(path))
                    {
                        var host = Request.Host;
                        var scheme = Request.Scheme;

                        result.Path = $"{scheme}://{host}/{path}";
                        var updateResult = _restaurantDAL.UpdateMenu(result);
                    }

                    response.Menu = result;
                    response.Message = "Successfully added.";
                    return Ok(response);
                }
                else
                {
                    _restaurantDAL.DeleteMenu(result);
                    response.Status = "Failed";
                    response.Menu = new Menus();
                    response.Message = path;
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

        [HttpPost("menus/update"), Authorize()]
        public async Task<ActionResult<MenuResponse>> MenuUpdate(MenuDto request)
        {
            var response = new MenuResponse();
            try
            {
                var menu = _restaurantDAL.GetMenu(request.Menu_Id);

                if (menu != null)
                {
                    string deleteFileResult = string.Empty;
                    if (!string.IsNullOrEmpty(menu.Path))
                    {
                        deleteFileResult = UploadHelper.DeleteFile(menu.Path);
                    }

                    var path = await UploadHelper.SaveFile(request.File, menu.Menu_Id, _fromMenus);

                    if (!path.Contains("Error:"))
                    {
                        if (!string.IsNullOrEmpty(path))
                        {
                            var host = Request.Host;
                            var scheme = Request.Scheme;

                            menu.Path = $"{scheme}://{host}/{path}";
                        }

                        menu.Establishment_Id = request.Establishment_Id;
                        menu.Type = request.Type;

                        var updateResult = _restaurantDAL.UpdateMenu(menu);

                        response.Menu = updateResult;
                        response.Message = "Successfully updated.";
                        return Ok(response);
                    }
                    else
                    {
                        response.Menu = menu;
                        response.Status = "Failed";
                        response.Message = path;
                        return BadRequest(response);
                    }
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
                    string deleteFileResult = string.Empty;
                    if (!string.IsNullOrEmpty(menu.Path))
                    {
                        deleteFileResult = UploadHelper.DeleteFile(menu.Path);
                    }

                    if (!deleteFileResult.Contains("Error:"))
                    {
                        _restaurantDAL.DeleteMenu(menu);

                        response.Menu = new Menus();
                        response.Message = "Successfully deleted.";
                        return Ok(response);
                    }
                    else
                    {
                        response.Menu = menu;
                        response.Status = "Failed";
                        response.Message = deleteFileResult;
                        return BadRequest(response);
                    }
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

        [HttpGet("menus/{id}"), Authorize()]
        public ActionResult<MenuResponse> GetMenu(int id)
        {
            var response = new MenuResponse();
            try
            {
                var result = _restaurantDAL.GetMenu(id);

                if (result != null)
                {
                    response.Menu = result;
                    response.Message = "Successfully get Establishment Menu.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Establishment Menu found by establishment id: {id}";
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

        [HttpGet("menus"), Authorize()]
        public ActionResult<MenusResponse> GetMenus(int establishmentId)
        {
            var response = new MenusResponse();
            try
            {
                var result = _restaurantDAL.GetMenus(establishmentId);

                if (result != null && result.Count > 0)
                {
                    response.Menus = result;
                    response.Message = "Successfully get Establishment Menus.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Establishment Menus found by establishment id: {establishmentId}";
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
                var availability = new Availability
                {
                    Establishment_Id = request.Establishment_Id,
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
                var availability = _restaurantDAL.GetAvailability(request.Availability_Id);

                if (availability != null)
                {
                    availability.Establishment_Id = request.Establishment_Id;
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
                    response.Message = $"No found Availability id: {request.Availability_Id}";

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

                    response.Availability = new Availability();
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

        [HttpGet("availabilities/{id}"), Authorize()]
        public ActionResult<RestaurantAvailabilityResponse> GetAvailability(int id)
        {
            var response = new RestaurantAvailabilityResponse();
            try
            {
                var result = _restaurantDAL.GetAvailability(id);

                if (result != null)
                {
                    response.Availability = result;

                    response.Message = "Successfully get Restaurant Availability.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Restaurant Availability found by id: {id}";
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

        [HttpGet("availabilities"), Authorize()]
        public ActionResult<RestaurantAvailabilitiesResponse> GetAvailabilityByEstablishmentId(int establishmentId)
        {
            var response = new RestaurantAvailabilitiesResponse();
            try
            {
                var result = _restaurantDAL.GetAvailabilities(establishmentId);

                if(result != null && result.Count > 0)
                {
                    response.Availabilities = result;

                    response.Message = "Successfully get Restaurant Availabilities.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Restaurant Availability found by establishment id: {establishmentId}";
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
                    Establishment_Id = request.Establishment_Id,
                    Date = request.Date,
                    Title = request.Title
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
                    nonOperatingHours.Establishment_Id = request.Establishment_Id;
                    nonOperatingHours.Date = request.Date;
                    nonOperatingHours.Title = request.Title;

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
        public ActionResult<NonOperatingHourResponse> DeleteNonOperatingHours(int id)
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
        public ActionResult<NonOperatingHoursResponse> GetNonOperatingHoursByRestaurantId(int establishmentId)
        {
            var response = new NonOperatingHoursResponse();
            try
            {
                var result = _restaurantDAL.GetNonOperatingHoursByEstablishmentId(establishmentId);

                if (result != null && result.Count > 0)
                {
                    response.NonOperatingHours = result;
                    response.Message = "Successfully get Non Operating Hours.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Non Operating Hours found by establishment id: {establishmentId}";
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
                var restaurantCuisine = new EstablishmentCuisines
                {
                    Establishment_Id = request.Establishment_Id,
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
                var cuisine = _restaurantDAL.GetEstablishmentCuisine(request.EstablishmentCuisine_Id);

                if (cuisine != null)
                {
                    cuisine.Establishment_Id = request.Establishment_Id;
                    cuisine.Name = request.Name;

                    var result = _restaurantDAL.UpdateCuisine(cuisine);

                    response.RestaurantCuisine = result;
                    response.Message = "Successfully updated.";
                    return Ok(response);
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = $"No found Cuisine id: {request.EstablishmentCuisine_Id}";

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
                var cuisine = _restaurantDAL.GetEstablishmentCuisine(id);

                if (cuisine != null)
                {
                    _restaurantDAL.DeleteCuisine(cuisine);

                    response.RestaurantCuisine = new EstablishmentCuisines();
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

        [HttpGet("cuicines/{id}"), Authorize()]
        public ActionResult<RestaurantCuisineResponse> GetCuicine(int id)
        {
            var response = new RestaurantCuisineResponse();
            try
            {
                var result = _restaurantDAL.GetEstablishmentCuisine(id);

                if (result != null)
                {
                    response.RestaurantCuisine = result;
                    response.Message = "Successfully get Establishment Cuisine.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Establishment Cuicine found by id: {id}";
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

        [HttpGet("cuicines"), Authorize()]
        public ActionResult<RestaurantCuisinesResponse> GetCuicines(int? establishmentId)
        {
            var response = new RestaurantCuisinesResponse();
            try
            {
                var result = _restaurantDAL.GetEstablishmentCuisines(establishmentId);

                if (result != null && result.Count > 0)
                {
                    response.RestaurantCuisines = result;
                    response.Message = "Successfully get Establishment Cuisines.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Establishment Cuicine found by establishment id: {establishmentId}";
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
        public ActionResult<EstablishmentBookTypeResponse> BookTypeAdd(EstablishmentBookTypeDTO request)
        {
            var response = new EstablishmentBookTypeResponse();
            try
            {
                var bookType = new EstablishmentBookType
                {
                    Establishment_Id = request.Establishment_Id,
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
        public ActionResult<EstablishmentBookTypeResponse> BookTypeUpdate(EstablishmentBookTypeDTO request)
        {
            var response = new EstablishmentBookTypeResponse();
            try
            {
                var bookType = _restaurantDAL.GetBookType(request.EstablishmentBookType_Id);

                if (bookType != null)
                {
                    bookType.Establishment_Id = request.Establishment_Id;
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
                    response.Message = $"No found BookType id: {request.EstablishmentBookType_Id}";

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
        public ActionResult<EstablishmentBookTypeResponse> BookTypeDelete(int id)
        {
            var response = new EstablishmentBookTypeResponse();
            try
            {
                var bookType = _restaurantDAL.GetBookType(id);

                if (bookType != null)
                {
                    bookType.Is_Deleted = true;

                    var result = _restaurantDAL.UpdateBookType(bookType);

                    response.BookType = new EstablishmentBookType();
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
        public ActionResult<EstablishmentBookTypesResponse> GetBookTypes(int? establishmentId)
        {
            var response = new EstablishmentBookTypesResponse();
            try
            {
                var result = _restaurantDAL.GetBookTypes(establishmentId);

                if (result != null && result.Count > 0)
                {
                    response.BookTypes = result;
                    response.Message = "Successfully get Restaurant Book types.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Restaurant Book Type found by restaurant id: {establishmentId}";
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

        [HttpGet("booktype/{id}"), Authorize()]
        public ActionResult<EstablishmentBookTypeResponse> GetBookTypes(int id)
        {
            var response = new EstablishmentBookTypeResponse();
            try
            {
                var result = _restaurantDAL.GetBookType(id);

                if (result != null)
                {
                    response.BookType = result;
                    response.Message = "Successfully get Restaurant Book type.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Restaurant Book Type found by id: {id}";
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
