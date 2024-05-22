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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryDAL _modelDAL;

        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger, ICategoryDAL modelDAL)
        {
            _logger = logger;
            _modelDAL = modelDAL;
        }

        [HttpPost("add"), Authorize()]
        public ActionResult<CategoryResponse> Add(CategoryDTO request)
        {
            var response = new CategoryResponse();
            try
            {
                var model = new Category
                {
                    Name = request.Name,
                    Is_Active = request.Is_Active,
                };

                var result = _modelDAL.Add(model);

                response.Category = result;
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
        public ActionResult<CategoryResponse> Update(CategoryDTO request)
        {
            var response = new CategoryResponse();
            try
            {
                var model = _modelDAL.Get(request.Category_Id);

                if (model != null)
                {
                    model.Name = request.Name;
                    model.Is_Active = request.Is_Active;

                    var result = _modelDAL.Update(model);

                    response.Category = result;
                    response.Message = "Successfully updated.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No category found by id: {request.Category_Id}";
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

        [HttpPost("deactivate/{id}"), Authorize()]
        public ActionResult<CategoryResponse> Delete(int id)
        {
            var response = new CategoryResponse();
            try
            {
                var model = _modelDAL.Get(id);

                if (model != null)
                {
                    model.Is_Active = false;

                    var result = _modelDAL.Update(model);

                    response.Message = "Successfully deactivated.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No category found by id: {id}";
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
        public ActionResult<CategoryResponse> GetPromotion(int id)
        {
            var response = new CategoryResponse();
            try
            {
                var result = _modelDAL.Get(id);

                if (result != null)
                {
                    response.Category = result;
                    response.Message = "Successfully get category.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No category found by id: {id}";
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
