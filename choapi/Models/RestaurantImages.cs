using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class RestaurantImages
    {
        [Key]
        public int RestaurantImages_Id { get; set; }

        public int Restaurant_Id { get; set; }

        public string? Image_Url { get; set; } = null;
    }
}
