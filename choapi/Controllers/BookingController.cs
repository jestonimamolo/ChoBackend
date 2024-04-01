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
    public class BookingController : ControllerBase
    {
        private readonly IBookingDAL _bookingDAL;

        private readonly ILogger<BookingController> _logger;

        public BookingController(ILogger<BookingController> logger, IBookingDAL bookingDAL)
        {
            _logger = logger;
            _bookingDAL = bookingDAL;
        }

        [HttpPost("add"), Authorize()]
        public ActionResult<Booking> Add(BookingDto request)
        {
            var booking = new Booking
            {
                User_Id = request.User_Id,
                Restaurant_id = request.Restaurant_id,
                Table_Id = request.Table_Id,
                Booking_Time = request.Booking_Time,
                Number_Of_People = request.Number_Of_People,
                Is_Paid = false
            };

            var resultRestaurant = _bookingDAL.Add(booking);

            return Ok(resultRestaurant);
        }

        [HttpPost("cancel/{id}"), Authorize()]
        public ActionResult<string> Delete(int id)
        {
            _bookingDAL.DeleteBooking(id);

            return Ok("Booking canceled successfully");
        }
    }
}
