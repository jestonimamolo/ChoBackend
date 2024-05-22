namespace choapi.DTOs
{
    public class PromotionDTO
    {
        public int Promotion_Id { get; set; }

        public int Establishment_Id { get; set; }

        public string? Promotion_Details { get; set; } = null;

        public DateTime? Date_Promoted { get; set; } = null;

        public bool? Is_Active { get; set; } = null;
    }
}
