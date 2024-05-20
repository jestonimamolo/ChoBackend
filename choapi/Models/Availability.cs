using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class Availability
    {
        [Key]
        public int Availability_Id { get; set; }

        public int Establishment_Id { get; set; }

        public string? Day { get; set; } = null;

        public string? Time_Start { get; set; } = null;

        public string? Time_End { get; set; } = null;
    }
}
