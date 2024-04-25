using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class Restaurants
    {
        [Key]
        public int Restaurant_Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; } = null;

        public int? User_Id { get; set; } = null;

        public int? Credits { get; set; } = null;

        public string? Plan { get; set; } = null;

        public decimal? Latitude { get; set; } = null;

        public decimal? Longitude { get; set; } = null;

        public bool? Is_Promoted { get; set; } = null;

        public string? Address { get; set;} = null;

        public bool? Is_Active { get; set; } = null;
    }
}
