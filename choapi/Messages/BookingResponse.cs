using choapi.Models;

namespace choapi.Messages
{
    public class BookingResponse : ResponseBase
    {
        public Bookings Booking { get; set; } = new Bookings();
    }
}
