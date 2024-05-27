using choapi.Models;

namespace choapi.Messages
{
    public class EstablishmentCuisinesResponse : ResponseBase
    {
        public List<EstablishmentCuisines>? EstablishmentCuisines { get; set; } = new List<EstablishmentCuisines>();
    }
}
