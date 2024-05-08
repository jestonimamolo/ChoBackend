using choapi.Models;

namespace choapi.Messages
{
    public class ManagerResponse : ResponseBase
    {
        public Manager Manager { get; set; } = new Manager();
    }
}
