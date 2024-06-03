namespace choapi.DTOs
{
    public class UserUpdateDTO
    {
        public int User_id { get; set; }

        public int? Role_Id { get; set; } = null;

        public string? Email { get; set; } = null;

        public string? Phone { get; set; } = null;

        public string? Display_Name { get; set; } = null;

        public double? Latitude { get; set; } = null;

        public double? Longitude { get; set; } = null;

        public IFormFile? File { get; set; }
    }
}
