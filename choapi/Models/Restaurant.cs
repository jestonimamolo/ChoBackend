using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class Restaurant
    {
        [Key]
        public int Restaurant_Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Address { get; set; } = null;

        public string? Contact_Number { get; set; } = null;

        public string? Opening_Hours { get; set; } = null;

        public string? Cousine_Type { get; set; } = null;

        public double? Registration_Fee { get; set; } = null;

        public int? User_Id_Manager { get; set;} = null;

        public bool? Is_Registered { get; set; } = null;
    }
}
