using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class EstablishmentCuisines
    {
        [Key]
        public int EstablishmentCuisine_Id { get; set; }

        public int Establishment_Id { get; set; }

        public string? Name { get; set; } = null;
    }
}
