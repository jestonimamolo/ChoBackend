using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class AppInfo
    {
        [Key]
        public int Id { get; set; }

        public string? Title { get; set; } = null;

        public string Content { get; set; } = "";

        public DateTime? Date_Added { get; set; } = null;

        public bool? Is_Active { get; set; } = null;
    }
}
