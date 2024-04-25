namespace choapi.Messages
{
    public class UserResponse
    {
        public int User_Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public int? Role_Id { get; set; } = null;

        public string? Email { get; set; } = null;

        public string? Phone { get; set; } = string.Empty;

        public bool? Is_Active { get; set; } = null;

        public string? Display_Name { get; set; } = null;

        public string? Photo_Url { get; set; } = null;

        public decimal? Latitude { get; set; } = null;

        public decimal? Longitude { get; set; } = null;
    }
}
