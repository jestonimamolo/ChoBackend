namespace choapi.DTOs
{
    public class FCMNotificationDTO
    {
        public int FCMNotification_Id { get; set; }

        public int User_Id { get; set; }

        public string? FCM_Id { get; set; } = null;
    }
}
