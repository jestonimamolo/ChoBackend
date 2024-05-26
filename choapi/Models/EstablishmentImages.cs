using System.ComponentModel.DataAnnotations;

namespace choapi.Models
{
    public class EstablishmentImages
    {
        [Key]
        public int EstablishmentImage_Id { get; set; }

        public int Establishment_Id { get; set; }

        public string? Image_Url { get; set; } = null;
    }
}
