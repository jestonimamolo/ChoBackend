namespace choapi.Messages
{
    public class BookingBookingsResponse : ResponseBase
    {
        public List<BookingsResponse> Bookings { get; set; } = new List<BookingsResponse>(); 
    }
}
