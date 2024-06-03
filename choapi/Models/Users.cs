using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class Users
    {
        [Key]
        public int User_Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Password_Hash { get; set; } = string.Empty;

        public string? Email { get; set; } = null;

        public string? Phone { get; set; } = null;

        public int? Role_Id { get; set; } = null;

        public bool? Is_Active { get; set; } = null;

        public string? Display_Name { get; set; } = null;

        public string? Photo_Url { get; set; } = null;

        public double? Latitude { get; set; } = null;

        public double? Longitude { get; set; } = null;
    }
}
