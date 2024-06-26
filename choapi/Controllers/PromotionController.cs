﻿using choapi.DAL;
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
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionDAL _modelDAL;
        private readonly IEstablishmentDAL _establishmentDAL;

        private readonly ILogger<PromotionController> _logger;

        public PromotionController(ILogger<PromotionController> logger, IPromotionDAL modelDAL, IEstablishmentDAL establishmentDAL)
        {
            _logger = logger;
            _modelDAL = modelDAL;
            _establishmentDAL = establishmentDAL;
        }

        [HttpPost("add"), Authorize()]
        public ActionResult<PromotionResponse> Add(PromotionDTO request)
        {
            var response = new PromotionResponse();
            try
            {
                if (request.Establishment_Id <= 0)
                {
                    response.Message = "Required Establishment Id.";
                    response.Status = "Failed";

                    return BadRequest(response);
                }

                var model = new Promotion
                {
                    Establishment_Id = request.Establishment_Id,
                    Promotion_Details = request.Promotion_Details,
                    Date_Promoted = request.Date_Promoted,
                    Is_Active = request.Is_Active,
                };

                var result = _modelDAL.Add(model);

                //var establishment = _establishmentDAL.GetEstablishment(model.Establishment_Id);

                //if (establishment != null)
                //{
                //    if (establishment.Promo_Credit != null)
                //        establishment.Promo_Credit += 1;
                //    else
                //        establishment.Promo_Credit = 1;

                //    var resultPromotCreditEstablishment = _establishmentDAL.Update(establishment);
                //}

                response.Promotion = result;
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
        public ActionResult<PromotionResponse> Update(PromotionDTO request)
        {
            var response = new PromotionResponse();
            try
            {
                var model = _modelDAL.Get(request.Promotion_Id);

                if (model != null)
                {
                    model.Establishment_Id = request.Establishment_Id;
                    model.Promotion_Details = request.Promotion_Details;
                    model.Date_Promoted = request.Date_Promoted;
                    model.Is_Active = request.Is_Active;

                    var result = _modelDAL.Update(model);

                    response.Promotion = result;
                    response.Message = "Successfully updated.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No promotion found by id: {request.Promotion_Id}";
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
        public ActionResult<PromotionResponse> Delete(int id)
        {
            var response = new PromotionResponse();
            try
            {
                var model = _modelDAL.Get(id);

                if (model != null)
                {
                    model.Is_Deleted = true;

                    var result = _modelDAL.Delete(model);

                    response.Message = "Successfully deleted.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No promotion found by id: {id}";
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
        public ActionResult<PromotionResponse> GetPromotion(int id)
        {
            var response = new PromotionResponse();
            try
            {
                var result = _modelDAL.Get(id);

                if (result != null)
                {
                    response.Promotion = result;
                    response.Message = "Successfully get promotion.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No promotion found by id: {id}";
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
        public ActionResult<PromotionsResponse> GetManagerByEstablishmentId(int id)
        {
            var response = new PromotionsResponse();
            try
            {
                var result = _modelDAL.GetByEstablishmentId(id);

                if (result != null && result.Count > 0)
                {
                    response.Promotions = result;
                    response.Message = "Successfully get Promotions.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Promotion found by establishment id: {id}";
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
