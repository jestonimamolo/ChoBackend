using choapi.DAL;
using choapi.DTOs;
using choapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace choapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentDAL _paymentDAL;

        private readonly ILogger<PaymentController> _logger;

        public PaymentController(ILogger<PaymentController> logger, IPaymentDAL paymentDAL)
        {
            _logger = logger;
            _paymentDAL = paymentDAL;
        }

        [HttpPost("add"), Authorize()]
        public ActionResult<Bookings> Add(PaymentDTO request)
        {
            var payment = new Payment
            {
                User_Id = request.User_Id,
                Restaurant_id = request.Restaurant_id,
                Amount = request.Amount,
                Payment_date = request.Payment_date,
                Payment_Method = request.Payment_Method,
                Transaction_Id = request.Transaction_Id,
                Is_Paid = request.Is_Paid
            };

            var bookingResult = _paymentDAL.Add(payment);

            return Ok(bookingResult);
        }
    }
}
