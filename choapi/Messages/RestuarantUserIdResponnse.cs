using choapi.Models;

namespace choapi.Messages
{
    public class RestuarantUserIdResponnse : ResponseBase
    {
        public List<RestaurantsReponse> Restaurants { get; set; } = new List<RestaurantsReponse>();
    }
}
