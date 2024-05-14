namespace choapi.DTOs
{
    public class CreditDTO
    {
        public int Credit_Id { get; set; }

        public int Establishment_Id { get; set; }

        public int? Amount { get; set; } = null;

        public string? Transaction_Type { get; set; } = null;

        public DateTime? Transaction_Date { get; set; } = null;
    }
}
