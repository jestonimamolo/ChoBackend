using choapi.Models;

namespace choapi.Messages
{
    public class BookingStatusResponse : ResponseBase
    {
        public Bookings Booking { get; set; } = new Bookings();
    }
}
