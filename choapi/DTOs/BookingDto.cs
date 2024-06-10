namespace choapi.DTOs
{
    public class BookingDTO
    {
        public int Booking_Id { get; set; }

        public required int User_Id { get; set; }

        public required int Establishment_Id { get; set; }

        public DateTime Booking_Date { get; set; } = DateTime.Now;

        public int? Number_Of_Seats { get; set; } = null;

        public string? Status { get; set; } = null;

        public string? Notes { get; set; } = null;

        public string? Reason_For_Rejection { get; set; } = null;

        public DateTime Created_Date { get; set; } = DateTime.Now;

        public string? Payment_Status { get; set; } = null;

        public int? Transaction_Id { get; set; } = null;
    }
}
