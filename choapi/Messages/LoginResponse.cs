namespace choapi.Messages
{
    public class LoginResponse : ResponseBase
    {
        public UserResponse User { get; set; } = new UserResponse();

        public string? Token { get; set; }
    }
}
