namespace choapi.DTOs
{
    public class AppInfoDTO
    {
        public int Id { get; set; }

        public string? Title { get; set; } = null;

        public string Content { get; set; } = "";

        public DateTime? Date_Added { get; set; } = null;

        public bool? Is_Active { get; set; } = null;
    }
}
