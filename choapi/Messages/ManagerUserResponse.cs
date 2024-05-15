using choapi.Models;

namespace choapi.Messages
{
    public class ManagerUserResponse : ResponseBase
    {
        public Manager Manager { get; set; } = new Manager();

        public UserResponse User { get; set; } = new UserResponse();
    }
}
