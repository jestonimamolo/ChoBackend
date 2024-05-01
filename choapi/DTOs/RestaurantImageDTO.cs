namespace choapi.DTOs
{
    public class RestaurantImageDTO
    {
        public int RestaurantImages_Id { get; set; }

        public int Restaurant_Id { get; set; }

        public IFormFile? File { get; set; }
    }
}
