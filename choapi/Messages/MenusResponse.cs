using choapi.Models;

namespace choapi.Messages
{
    public class MenusResponse : ResponseBase
    {
        public List<Menus> Menus { get; set; } = new List<Menus>();
    }
}
