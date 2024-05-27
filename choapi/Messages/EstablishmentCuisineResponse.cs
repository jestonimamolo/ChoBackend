using choapi.Models;

namespace choapi.Messages
{
    public class EstablishmentCuisineResponse : ResponseBase
    {
        public EstablishmentCuisines EstablishmentCuisine { get; set; } = new EstablishmentCuisines();
    }
}
