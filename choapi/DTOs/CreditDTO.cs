namespace choapi.DTOs
{
    public class CreditDTO
    {
        public int Credit_Id { get; set; }

        public int Restaurant_Id { get; set; }

        public double? Amount { get; set; } = null;

        public string? Transaction_Type { get; set; } = null;

        public DateTime? Transaction_Date { get; set; } = null;
    }
}
