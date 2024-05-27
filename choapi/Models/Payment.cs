using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class Payment
    {
        [Key]
        public int Payment_Id { get; set; }

        public int User_Id { get; set; }

        public int Establishment_Id { get; set; }

        public float? Amount { get; set; } = null;

        public DateTime? Payment_date { get; set; } = null;

        public string? Payment_Method { get; set; } = null;

        public string? Transaction_Id { get; set; } = null;

        public bool? Is_Paid { get; set; } = null;
    }
}
