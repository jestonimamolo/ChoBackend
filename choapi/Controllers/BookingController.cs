﻿using choapi.DAL;
using choapi.DTOs;
using choapi.Messages;
using choapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace choapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingDAL _bookingDAL;
        private readonly IUserDAL _userDAL;
        private readonly IEstablishmentDAL _establishmentDAL;
        private readonly ICategoryDAL _categoryDAL;

        private readonly ILogger<BookingController> _logger;

        public BookingController(ILogger<BookingController> logger, IBookingDAL bookingDAL, IUserDAL userDAL, IEstablishmentDAL establishmentDAL, ICategoryDAL categoryDAL)
        {
            _logger = logger;
            _bookingDAL = bookingDAL;
            _userDAL = userDAL;
            _establishmentDAL = establishmentDAL;
            _categoryDAL = categoryDAL;
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

                var establishment = _establishmentDAL.GetEstablishment(result.Establishment_Id);

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

        [HttpGet("bookings"), Authorize()]
        public ActionResult<BookingBookingsResponse> EstatablishmentBookings(int establishmentId)
        {
            var response = new BookingBookingsResponse();
            try
            {
                var result = _bookingDAL.GetEstablishmentBookings(establishmentId);

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

                        var establishment = _establishmentDAL.GetEstablishment(booking.Establishment_Id);
                        bookingsResponse.Esablishment = establishment;

                        response.Bookings.Add(bookingsResponse);
                    }

                    response.Message = "Successfully get Establishment bookings.";
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No Establishment Bookings found by establishment id: {establishmentId}";
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

                        var establishment = _establishmentDAL.GetEstablishment(booking.Establishment_Id);
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

                    var establishment = _establishmentDAL.GetEstablishment(result.Establishment_Id);

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

        [HttpGet("restaurants"), Authorize()]
        public ActionResult<BookingBookingsResponse> RestaurantsBooking()
        {
            var response = new BookingBookingsResponse();
            try
            {
                var restaurantCategory = _categoryDAL.GetByName("Restaurant");
                if (restaurantCategory == null)
                {
                    response.Message = $"Category of restaurant no found.";
                    response.Status = "Failed";
                    return BadRequest(response);
                }

                var resultEstablishment = _establishmentDAL.GetEstablishmentsByCategoryId(restaurantCategory.Category_Id);

                if (resultEstablishment != null && resultEstablishment.Count > 0)
                {
                    var bookingsResponse = new BookingsResponse();
                    foreach (var restaurant in resultEstablishment)
                    {
                        var result = _bookingDAL.GetEstablishmentBookings(restaurant.Establishment_Id);

                        if (result != null && result.Count > 0)
                        {
                            foreach (var booking in result)
                            {
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

                                var establishment = _establishmentDAL.GetEstablishment(booking.Establishment_Id);
                                bookingsResponse.Esablishment = establishment;

                                response.Bookings.Add(bookingsResponse);
                            }                            
                        }
                    }

                    if (response.Bookings.Count == 0)
                    {
                        response.Message = $"No Bookings found for restaurant establishment";
                        response.Status = "Failed";

                        return BadRequest(response);
                    }
                    else
                    {
                        response.Message = "Successfully get User bookings.";
                        return Ok(response);
                    }
                    
                }
                else
                {
                    response.Message = $"No establishment of restaurant found.";
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
