using choapi.DAL;
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
    public class CuisineController : ControllerBase
    {
        private readonly ICuisineDAL _cuisineDAL;

        private readonly ILogger<CuisineController> _logger;

        public CuisineController(ILogger<CuisineController> logger, ICuisineDAL cuisineDAL)
        {
            _logger = logger;
            _cuisineDAL = cuisineDAL;
        }

        [HttpPost("add"), Authorize()]
        public ActionResult<CuisineResponse> AddCuisine(CuisineDTO request)
        {
            var response = new CuisineResponse();
            try
            {
                var cuisine = new Cuisines
                {
                    Title = request.Title
                };

                var result = _cuisineDAL.Add(cuisine);

                response.Cuisine = result;

                response.Message = "Successfully Cuisine added.";

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Failed";

                return BadRequest(response);
            }
        }

        [HttpGet("cuicines"), Authorize()]
        public ActionResult<CuisinesResponse> GetCuicines(int? cuisineId)
        {
            var response = new CuisinesResponse();
            try
            {
                var result = _cuisineDAL.GetCuicines(cuisineId);

                if (result != null && result.Count > 0)
                {
                    response.Cuisines = result;
                    response.Message = "Successfully get Cuisines.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Cuicine found by cuicine id: {cuisineId}";
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
