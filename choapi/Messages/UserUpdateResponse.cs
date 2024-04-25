namespace choapi.Messages
{
    public class UserUpdateResponse : ResponseBase
    {
        public UserResponse User { get; set; } = new UserResponse();
    }
}
