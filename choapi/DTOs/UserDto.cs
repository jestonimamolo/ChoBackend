namespace choapi.DTOs
{
    public class UserDTO
    {
        public required string Username { get; set; }

        public required string Password { get; set; }

        public int? Role_Id { get; set; } = null;

        public string? Email { get; set; } = null;

        public string? Phone { get; set; } = null;

        public string? Display_Name { get; set; } = null;

        public string? Photo_Url { get; set; } = null;

        public decimal? Latitude { get; set; } = null;

        public decimal? Longitude { get; set; } = null;
    }
}
