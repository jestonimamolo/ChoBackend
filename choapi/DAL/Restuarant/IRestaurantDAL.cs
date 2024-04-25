using choapi.Models;

namespace choapi.DAL
{
    public interface IRestaurantDAL
    {
        Restaurants Add(Restaurants model);

        List<RestaurantImages>? AddImages(List<RestaurantImages> restaurantImages);

        Restaurants? GetRestaurant(int id);

        List<RestaurantImages>? GetRestaurantImages(int id);

        List<Restaurants>? GetRestaurants(int? userId);

        Menus Add(Menus model);

        List<Menus>? GetMenus(int restaurantId);

        Menus? GetMenu(int id);

        Menus UpdateMenu(Menus model);

        void DeleteMenu(Menus model);

        RestaurantAvailability Add(RestaurantAvailability model);

        RestaurantAvailability? GetAvailability(int id);

        void DeleteAvailability(RestaurantAvailability model);

        RestaurantAvailability UpdateAvailability(RestaurantAvailability model);

        List<RestaurantAvailability>? GetAvailabilities(int restaurantId);

        RestaurantCuisines Add(RestaurantCuisines model);

        List<RestaurantCuisines>? GetRestaurantCuicines(int? restaurantId);

        RestaurantBookType Add(RestaurantBookType model);

        List<RestaurantBookType>? GetBookTypes(int? restaurantId);

        RestaurantBookType? GetBookType(int id);

        RestaurantBookType UpdateBookType(RestaurantBookType model);
    }
}
