using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class SaveEstablishment
    {
        [Key]
        public int SaveEstablishment_Id { get; set; }

        public int Establishment_Id { get; set; }

        public int User_Id { get; set; }

        public DateTime? Date_Added { get; set; } = null;

        public bool? Is_Deleted { get; set; } = null;
    }
}
