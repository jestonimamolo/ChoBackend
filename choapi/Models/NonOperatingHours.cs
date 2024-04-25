using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class NonOperatingHours
    {
        [Key]
        public int NonOperatingHours_Id { get; set; }

        public int Restaurant_Id { get; set; }

        public DateTime? Date { get; set; } = null;
    }
}
