using choapi.Models;

namespace choapi.Messages
{
    public class NotificationResponse : ResponseBase
    {
        public Notification Notification { get; set; } = new Notification();
    }
}
