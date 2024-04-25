using System.ComponentModel.DataAnnotations;

namespace choapi.DTOs
{
    public class PaymentDTO
    {
        public required int User_Id { get; set; }

        public required int Restaurant_id { get; set; }

        public float? Amount { get; set; } = null;

        public DateTime? Payment_date { get; set; } = null;

        public string? Payment_Method { get; set; } = null;

        public string? Transaction_Id { get; set; } = null;

        public bool? Is_Paid { get; set; } = null;
    }
}
