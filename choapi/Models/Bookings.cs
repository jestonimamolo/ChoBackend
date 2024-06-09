using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class Bookings
    {
        [Key]
        public int Booking_Id { get; set; }

        public int User_Id { get; set; }

        public int Establishment_Id { get; set; }

        public DateTime? Booking_Date { get; set; } = null;

        public int? Number_Of_Seats { get; set; } = null;

        public string? Status { get; set; } = null;

        public string? Notes { get; set; } = null;

        public string? Reason_For_Rejection { get; set; } = null;

        public DateTime Created_Date { get; set; } = DateTime.Now;

        public string? Payment_Status { get; set; } = null;

        public int? Transaction_Id { get; set; } = null;

        public bool? Is_Deleted { get; set; } = null;
    }
}
