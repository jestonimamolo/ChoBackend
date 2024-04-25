using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class RestaurantAvailability
    {
        [Key]
        public int RestaurantAvailability_Id { get; set; }

        public int Restaurant_Id { get; set; }

        public string? Day { get; set; } = null;

        public string? Time_Start { get; set; } = null;

        public string? Time_End { get; set; } = null;
    }
}
