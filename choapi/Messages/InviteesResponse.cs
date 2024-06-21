using choapi.Models;

namespace choapi.Messages
{
    public class InviteesResponse : ResponseBase
    {
        public List<Invite>? Invitees { get; set; } = new List<Invite>();
    }
}
