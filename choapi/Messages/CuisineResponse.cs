using choapi.Models;

namespace choapi.Messages
{
    public class CuisineResponse : ResponseBase
    {
        public Cuisines Cuisine { get; set; } = new Cuisines();
    }
}
