using choapi.DAL;
using choapi.DTOs;
using choapi.Helper;
using choapi.Messages;
using choapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace choapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstablishmentController : ControllerBase
    {
        private readonly IEstablishmentDAL _establishmentDAL;
        private readonly ICategoryDAL _categoryDAL;
        private readonly IUserDAL _userDAL;
        private readonly ISaveEstablishmentDAL _saveEstablishmentDAL;

        private readonly ILogger<EstablishmentController> _logger;

        private const string _fromMenus = "establishment-menus";
        private const string _fromImages = "establishment-images";

        public EstablishmentController(ILogger<EstablishmentController> logger, 
            IEstablishmentDAL establishmentDAL, 
            ICategoryDAL categoryDAL, 
            IUserDAL userDAL,
            ISaveEstablishmentDAL saveEstablishmentDAL)
        {
            _logger = logger;
            _establishmentDAL = establishmentDAL;
            _categoryDAL = categoryDAL;
            _userDAL = userDAL;
            _saveEstablishmentDAL = saveEstablishmentDAL;
        }

        [HttpPost("register"), Authorize()]
        public ActionResult<EstablishmentResponse> Register(EstablishmentDTO request)
        {
            var response = new EstablishmentResponse();
            try
            {
                if (request.Category_Id <= 0)
                {
                    response.Message = "Required Category Id.";
                    response.Status = "Failed";

                    return BadRequest(response);
                }

                var establishment = new Establishment
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
                    Promo_Credit = request.Promo_Credit,
                    Promo_Type = request.Promo_Type,
                    Payment_Card_Option = request.Payment_Card_Option,
                    Category_Id = request.Category_Id,
                    Is_Active = true
                };

                var resultEstablishment = _establishmentDAL.Add(establishment);

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

                //var resultRestaurantImages = _establishmentDAL.AddImages(restaurantImages);

                response.Establishment = resultEstablishment;
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
        public ActionResult<EstablishmentResponse> Update(EstablishmentDTO request)
        {
            var response = new EstablishmentResponse();
            try
            {
                var establishment = _establishmentDAL.GetEstablishment(request.Establishment_Id);

                if (establishment != null)
                {
                    establishment.Name = request.Name;
                    establishment.Description = request.Description;
                    establishment.User_Id = request.User_Id;
                    establishment.Credits = request.Credits;
                    establishment.Plan = request.Plan;
                    establishment.Latitude = request.Latitude;
                    establishment.Longitude = request.Longitude;
                    establishment.Is_Promoted = request.Is_Promoted;
                    establishment.Address = request.Address;
                    establishment.Promo_Credit = request.Promo_Credit;
                    establishment.Promo_Type = request.Promo_Type;
                    establishment.Payment_Card_Option = request.Payment_Card_Option;
                    establishment.Category_Id = request.Category_Id;

                    var result = _establishmentDAL.Update(establishment);

                    response.Establishment = result;
                    //response.Images = _establishmentDAL.GetRestaurantImages(request.Restaurant_Id);
                    response.Message = "Successfully updated.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Establishment found by id: {request.Establishment_Id}";
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
        public ActionResult<EstablishmentResponse> Delete(int id)
        {
            var response = new EstablishmentResponse();
            try
            {
                var establishment = _establishmentDAL.GetEstablishment(id);

                if (establishment != null)
                {
                    establishment.Is_Active = false;

                    var result = _establishmentDAL.Update(establishment);

                    response.Establishment = new Establishment();
                    //response.Images = new List<RestaurantImages>();
                    response.Message = "Successfully deleted.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Establishment found by id: {id}";
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
        public ActionResult<EstablishmentByIdResponse> GetEstablishment(int id)
        {
            var response = new EstablishmentByIdResponse();

            try
            {
                var resultEstablishment = _establishmentDAL.GetEstablishment(id);

                if (resultEstablishment != null)
                {
                    response.Establishment = resultEstablishment;

                    response.Images = _establishmentDAL.GetEstablishmentImages(resultEstablishment.Establishment_Id);

                    response.Message = $"Successfully get Establishment.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Establishment found by establishment id: {id}";
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

        [HttpGet("establishments"), Authorize()]
        public ActionResult<EstablishmentUserIdResponnse> GetEstablishments(int? userId)
        {
            var response = new EstablishmentUserIdResponnse();

            try
            {
                var resultEstablishments = _establishmentDAL.GetEstablishments(userId);

                if (resultEstablishments != null && resultEstablishments.Count > 0)
                {
                    foreach (var establishment in resultEstablishments)
                    {
                        var resultEstablishment = new EstablishmentReponse();

                        resultEstablishment.Establishment_Id = establishment.Establishment_Id;
                        resultEstablishment.Name = establishment.Name;
                        resultEstablishment.Description = establishment.Description;
                        resultEstablishment.User_Id = establishment.User_Id;
                        resultEstablishment.Credits = establishment.Credits;
                        resultEstablishment.Plan = establishment.Plan;
                        resultEstablishment.Latitude = establishment.Latitude;
                        resultEstablishment.Longitude = establishment.Longitude;
                        resultEstablishment.Is_Promoted = establishment.Is_Promoted;
                        resultEstablishment.Address = establishment.Address;
                        resultEstablishment.Is_Active = establishment.Is_Active;

                        resultEstablishment.Images = _establishmentDAL.GetEstablishmentImages(establishment.Establishment_Id);

                        response.Establishments.Add(resultEstablishment);
                    }
                    response.Message = $"Successfully get Establishments.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Establishment found by user id: {userId}";
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
        public async Task<ActionResult<EstablishmentImageResponse>> AddImage(EstablishmentImageDTO request)
        {
            var response = new EstablishmentImageResponse();
            try
            {
                var image = new EstablishmentImages
                {
                    Establishment_Id = request.Establishment_Id
                };

                var result = _establishmentDAL.AddImage(image);

                var path = await UploadHelper.SaveFile(request.File, result.EstablishmentImage_Id, _fromImages);

                if (!path.Contains("Error:"))
                {
                    if (!string.IsNullOrEmpty(path))
                    {
                        var host = Request.Host;
                        var scheme = Request.Scheme;

                        result.Image_Url = $"{scheme}://{host}/{path}";
                        var updateResult = _establishmentDAL.UpdateImage(result);
                    }

                    response.Image = result;
                    response.Message = "Successfully added.";
                    return Ok(response);
                }
                else
                {
                    _establishmentDAL.DeleteImage(result);
                    response.Status = "Failed";
                    response.Image = new EstablishmentImages();
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
        public async Task<ActionResult<EstablishmentImageResponse>> UpdateImage(EstablishmentImageDTO request)
        {
            var response = new EstablishmentImageResponse();
            try
            {
                var image = _establishmentDAL.GetEstablishmentImage(request.EstablishmentImage_Id);

                if (image != null)
                {
                    string deleteFileResult = string.Empty;
                    if (!string.IsNullOrEmpty(image.Image_Url))
                    {
                        deleteFileResult = UploadHelper.DeleteFile(image.Image_Url);
                    }

                    var path = await UploadHelper.SaveFile(request.File, image.EstablishmentImage_Id, _fromImages);

                    if (!path.Contains("Error:"))
                    {
                        if (!string.IsNullOrEmpty(path))
                        {
                            var host = Request.Host;
                            var scheme = Request.Scheme;

                            image.Image_Url = $"{scheme}://{host}/{path}";
                        }

                        image.Establishment_Id = request.Establishment_Id;

                        var updateResult = _establishmentDAL.UpdateImage(image);

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
                    response.Message = $"No Establishment images found by id: {request.EstablishmentImage_Id}";
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
        public ActionResult<EstablishmentImageResponse> DeleteImage(int id)
        {
            var response = new EstablishmentImageResponse();
            try
            {
                var image = _establishmentDAL.GetEstablishmentImage(id);

                if (image != null)
                {
                    string deleteFileResult = string.Empty;
                    if (!string.IsNullOrEmpty(image.Image_Url))
                    {
                        deleteFileResult = UploadHelper.DeleteFile(image.Image_Url);
                    }

                    if (!deleteFileResult.Contains("Error:"))
                    {
                        _establishmentDAL.DeleteImage(image);

                        response.Image = new EstablishmentImages();
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
                    response.Message = $"No Establishment images found by id: {id}";
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
        public ActionResult<EstablishmentImagesResponse> GetImagesByEstablishmentId(int establishmentId)
        {
            var response = new EstablishmentImagesResponse();

            try
            {
                var result = _establishmentDAL.GetEstablishmentImages(establishmentId);

                if (result != null && result.Count > 0)
                {
                    response.Images = result;
                    response.Message = $"Successfully get Establishment Images.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Establishment Images found by establishment id: {establishmentId}";
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
        public ActionResult<EstablishmentImageResponse> GetImage(int id)
        {
            var response = new EstablishmentImageResponse();

            try
            {
                var result = _establishmentDAL.GetEstablishmentImage(id);

                if (result != null)
                {
                    response.Image = result;
                    response.Message = $"Successfully get Establishment Image.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Establishment I mages found by id: {id}";
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

                var result = _establishmentDAL.Add(menu);

                var path = await UploadHelper.SaveFile(request.File, result.Menu_Id, _fromMenus);

                if (!path.Contains("Error:"))
                {
                    if (!string.IsNullOrEmpty(path))
                    {
                        var host = Request.Host;
                        var scheme = Request.Scheme;

                        result.Path = $"{scheme}://{host}/{path}";
                        var updateResult = _establishmentDAL.UpdateMenu(result);
                    }

                    response.Menu = result;
                    response.Message = "Successfully added.";
                    return Ok(response);
                }
                else
                {
                    _establishmentDAL.DeleteMenu(result);
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
                var menu = _establishmentDAL.GetMenu(request.Menu_Id);

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

                        var updateResult = _establishmentDAL.UpdateMenu(menu);

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
                var menu = _establishmentDAL.GetMenu(id);

                if (menu != null)
                {
                    string deleteFileResult = string.Empty;
                    if (!string.IsNullOrEmpty(menu.Path))
                    {
                        deleteFileResult = UploadHelper.DeleteFile(menu.Path);
                    }

                    if (!deleteFileResult.Contains("Error:"))
                    {
                        _establishmentDAL.DeleteMenu(menu);

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
                var result = _establishmentDAL.GetMenu(id);

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
                var result = _establishmentDAL.GetMenus(establishmentId);

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
        public ActionResult<EstablishmentAvailabilityResponse> AvailabilityAdd(EstablishmentAvailabilityDTO request)
        {
            var response = new EstablishmentAvailabilityResponse();
            try
            {
                var availability = new Availability
                {
                    Establishment_Id = request.Establishment_Id,
                    Day = request.Day,
                    Time_Start = request.Time_Start,
                    Time_End = request.Time_End
                };

                var result = _establishmentDAL.Add(availability);

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
        public ActionResult<EstablishmentAvailabilityResponse> AvailabilityUpdate(EstablishmentAvailabilityDTO request)
        {
            var response = new EstablishmentAvailabilityResponse();
            try
            {
                var availability = _establishmentDAL.GetAvailability(request.Availability_Id);

                if (availability != null)
                {
                    availability.Establishment_Id = request.Establishment_Id;
                    availability.Day = request.Day;
                    availability.Time_Start = request.Time_Start;
                    availability.Time_End = request.Time_End;

                    var result = _establishmentDAL.UpdateAvailability(availability);

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
        public ActionResult<EstablishmentAvailabilityResponse> AvailabilityDelete(int id)
        {
            var response = new EstablishmentAvailabilityResponse();
            try
            {
                var availability = _establishmentDAL.GetAvailability(id);

                if (availability != null)
                {
                    _establishmentDAL.DeleteAvailability(availability);

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
        public ActionResult<EstablishmentAvailabilityResponse> GetAvailability(int id)
        {
            var response = new EstablishmentAvailabilityResponse();
            try
            {
                var result = _establishmentDAL.GetAvailability(id);

                if (result != null)
                {
                    response.Availability = result;

                    response.Message = "Successfully get Establishment Availability.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Establishment Availability found by id: {id}";
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
        public ActionResult<EstablishmentAvailabilitiesResponse> GetAvailabilityByEstablishmentId(int establishmentId)
        {
            var response = new EstablishmentAvailabilitiesResponse();
            try
            {
                var result = _establishmentDAL.GetAvailabilities(establishmentId);

                if(result != null && result.Count > 0)
                {
                    response.Availabilities = result;

                    response.Message = "Successfully get Establishment Availabilities.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Establishment Availability found by establishment id: {establishmentId}";
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

                var result = _establishmentDAL.Add(nonOperatingHours);

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
                var nonOperatingHours = _establishmentDAL.GetNonOperatingHours(request.NonOperatingHours_Id);

                if (nonOperatingHours != null)
                {
                    nonOperatingHours.Establishment_Id = request.Establishment_Id;
                    nonOperatingHours.Date = request.Date;
                    nonOperatingHours.Title = request.Title;

                    var result = _establishmentDAL.Update(nonOperatingHours);

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
                var nonOperatingHours = _establishmentDAL.GetNonOperatingHours(id);

                if (nonOperatingHours != null)
                {
                    _establishmentDAL.Delete(nonOperatingHours);

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
                var result = _establishmentDAL.GetNonOperatingHours(id);

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
        public ActionResult<NonOperatingHoursResponse> GetNonOperatingHoursByEstablishmentId(int establishmentId)
        {
            var response = new NonOperatingHoursResponse();
            try
            {
                var result = _establishmentDAL.GetNonOperatingHoursByEstablishmentId(establishmentId);

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
        public ActionResult<EstablishmentCuisineResponse> CuisinesAdd(EstablishmentCuisineDTO request)
        {
            var response = new EstablishmentCuisineResponse();
            try
            {
                var establishmentCuisine = new EstablishmentCuisines
                {
                    Establishment_Id = request.Establishment_Id,
                    Name = request.Name
                };

                var result = _establishmentDAL.Add(establishmentCuisine);

                response.EstablishmentCuisine = result;

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
        public ActionResult<EstablishmentCuisineResponse> CuisineUpdate(EstablishmentCuisineDTO request)
        {
            var response = new EstablishmentCuisineResponse();
            try
            {
                var cuisine = _establishmentDAL.GetEstablishmentCuisine(request.EstablishmentCuisine_Id);

                if (cuisine != null)
                {
                    cuisine.Establishment_Id = request.Establishment_Id;
                    cuisine.Name = request.Name;

                    var result = _establishmentDAL.UpdateCuisine(cuisine);

                    response.EstablishmentCuisine = result;
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
        public ActionResult<EstablishmentCuisineResponse> CuisineDelete(int id)
        {
            var response = new EstablishmentCuisineResponse();
            try
            {
                var cuisine = _establishmentDAL.GetEstablishmentCuisine(id);

                if (cuisine != null)
                {
                    _establishmentDAL.DeleteCuisine(cuisine);

                    response.EstablishmentCuisine = new EstablishmentCuisines();
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
        public ActionResult<EstablishmentCuisineResponse> GetCuicine(int id)
        {
            var response = new EstablishmentCuisineResponse();
            try
            {
                var result = _establishmentDAL.GetEstablishmentCuisine(id);

                if (result != null)
                {
                    response.EstablishmentCuisine = result;
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
        public ActionResult<EstablishmentCuisinesResponse> GetCuicines(int? establishmentId)
        {
            var response = new EstablishmentCuisinesResponse();
            try
            {
                var result = _establishmentDAL.GetEstablishmentCuisines(establishmentId);

                if (result != null && result.Count > 0)
                {
                    response.EstablishmentCuisines = result;
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

                var result = _establishmentDAL.Add(bookType);

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
                var bookType = _establishmentDAL.GetBookType(request.EstablishmentBookType_Id);

                if (bookType != null)
                {
                    bookType.Establishment_Id = request.Establishment_Id;
                    bookType.Is_Payable = request.Is_Payable;
                    bookType.Name = request.Name;
                    bookType.Currency = request.Currency;
                    bookType.Price = request.Price;

                    var result = _establishmentDAL.UpdateBookType(bookType);

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
                var bookType = _establishmentDAL.GetBookType(id);

                if (bookType != null)
                {
                    bookType.Is_Deleted = true;

                    var result = _establishmentDAL.UpdateBookType(bookType);

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
                var result = _establishmentDAL.GetBookTypes(establishmentId);

                if (result != null && result.Count > 0)
                {
                    response.BookTypes = result;
                    response.Message = "Successfully get Establishment Book types.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Establishment Book Type found by establishment id: {establishmentId}";
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
                var result = _establishmentDAL.GetBookType(id);

                if (result != null)
                {
                    response.BookType = result;
                    response.Message = "Successfully get Establishment Book type.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Establishment Book Type found by id: {id}";
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
        public ActionResult<EstablishmentByIdResponse> GetRestaurant(int id)
        {
            var response = new EstablishmentByIdResponse();

            try
            {
                var restaurantCategory = _categoryDAL.GetByName("Restaurant");
                if (restaurantCategory == null)
                {
                    response.Message = $"Category of restaurant no found.";
                    response.Status = "Failed";
                    return BadRequest(response);
                }

                var resultEstablishment = _establishmentDAL.GetRestaurant(restaurantCategory.Category_Id, id);

                if (resultEstablishment != null)
                {
                    response.Establishment = resultEstablishment;

                    response.Images = _establishmentDAL.GetEstablishmentImages(resultEstablishment.Establishment_Id);

                    response.Message = $"Successfully get Restaurant.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Restaurant found by restaurant id: {id}";
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

        [HttpGet("restaurants/user"), Authorize()]
        public ActionResult<EstablishmentUserIdResponnse> GetRestaurantsByUser(int? id)
        {
            var response = new EstablishmentUserIdResponnse();

            try
            {
                var restaurantCategory = _categoryDAL.GetByName("Restaurant");
                if (restaurantCategory == null)
                {
                    response.Message = $"Category of restaurant no found.";
                    response.Status = "Failed";
                    return BadRequest(response);
                }

                var resultEstablishments = _establishmentDAL.GetRestaurants(restaurantCategory.Category_Id, id);

                if (resultEstablishments != null && resultEstablishments.Count > 0)
                {
                    foreach (var establishment in resultEstablishments)
                    {
                        var resultEstablishment = new EstablishmentReponse();

                        resultEstablishment.Establishment_Id = establishment.Establishment_Id;
                        resultEstablishment.Name = establishment.Name;
                        resultEstablishment.Description = establishment.Description;
                        resultEstablishment.User_Id = establishment.User_Id;
                        resultEstablishment.Credits = establishment.Credits;
                        resultEstablishment.Plan = establishment.Plan;
                        resultEstablishment.Latitude = establishment.Latitude;
                        resultEstablishment.Longitude = establishment.Longitude;
                        resultEstablishment.Is_Promoted = establishment.Is_Promoted;
                        resultEstablishment.Address = establishment.Address;
                        resultEstablishment.Is_Active = establishment.Is_Active;

                        resultEstablishment.Save_User = _saveEstablishmentDAL.GetSaveEstablishmentOfUser(establishment.Establishment_Id);

                        resultEstablishment.Images = _establishmentDAL.GetEstablishmentImages(establishment.Establishment_Id);

                        response.Establishments.Add(resultEstablishment);
                    }
                    response.Message = $"Successfully get Restaurants.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Restaurant found by user id: {id}";
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
        public ActionResult<EstablishmentUserIdResponnse> GetRestaurants()
        {
            var response = new EstablishmentUserIdResponnse();

            try
            {
                var restaurantCategory = _categoryDAL.GetByName("Restaurant");
                if (restaurantCategory == null)
                {
                    response.Message = $"Category of restaurant no found.";
                    response.Status = "Failed";
                    return BadRequest(response);
                }

                var resultEstablishments = _establishmentDAL.GetRestaurants(restaurantCategory.Category_Id, null);

                if (resultEstablishments != null && resultEstablishments.Count > 0)
                {
                    foreach (var establishment in resultEstablishments)
                    {
                        var resultEstablishment = new EstablishmentReponse();

                        resultEstablishment.Establishment_Id = establishment.Establishment_Id;
                        resultEstablishment.Name = establishment.Name;
                        resultEstablishment.Description = establishment.Description;
                        resultEstablishment.User_Id = establishment.User_Id;
                        resultEstablishment.Credits = establishment.Credits;
                        resultEstablishment.Plan = establishment.Plan;
                        resultEstablishment.Latitude = establishment.Latitude;
                        resultEstablishment.Longitude = establishment.Longitude;
                        resultEstablishment.Is_Promoted = establishment.Is_Promoted;
                        resultEstablishment.Address = establishment.Address;
                        resultEstablishment.Is_Active = establishment.Is_Active;

                        resultEstablishment.Save_User = _saveEstablishmentDAL.GetSaveEstablishmentOfUser(establishment.Establishment_Id);

                        resultEstablishment.Images = _establishmentDAL.GetEstablishmentImages(establishment.Establishment_Id);

                        response.Establishments.Add(resultEstablishment);
                    }
                    response.Message = $"Successfully get Restaurants.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Restaurant found.";
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

        [HttpGet("distance/restaurants/device/location"), Authorize()]
        public ActionResult<EstablishmentUserIdResponnse> GetDistanceRestaurantsUser(double latitude, double longitude)
        {
            var response = new EstablishmentUserIdResponnse();

            try
            {
                var restaurantCategory = _categoryDAL.GetByName("Restaurant");
                if (restaurantCategory == null)
                {
                    response.Message = $"Category of restaurant no found.";
                    response.Status = "Failed";
                    return BadRequest(response);
                }

                var resultEstablishments = _establishmentDAL.GetEstablishmentsByCategoryId(restaurantCategory.Category_Id);

                if (resultEstablishments != null && resultEstablishments.Count > 0)
                {
                    foreach (var establishment in resultEstablishments)
                    {
                        var resultEstablishment = new EstablishmentReponse();

                        resultEstablishment.Establishment_Id = establishment.Establishment_Id;
                        resultEstablishment.Name = establishment.Name;
                        resultEstablishment.Description = establishment.Description;
                        resultEstablishment.User_Id = establishment.User_Id;
                        resultEstablishment.Credits = establishment.Credits;
                        resultEstablishment.Plan = establishment.Plan;
                        resultEstablishment.Latitude = establishment.Latitude;
                        resultEstablishment.Longitude = establishment.Longitude;
                        resultEstablishment.Is_Promoted = establishment.Is_Promoted;
                        resultEstablishment.Address = establishment.Address;
                        resultEstablishment.Is_Active = establishment.Is_Active;

                        if (establishment.Latitude != null || establishment.Longitude != null)
                        {
                            double userLat = latitude;
                            double userLong = longitude;
                            double establishmentLat = (double)(establishment.Latitude == null ? 0 : establishment.Latitude);
                            double establishmentLong = (double)(establishment.Longitude == null ? 0 : establishment.Longitude);
                            resultEstablishment.Distance = CalculateDistance(userLat, userLong, establishmentLat, establishmentLong);
                        }

                        resultEstablishment.Images = _establishmentDAL.GetEstablishmentImages(establishment.Establishment_Id);

                        response.Establishments.Add(resultEstablishment);
                    }
                    response.Message = $"Successfully get Restaurants.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Restaurants found";
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

        [HttpGet("distance/restaurants/user/{id}"), Authorize()]
        public ActionResult<EstablishmentUserIdResponnse> GetDistanceRestaurantsUser(int id)
        {
            var response = new EstablishmentUserIdResponnse();

            try
            {
                var restaurantCategory = _categoryDAL.GetByName("Restaurant");
                if (restaurantCategory == null)
                {
                    response.Message = $"Category of restaurant no found.";
                    response.Status = "Failed";
                    return BadRequest(response);
                }

                var user = _userDAL.GetUser(id);
                if (user == null)
                {
                    response.Message = $"User no found.";
                    response.Status = "Failed";
                    return BadRequest(response);
                }

                if (user.Latitude == null || user.Longitude == null)
                {
                    response.Message = $"User no location set.";
                    response.Status = "Failed";
                    return BadRequest(response);
                }

                var resultEstablishments = _establishmentDAL.GetEstablishmentsByCategoryId(restaurantCategory.Category_Id);

                if (resultEstablishments != null && resultEstablishments.Count > 0)
                {
                    foreach (var establishment in resultEstablishments)
                    {
                        var resultEstablishment = new EstablishmentReponse();

                        resultEstablishment.Establishment_Id = establishment.Establishment_Id;
                        resultEstablishment.Name = establishment.Name;
                        resultEstablishment.Description = establishment.Description;
                        resultEstablishment.User_Id = establishment.User_Id;
                        resultEstablishment.Credits = establishment.Credits;
                        resultEstablishment.Plan = establishment.Plan;
                        resultEstablishment.Latitude = establishment.Latitude;
                        resultEstablishment.Longitude = establishment.Longitude;
                        resultEstablishment.Is_Promoted = establishment.Is_Promoted;
                        resultEstablishment.Address = establishment.Address;
                        resultEstablishment.Is_Active = establishment.Is_Active;

                        if (establishment.Latitude != null || establishment.Longitude != null)
                        {
                            double userLat = (double)(user.Latitude == null ? 0 : user.Latitude);
                            double userLong = (double)(user.Longitude == null ? 0 : user.Longitude);
                            double establishmentLat = (double)(establishment.Latitude == null ? 0 : establishment.Latitude);
                            double establishmentLong = (double)(establishment.Longitude == null ? 0 : establishment.Longitude);
                            resultEstablishment.Distance = CalculateDistance(userLat, userLong, establishmentLat, establishmentLong);
                        }

                        resultEstablishment.Images = _establishmentDAL.GetEstablishmentImages(establishment.Establishment_Id);

                        response.Establishments.Add(resultEstablishment);
                    }
                    response.Message = $"Successfully get Restaurants.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Restaurants found";
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

        [HttpGet("distance/restaurants/promoted/user/{id}"), Authorize()]
        public ActionResult<EstablishmentUserIdResponnse> GetDistanceRestaurantsPromotedUser(int id)
        {
            var response = new EstablishmentUserIdResponnse();

            try
            {
                var restaurantCategory = _categoryDAL.GetByName("Restaurant");
                if (restaurantCategory == null)
                {
                    response.Message = $"Category of restaurant no found.";
                    response.Status = "Failed";
                    return BadRequest(response);
                }

                var user = _userDAL.GetUser(id);
                if (user == null)
                {
                    response.Message = $"User no found.";
                    response.Status = "Failed";
                    return BadRequest(response);
                }

                if (user.Latitude == null || user.Longitude == null)
                {
                    response.Message = $"User no location set.";
                    response.Status = "Failed";
                    return BadRequest(response);
                }

                var resultEstablishments = _establishmentDAL.GetEstablishmentsPromotedByCategoryId(restaurantCategory.Category_Id, true);

                if (resultEstablishments != null && resultEstablishments.Count > 0)
                {
                    foreach (var establishment in resultEstablishments)
                    {
                        var resultEstablishment = new EstablishmentReponse();

                        resultEstablishment.Establishment_Id = establishment.Establishment_Id;
                        resultEstablishment.Name = establishment.Name;
                        resultEstablishment.Description = establishment.Description;
                        resultEstablishment.User_Id = establishment.User_Id;
                        resultEstablishment.Credits = establishment.Credits;
                        resultEstablishment.Plan = establishment.Plan;
                        resultEstablishment.Latitude = establishment.Latitude;
                        resultEstablishment.Longitude = establishment.Longitude;
                        resultEstablishment.Is_Promoted = establishment.Is_Promoted;
                        resultEstablishment.Address = establishment.Address;
                        resultEstablishment.Is_Active = establishment.Is_Active;

                        if (establishment.Latitude != null || establishment.Longitude != null)
                        {
                            double userLat = (double)(user.Latitude == null ? 0 : user.Latitude);
                            double userLong = (double)(user.Longitude == null ? 0 : user.Longitude);
                            double establishmentLat = (double)(establishment.Latitude == null ? 0 : establishment.Latitude);
                            double establishmentLong = (double)(establishment.Longitude == null ? 0 : establishment.Longitude);
                            resultEstablishment.Distance = CalculateDistance(userLat, userLong, establishmentLat, establishmentLong);
                        }

                        resultEstablishment.Images = _establishmentDAL.GetEstablishmentImages(establishment.Establishment_Id);

                        response.Establishments.Add(resultEstablishment);
                    }
                    response.Message = $"Successfully get Restaurants.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Restaurants found";
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

        [HttpGet("distance/restaurants/unpromoted/user/{id}"), Authorize()]
        public ActionResult<EstablishmentUserIdResponnse> GetDistanceRestaurantsUnPromotedUser(int id)
        {
            var response = new EstablishmentUserIdResponnse();

            try
            {
                var restaurantCategory = _categoryDAL.GetByName("Restaurant");
                if (restaurantCategory == null)
                {
                    response.Message = $"Category of restaurant no found.";
                    response.Status = "Failed";
                    return BadRequest(response);
                }

                var user = _userDAL.GetUser(id);
                if (user == null)
                {
                    response.Message = $"User no found.";
                    response.Status = "Failed";
                    return BadRequest(response);
                }

                if (user.Latitude == null || user.Longitude == null)
                {
                    response.Message = $"User no location set.";
                    response.Status = "Failed";
                    return BadRequest(response);
                }

                var resultEstablishments = _establishmentDAL.GetEstablishmentsPromotedByCategoryId(restaurantCategory.Category_Id, false);

                if (resultEstablishments != null && resultEstablishments.Count > 0)
                {
                    foreach (var establishment in resultEstablishments)
                    {
                        var resultEstablishment = new EstablishmentReponse();

                        resultEstablishment.Establishment_Id = establishment.Establishment_Id;
                        resultEstablishment.Name = establishment.Name;
                        resultEstablishment.Description = establishment.Description;
                        resultEstablishment.User_Id = establishment.User_Id;
                        resultEstablishment.Credits = establishment.Credits;
                        resultEstablishment.Plan = establishment.Plan;
                        resultEstablishment.Latitude = establishment.Latitude;
                        resultEstablishment.Longitude = establishment.Longitude;
                        resultEstablishment.Is_Promoted = establishment.Is_Promoted;
                        resultEstablishment.Address = establishment.Address;
                        resultEstablishment.Is_Active = establishment.Is_Active;

                        if (establishment.Latitude != null || establishment.Longitude != null)
                        {
                            double userLat = (double)(user.Latitude == null ? 0 : user.Latitude);
                            double userLong = (double)(user.Longitude == null ? 0 : user.Longitude);
                            double establishmentLat = (double)(establishment.Latitude == null ? 0 : establishment.Latitude);
                            double establishmentLong = (double)(establishment.Longitude == null ? 0 : establishment.Longitude);
                            resultEstablishment.Distance = CalculateDistance(userLat, userLong, establishmentLat, establishmentLong);
                        }

                        resultEstablishment.Images = _establishmentDAL.GetEstablishmentImages(establishment.Establishment_Id);

                        response.Establishments.Add(resultEstablishment);
                    }
                    response.Message = $"Successfully get Restaurants.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Restaurants found";
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

        [HttpGet("restaurants/manager/{id}"), Authorize()]
        public ActionResult<EstablishmentUserIdResponnse> GetRestaurantsByManager(int id)
        {
            var response = new EstablishmentUserIdResponnse();

            try
            {
                var restaurantCategory = _categoryDAL.GetByName("Restaurant");
                if (restaurantCategory == null)
                {
                    response.Message = $"Category of restaurant no found.";
                    response.Status = "Failed";
                    return BadRequest(response);
                }

                var resultEstablishments = _establishmentDAL.GetRestaurants(restaurantCategory.Category_Id, id);

                if (resultEstablishments != null && resultEstablishments.Count > 0)
                {
                    foreach (var establishment in resultEstablishments)
                    {
                        var resultEstablishment = new EstablishmentReponse();

                        resultEstablishment.Establishment_Id = establishment.Establishment_Id;
                        resultEstablishment.Name = establishment.Name;
                        resultEstablishment.Description = establishment.Description;
                        resultEstablishment.User_Id = establishment.User_Id;
                        resultEstablishment.Credits = establishment.Credits;
                        resultEstablishment.Plan = establishment.Plan;
                        resultEstablishment.Latitude = establishment.Latitude;
                        resultEstablishment.Longitude = establishment.Longitude;
                        resultEstablishment.Is_Promoted = establishment.Is_Promoted;
                        resultEstablishment.Address = establishment.Address;
                        resultEstablishment.Is_Active = establishment.Is_Active;

                        resultEstablishment.Images = _establishmentDAL.GetEstablishmentImages(establishment.Establishment_Id);

                        response.Establishments.Add(resultEstablishment);
                    }
                    response.Message = $"Successfully get Restaurants.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Restaurant found by manager id: {id}";
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

        [HttpGet("restaurants/search"), Authorize()]
        public ActionResult<EstablishmentUserIdResponnse> GetRestaurantsSearch(string keywords)
        {
            var response = new EstablishmentUserIdResponnse();

            try
            {
                var restaurantCategory = _categoryDAL.GetByName("Restaurant");
                if (restaurantCategory == null)
                {
                    response.Message = $"Category of restaurant no found.";
                    response.Status = "Failed";
                    return BadRequest(response);
                }

                if (string.IsNullOrEmpty(keywords))
                {
                    response.Message = $"Search not found.";
                    return Ok(response);
                }

                var resultEstablishments = _establishmentDAL.GetRestaurantsSearch(restaurantCategory.Category_Id, keywords);

                if (resultEstablishments != null && resultEstablishments.Count > 0)
                {
                    foreach (var establishment in resultEstablishments)
                    {
                        var resultEstablishment = new EstablishmentReponse();

                        resultEstablishment.Establishment_Id = establishment.Establishment_Id;
                        resultEstablishment.Name = establishment.Name;
                        resultEstablishment.Description = establishment.Description;
                        resultEstablishment.User_Id = establishment.User_Id;
                        resultEstablishment.Credits = establishment.Credits;
                        resultEstablishment.Plan = establishment.Plan;
                        resultEstablishment.Latitude = establishment.Latitude;
                        resultEstablishment.Longitude = establishment.Longitude;
                        resultEstablishment.Is_Promoted = establishment.Is_Promoted;
                        resultEstablishment.Address = establishment.Address;
                        resultEstablishment.Is_Active = establishment.Is_Active;

                        resultEstablishment.Images = _establishmentDAL.GetEstablishmentImages(establishment.Establishment_Id);

                        response.Establishments.Add(resultEstablishment);
                    }
                    response.Message = $"Successfully get Restaurants.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Restaurant found by searching: {keywords}";
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

        private double CalculateDistance(double userLatitude, double userLongitude, double establishmentLatitude, double establishmentLongitude)
        {
            var d1 = userLatitude * (Math.PI / 180.0);
            var num1 = userLongitude * (Math.PI / 180.0);
            var d2 = establishmentLatitude * (Math.PI / 180.0);
            var num2 = establishmentLongitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                     Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
            var meters = 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
            return meters / 1000;
        }
    }
}
