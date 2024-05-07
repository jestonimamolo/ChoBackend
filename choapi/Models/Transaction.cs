using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class Transaction
    {
        [Key]
        public int Transaction_Id { get; set; }

        public int User_Id { get; set; }

        public string? Transaction_Type { get; set; } = null;

        public string? Type { get; set; } = null;

        public string? Status { get; set; } = null;

        public DateTime? Created_Date { get; set; } = null;

        public bool? Is_Deleted { get; set; } = null;
    }
}
