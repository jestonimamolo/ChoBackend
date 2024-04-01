using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class User
    {
        [Key]
        public int User_Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Password_Hash { get; set; } = string.Empty;

        public string? Role { get; set; } = null;

        public bool? Is_Active { get; set; } = null;

    }
}
