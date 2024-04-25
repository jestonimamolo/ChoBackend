using Azure.Core;
using choapi.DAL;
using choapi.DTOs;
using choapi.Messages;
using choapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design.Serialization;

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
        public ActionResult<BookingResponse> Add(BookingDTO request)
        {
            var response = new BookingResponse();
            try
            {
                var booking = new Bookings
                {
                    User_Id = request.User_Id,
                    Restaurant_Id = request.Restaurant_Id,
                    Booking_Date = request.Booking_Date,
                    Number_Of_Seats = request.Number_Of_Seats,
                    Status = request.Status,
                    Notes = request.Notes,
                    Reason_For_Rejection = request.Reason_For_Rejection,
                    Created_Date = request.Created_Date,
                    Payment_Status = request.Payment_Status,
                    Transaction_Id = request?.Transaction_Id,
                    Is_Deleted = false
                };

                var result = _bookingDAL.Add(booking);

                response.Booking = result;
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
        public ActionResult<BookingStatusResponse> Update(BookingStatusDTO request)
        {
            var response = new BookingStatusResponse();
            try
            {
                var booking = _bookingDAL.GetBooking(request.Booking_Id);

                if (booking != null)
                {
                    booking.Status = request.Status;
                    booking.Notes = request.Notes;
                    booking.Reason_For_Rejection = request.Reason_For_Rejection;

                    var result = _bookingDAL.Update(booking);

                    response.Booking = result;
                    response.Message = "Successfully updated.";
                    return Ok(response);
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = $"No found Booking id: {request.Booking_Id}";

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

        [HttpPost("delete/{id}"), Authorize()]
        public ActionResult<BookingDeleteResponse> Delete(int id)
        {
            var response = new BookingDeleteResponse();
            try
            {
                var booking = _bookingDAL.GetBooking(id);

                if (booking != null)
                {
                    booking.Is_Deleted = true;

                    var result = _bookingDAL.Update(booking);

                    response.Booking = result;
                    response.Message = "Successfully deleted.";
                    return Ok(response);
                }
                else
                {
                    response.Status = "Failed";
                    response.Message = $"No found Booking id: {id}";

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

        [HttpGet("bookings/restaurant/{id}"), Authorize()]
        public ActionResult<BookingsResponse> RestaurantBookings(int id)
        {
            var response = new BookingsResponse();
            try
            {
                var result = _bookingDAL.GetRestaurantBookings(id);

                if (result != null && result.Count > 0)
                {
                    response.Bookings = result;
                    response.Message = "Successfully get Restaurant bookings.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Restaurant Bookings found by restaurant id: {id}";
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

        [HttpGet("bookings/user/{id}"), Authorize()]
        public ActionResult<BookingsResponse> UserBooking(int id)
        {
            var response = new BookingsResponse();
            try
            {
                var result = _bookingDAL.GetRestaurantBookings(id);

                if (result != null && result.Count > 0)
                {
                    response.Bookings = result;
                    response.Message = "Successfully get User bookings.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No User Bookings found by user id: {id}";
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
        public ActionResult<BookingResponse> Booking(int id)
        {
            var response = new BookingResponse();
            try
            {
                var result = _bookingDAL.GetBooking(id);

                if (result != null)
                {
                    response.Booking = result;
                    response.Message = "Successfully get booking.";

                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Booking found by booking id: {id}";
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
