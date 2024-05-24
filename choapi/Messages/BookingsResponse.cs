using choapi.Models;

namespace choapi.Messages
{
    public class BookingsResponse
    {
        public Bookings Booking { get; set; } = new Bookings();

        public UserResponse User { get; set; } = new UserResponse();

        public Restaurants? Esablishment { get; set; } = new Restaurants();
    }
}
