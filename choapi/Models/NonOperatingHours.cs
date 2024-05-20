using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class NonOperatingHours
    {
        [Key]
        public int NonOperatingHours_Id { get; set; }

        public int Establishment_Id { get; set; }

        public string? Title { get; set; } = null;

        public DateTime? Date { get; set; } = null;
    }
}
