namespace choapi.DTOs
{
    public class BookingStatusDTO
    {
        public required int Booking_Id { get; set; }

        public required string Status { get; set; }

        public string? Notes { get; set; } = null;

        public string? Reason_For_Rejection { get; set; } = null;
    }
}
