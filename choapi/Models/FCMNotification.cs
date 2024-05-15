using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class FCMNotification
    {
        [Key]
        public int FCMNotification_Id { get; set; }

        public int User_Id { get; set; }

        public string? FCM_Id { get; set; } = null;

        public DateTime? Date_Added { get; set; } = null;

        public bool? Is_Active { get; set; } = null;
    }
}
