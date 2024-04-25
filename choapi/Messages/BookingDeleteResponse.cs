using choapi.Models;

namespace choapi.Messages
{
    public class BookingDeleteResponse : ResponseBase
    {
        public Bookings Booking { get; set; } = new Bookings();
    }
}
