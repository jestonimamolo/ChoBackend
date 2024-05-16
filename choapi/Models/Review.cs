using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class Review
    {
        [Key]
        public int Review_Id { get; set; }

        public int Establishment_Id { get; set; }

        public int User_Id { get; set; }

        public string? Comment { get; set; } = null;

        public DateTime? Date_Added { get; set; } = null;

        public string? Status { get; set; } = null;

        public bool? Is_Deleted { get; set; } = null;
    }
}
