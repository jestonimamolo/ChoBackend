using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class Booking
    {
        [Key]
        public int Booking_Id { get; set; }

        public int User_Id { get; set; }

        public int Restaurant_id { get; set; }

        public int? Table_Id { get; set; } = null;

        public string? Booking_Time { get; set; } = null;

        public int? Number_Of_People { get; set; } = null;

        public bool? Is_Paid { get; set; } = null;
    }
}
