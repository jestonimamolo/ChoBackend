using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class RestaurantTable
    {
        [Key]
        public int RestaurantTable_Id { get; set; }

        public int Restaurant_Id { get; set; }

        public int? Table_Number { get; set; } = null;

        public int? Capacity { get; set; } = null;

        public string? Time_Start { get; set; } = null;

        public bool? Is_Reserved { get; set; } = null;
    }
}
