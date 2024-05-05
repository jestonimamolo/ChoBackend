using choapi.Models;

namespace choapi.DAL
{
    public interface IRestaurantTableDAL
    {
        RestaurantTable Add(RestaurantTable model);

        RestaurantTable Update(RestaurantTable model);

        void Delete(RestaurantTable model);

        RestaurantTable? GetRestaurantTable(int id);

        List<RestaurantTable>? GetRestaurantTables(int restaurantId);
    }
}
