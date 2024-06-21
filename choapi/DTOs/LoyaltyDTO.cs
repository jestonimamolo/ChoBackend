namespace choapi.DTOs
{
    public class LoyaltyDTO
    {
        public int Loyalty_Id { get; set; }

        public int User_Id { get; set; }

        public double Points { get; set; }

        public string? Type { get; set; } = null;

        public int? Invited_Id { get; set; } = null;

        public string? Status { get; set; } = null;

        public DateTime? Created_Date { get; set; } = null;
    }
}
