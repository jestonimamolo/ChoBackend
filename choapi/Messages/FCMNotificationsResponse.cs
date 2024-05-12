using choapi.Models;

namespace choapi.Messages
{
    public class FCMNotificationsResponse : ResponseBase
    {
        public List<FCMNotification> FCMNotifications { get; set; } = new List<FCMNotification>();
    }
}
