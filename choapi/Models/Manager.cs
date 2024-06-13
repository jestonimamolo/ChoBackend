using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class Manager
    {
        [Key]
        public int Manager_Id { get; set; }

        public int User_Id { get; set; }

        public int Establishment_Id { get; set; }

        public int? Created_By { get; set; } = null;

        public DateTime? Created_Date { get; set; } = null;

        public bool? Is_Deleted { get; set; } = null;
    }
}
