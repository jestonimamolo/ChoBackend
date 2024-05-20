namespace choapi.DTOs
{
    public class NonOperatingHoursDTO
    {
        public int NonOperatingHours_Id { get; set; }

        public int Establishment_Id { get; set; }

        public DateTime? Date { get; set; } = null;

        public string? Title { get; set; } = null;
    }
}
