namespace choapi.DTOs
{
    public class EstablishmentCuisineDTO
    {
        public int EstablishmentCuisine_Id { get; set; }

        public int Establishment_Id { get; set; }

        public required string Name { get; set; }
    }
}
