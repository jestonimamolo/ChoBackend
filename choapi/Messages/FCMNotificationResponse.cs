using choapi.Models;

namespace choapi.Messages
{
    public class FCMNotificationResponse : ResponseBase
    {
        public FCMNotification FCMNotification { get; set; } = new FCMNotification();
    }
}
