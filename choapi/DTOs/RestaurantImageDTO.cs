namespace choapi.DTOs
{
    public class RestaurantImageDTO
    {
        public int RestaurantImages_Id { get; set; }

        public int Restaurant_Id { get; set; }

        public string? Image_Url { get; set; } = null;
    }
}
