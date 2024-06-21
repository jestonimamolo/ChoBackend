using choapi.Models;

namespace choapi.Messages
{
    public class InviteResponse : ResponseBase
    {
        public Invite Invite { get; set; } = new Invite();
    }
}
