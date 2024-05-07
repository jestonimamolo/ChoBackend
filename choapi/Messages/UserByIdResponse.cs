namespace choapi.Messages
{
    public class UserByIdResponse : ResponseBase
    {
        public UserResponse User { get; set; } = new UserResponse();
    }
}
