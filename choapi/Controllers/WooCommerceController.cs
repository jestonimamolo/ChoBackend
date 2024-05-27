﻿using choapi.DAL;
using choapi.Messages;
using choapi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace choapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WooCommerceController : ControllerBase
    {
        private readonly IEstablishmentDAL _restaurantDAL;

        private readonly ILogger<RestaurantController> _logger;

        public WooCommerceController(ILogger<RestaurantController> logger, IEstablishmentDAL restaurantDAL)
        {
            _logger = logger;
            _restaurantDAL = restaurantDAL;
        }

        [HttpGet("restaurants")]
        public ActionResult<EstablishmentUserIdResponnse> GetRestaurants()
        {
            var response = new EstablishmentUserIdResponnse();

            try
            {
                var resultEstablishments = _restaurantDAL.GetEstablishments(null);

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

                        resultEstablishment.Images = _restaurantDAL.GetEstablishmentImages(establishment.Establishment_Id);

                        response.Establishments.Add(resultEstablishment);
                    }
                    response.Message = $"Successfully get establishments.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No establishment found.";
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

        [HttpGet("establishment/restaurants")]
        public ActionResult GetEstablishments()
        {
            var response = new EstablishmentUserIdResponnse();

            try
            {
                var resultEstablishment = _restaurantDAL.GetEstablishments(null);

                if (resultEstablishment != null && resultEstablishment.Count > 0)
                {
                    var establishments = (List<Establishment>) resultEstablishment;

                    return new JsonResult(establishments, new JsonSerializerOptions
                    {
                        ReferenceHandler = null,
                        WriteIndented = true,
                    });
                }
                else
                {
                    response.Message = $"No establishment of restaurant found.";
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
