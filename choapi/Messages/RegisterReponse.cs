namespace choapi.Messages
{
    public class RegisterReponse : ResponseBase
    {
        public UserResponse User { get; set; } = new UserResponse();
    }
}
