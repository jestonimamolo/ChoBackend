namespace choapi.DTOs
{
    public class NotificationDTO
    {
        public int Notification_Id { get; set; }

        public int Sender_Id { get; set; }

        public int Receiver_Id { get; set; }

        public string? Title { get; set; } = null;

        public string? Message { get; set; } = null;

        public DateTime? Created_Date { get; set; } = null;

        public bool? Is_Send { get; set; } = null;

        public bool? Is_Deleted { get; set; } = null;
    }
}
