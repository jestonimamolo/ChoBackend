using choapi.Models;

namespace choapi.Messages
{
    public class BookingsResponse : ResponseBase
    {
        public List<Bookings> Bookings { get; set; } = new List<Bookings>();
    }
}
