using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class Loyalty
    {
        [Key]
        public int Loyalty_Id { get; set; }

        public int User_Id { get; set; }

        public double Points { get; set; }

        public string? Type { get; set; } = null;

        public int? Invited_Id { get; set; } = null;

        public string? Status { get; set; } = null;

        public DateTime? Created_Date { get; set; } = null;

        public bool? Is_Deleted { get; set; } = null;
    }
}
