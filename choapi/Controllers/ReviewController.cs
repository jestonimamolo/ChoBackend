using choapi.DAL;
using choapi.DTOs;
using choapi.Messages;
using choapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace choapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewDAL _modelDAL;

        private readonly ILogger<ReviewController> _logger;

        private const string _entityName = "Review";

        public ReviewController(ILogger<ReviewController> logger, IReviewDAL modelDAL)
        {
            _logger = logger;
            _modelDAL = modelDAL;
        }

        [HttpPost("add"), Authorize()]
        public ActionResult<ReviewResponse> Add(ReviewDTO request)
        {
            var response = new ReviewResponse();
            try
            {
                if (request.User_Id <= 0)
                {
                    response.Message = $"Required User Id.";
                    response.Status = "Failed";

                    return BadRequest(response);
                }

                if (request.Establishment_Id <= 0)
                {
                    response.Message = $"Required Establishment Id.";
                    response.Status = "Failed";

                    return BadRequest(response);
                }

                var model = new Review
                {
                    Establishment_Id = request.Establishment_Id,
                    User_Id = request.User_Id,
                    Comment = request.Comment,
                    Date_Added = request.Date_Added,
                    Status = request.Status
                };

                var result = _modelDAL.Add(model);

                response.Review = result;
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
        public ActionResult<ReviewResponse> Update(ReviewDTO request)
        {
            var response = new ReviewResponse();
            try
            {
                var model = _modelDAL.Get(request.Review_Id);

                if (model != null)
                {
                    model.Establishment_Id = request.Establishment_Id;
                    model.User_Id = request.User_Id;
                    model.Comment = request.Comment;
                    model.Status = request.Status;

                    var result = _modelDAL.Update(model);

                    response.Review = result;
                    response.Message = "Successfully updated.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No {_entityName} found by id: {request.Review_Id}";
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
        public ActionResult<ReviewResponse> Delete(int id)
        {
            var response = new ReviewResponse();
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
                    response.Message = $"No {_entityName} found by id: {id}";
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
        public ActionResult<ReviewResponse> Get(int id)
        {
            var response = new ReviewResponse();
            try
            {
                var result = _modelDAL.Get(id);

                if (result != null)
                {
                    response.Review = result;
                    response.Message = $"Successfully get {_entityName}.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No {_entityName} found by id: {id}";
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

        [HttpGet("user/{id}"), Authorize()]
        public ActionResult<ReviewsResponse> GetByUser(int id)
        {
            var response = new ReviewsResponse();
            try
            {
                var result = _modelDAL.GetByUserId(id);

                if (result != null && result.Count > 0)
                {
                    response.Reviews = result;
                    response.Message = $"Successfully get ${_entityName}s.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No ${_entityName} found by user id: {id}";
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
