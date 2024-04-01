using choapi.Models;

namespace choapi.DAL
{
    public interface IRestaurantDAL
    {
        Restaurant? Add(Restaurant model);

        Restaurant? GetRestaurant(int id);

        List<Restaurant>? GetRestaurants(int userIdManager);
    }
}
