using choapi.Models;

namespace choapi.Messages
{
    public class NotificationsResponse : ResponseBase
    {
        public List<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
