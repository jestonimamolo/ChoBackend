namespace choapi.DTOs
{
    public class ReviewDTO
    {
        public int Review_Id { get; set; }

        public int Establishment_Id { get; set; }

        public int User_Id { get; set; }

        public string? Comment { get; set; } = null;

        public DateTime? Date_Added { get; set; } = null;

        public string? Status { get; set; } = null;
    }
}
