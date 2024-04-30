namespace choapi.DTOs
{
    public class MenuDto
    {
        public int Menu_Id { get; set; }

        public int Restaurant_Id { get; set; }

        public string? Type { get; set; } = null;

        public IFormFile File { get; set; }
    }
}
