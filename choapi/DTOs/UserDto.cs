namespace choapi.DTOs
{
    public class UserDto
    {
        public required string Username { get; set; }

        public required string Password { get; set; }

        public string? Role { get; set; } = null;

        public bool? Is_Active { get; set; } = null;
    }
}
