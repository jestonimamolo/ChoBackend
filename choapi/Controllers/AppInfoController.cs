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
    public class AppInfoController : ControllerBase
    {
        private readonly IAppInfoDAL _modelDAL;

        private readonly ILogger<FCMNotificationController> _logger;

        private const string _entityName = "AppInfo";

        public AppInfoController(ILogger<FCMNotificationController> logger, IAppInfoDAL modelDAL)
        {
            _logger = logger;
            _modelDAL = modelDAL;
        }

        [HttpPost("add"), Authorize()]
        public ActionResult<AppInfoResponse> Add(AppInfoDTO request)
        {
            var response = new AppInfoResponse();
            try
            {
                if (string.IsNullOrEmpty(request.Content))
                {
                    response.Message = $"Required Content.";
                    response.Status = "Failed";

                    return BadRequest(response);
                }

                var model = new AppInfo
                {
                    Title = request.Title,
                    Content = request.Content,
                    Date_Added = DateTime.Now
                };

                var result = _modelDAL.Add(model);

                response.AppInfo = result;
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
        public ActionResult<AppInfoResponse> Update(AppInfoDTO request)
        {
            var response = new AppInfoResponse();
            try
            {
                var model = _modelDAL.Get(request.Id);

                if (model != null)
                {
                    model.Title = request.Title;
                    model.Content= request.Content;

                    var result = _modelDAL.Update(model);

                    response.AppInfo = result;
                    response.Message = "Successfully updated.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No {_entityName} found by id: {request.Id}";
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
        public ActionResult<AppInfoResponse> Delete(int id)
        {
            var response = new AppInfoResponse();
            try
            {
                var model = _modelDAL.Get(id);

                if (model != null)
                {
                    model.Is_Active = false;

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
        public ActionResult<AppInfoResponse> Get(int id)
        {
            var response = new AppInfoResponse();
            try
            {
                var result = _modelDAL.Get(id);

                if (result != null)
                {
                    response.AppInfo = result;
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
    }
}
