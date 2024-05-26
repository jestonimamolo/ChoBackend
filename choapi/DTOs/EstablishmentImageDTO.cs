namespace choapi.DTOs
{
    public class EstablishmentImageDTO
    {
        public int EstablishmentImage_Id { get; set; }

        public int Establishment_Id { get; set; }

        public IFormFile? File { get; set; }
    }
}
