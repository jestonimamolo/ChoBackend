using System.ComponentModel.DataAnnotations;

namespace choapi.DTOs
{
    public class SaveEstablishmentDTO
    {
        public int SaveEstablishment_Id { get; set; }

        public int Establishment_Id { get; set; }

        public int User_Id { get; set; }

        public DateTime? Date_Added { get; set; } = null;
    }
}
