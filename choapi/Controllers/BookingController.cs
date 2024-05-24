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
    public class BookingController : ControllerBase
    {
        private readonly IBookingDAL _bookingDAL;
        private readonly IUserDAL _userDAL;
        private readonly IRestaurantDAL _restaurantDAL;

        private readonly ILogger<BookingController> _logger;

        public BookingController(ILogger<BookingController> logger, IBookingDAL bookingDAL, IUserDAL userDAL, IRestaurantDAL restaurantDAL)
        {
            _logger = logger;
            _bookingDAL = bookingDAL;
            _userDAL = userDAL;
            _restaurantDAL = restaurantDAL;
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
                    Establishment_Id = request.Establishment_Id,
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

                var user = _userDAL.GetUser(result.User_Id);
                var userResponse = new UserResponse();
                if (user != null)
                {
                    userResponse.User_Id = user.User_Id;
                    userResponse.Username = user.Username;
                    userResponse.Email = user.Email;
                    userResponse.Phone = user.Phone;
                    userResponse.Role_Id = user.Role_Id;
                    userResponse.Is_Active = user.Is_Active;
                    userResponse.Display_Name = user.Display_Name;
                    userResponse.Photo_Url = user.Photo_Url;
                    userResponse.Latitude = user.Latitude;
                    userResponse.Longitude = user.Longitude;
                }

                var establishment = _restaurantDAL.GetRestaurant(result.Establishment_Id);

                response.User = userResponse;
                response.Establishment = establishment;
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
        public ActionResult<BookingBookingsResponse> RestaurantBookings(int id)
        {
            var response = new BookingBookingsResponse();
            try
            {
                var result = _bookingDAL.GetEstablishmentBookings(id);

                if (result != null && result.Count > 0)
                {
                    foreach (var booking in result)
                    {
                        var bookingsResponse = new BookingsResponse();

                        bookingsResponse.Booking = booking;

                        var user = _userDAL.GetUser(booking.User_Id);
                        if (user != null)
                        {
                            bookingsResponse.User.User_Id = user.User_Id;
                            bookingsResponse.User.Username = user.Username;
                            bookingsResponse.User.Email = user.Email;
                            bookingsResponse.User.Phone = user.Phone;
                            bookingsResponse.User.Role_Id = user.Role_Id;
                            bookingsResponse.User.Is_Active = user.Is_Active;
                            bookingsResponse.User.Display_Name = user.Display_Name;
                            bookingsResponse.User.Photo_Url = user.Photo_Url;
                            bookingsResponse.User.Latitude = user.Latitude;
                            bookingsResponse.User.Longitude = user.Longitude;
                        }

                        var establishment = _restaurantDAL.GetRestaurant(booking.Establishment_Id);
                        bookingsResponse.Esablishment = establishment;

                        response.Bookings.Add(bookingsResponse);
                    }

                    response.Message = "Successfully get Establishment bookings.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Establishment Bookings found by establishment id: {id}";
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
        public ActionResult<BookingBookingsResponse> UserBooking(int id)
        {
            var response = new BookingBookingsResponse();
            try
            {
                var result = _bookingDAL.GetUserBookings(id);

                if (result != null && result.Count > 0)
                {
                    foreach (var booking in result)
                    {
                        var bookingsResponse = new BookingsResponse();

                        bookingsResponse.Booking = booking;

                        var user = _userDAL.GetUser(booking.User_Id);
                        if (user != null)
                        {
                            bookingsResponse.User.User_Id = user.User_Id;
                            bookingsResponse.User.Username = user.Username;
                            bookingsResponse.User.Email = user.Email;
                            bookingsResponse.User.Phone = user.Phone;
                            bookingsResponse.User.Role_Id = user.Role_Id;
                            bookingsResponse.User.Is_Active = user.Is_Active;
                            bookingsResponse.User.Display_Name = user.Display_Name;
                            bookingsResponse.User.Photo_Url = user.Photo_Url;
                            bookingsResponse.User.Latitude = user.Latitude;
                            bookingsResponse.User.Longitude = user.Longitude;
                        }

                        var establishment = _restaurantDAL.GetRestaurant(booking.Establishment_Id);
                        bookingsResponse.Esablishment = establishment;

                        response.Bookings.Add(bookingsResponse);
                    }

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
                    var user = _userDAL.GetUser(result.User_Id);
                    var userResponse = new UserResponse();
                    if (user != null)
                    {
                        userResponse.User_Id = user.User_Id;
                        userResponse.Username = user.Username;
                        userResponse.Email = user.Email;
                        userResponse.Phone = user.Phone;
                        userResponse.Role_Id = user.Role_Id;
                        userResponse.Is_Active = user.Is_Active;
                        userResponse.Display_Name = user.Display_Name;
                        userResponse.Photo_Url = user.Photo_Url;
                        userResponse.Latitude = user.Latitude;
                        userResponse.Longitude = user.Longitude;
                    }

                    var establishment = _restaurantDAL.GetRestaurant(result.Establishment_Id);

                    response.User = userResponse;
                    response.Establishment = establishment;
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
