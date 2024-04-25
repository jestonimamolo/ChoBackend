using choapi.Models;

namespace choapi.Messages
{
    public class MenuResponse : ResponseBase
    {
        public Menus Menu { get; set; } = new Menus();
    }
}
