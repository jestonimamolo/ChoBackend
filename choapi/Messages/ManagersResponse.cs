using choapi.Models;

namespace choapi.Messages
{
    public class ManagersResponse : ResponseBase
    {
        public List<Manager>? Managers { get; set; } = new List<Manager>();
    }
}
