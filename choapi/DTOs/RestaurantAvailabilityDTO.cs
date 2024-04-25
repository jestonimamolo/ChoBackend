namespace choapi.DTOs
{
    public class RestaurantAvailabilityDTO
    {
        public int RestaurantAvailability_Id { get; set; }

        public int Restaurant_Id { get; set; }

        public string? Day { get; set; } = null;

        public string? Time_Start { get; set; } = null;

        public string? Time_End { get; set; } = null;
    }
}
