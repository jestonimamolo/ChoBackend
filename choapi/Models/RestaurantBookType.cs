using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class RestaurantBookType
    {
        [Key]
        public int RestaurantBookType_Id { get; set; }

        public int Restaurant_Id { get; set; }

        public bool? Is_Payable { get; set; } = null;

        public string? Name { get; set; } = null;

        public string? Currency { get; set; } = null;

        public double? Price { get; set;} = null;

        public bool? Is_Deleted { get; set; } = null;
    }
}
