using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class Promotion
    {
        [Key]
        public int Promotion_Id { get; set; }

        public int Establishment_Id { get; set; }

        public string? Promotion_Details { get; set; } = null;

        public DateTime? Date_Start { get; set; } = null;

        public DateTime? Date_End { get; set; } = null;

        public bool? Is_Deleted { get; set; } = null;
    }
}
