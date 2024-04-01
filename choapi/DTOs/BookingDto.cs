namespace choapi.DTOs
{
    public class BookingDto
    {
        public required int User_Id { get; set; }

        public required int Restaurant_id { get; set; }

        public int? Table_Id { get; set; } = null;

        public string? Booking_Time { get; set; } = null;

        public int? Number_Of_People { get; set; } = null;

        public bool? Is_Paid { get; set; } = null;
    }
}
