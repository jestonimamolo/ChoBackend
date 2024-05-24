﻿using choapi.Models;

namespace choapi.Messages
{
    public class BookingResponse : ResponseBase
    {
        public Bookings Booking { get; set; } = new Bookings();

        public UserResponse User { get; set; } = new UserResponse();

        public Restaurants? Establishment { get; set; } = new Restaurants();
    }
}
