using choapi.DTOs;
using choapi.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace choapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailHelper _emailHelper;

        public EmailController(IEmailHelper emailHelper)
        {
            _emailHelper = emailHelper;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail(EmailDTO request)
        {
            try
            {
                await _emailHelper.SendEmailAsync(request.ToEmail, request.Subject, request.Body);
                return Ok("Email sent successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to send email: {ex.Message}");
            }
        }
    }
}
