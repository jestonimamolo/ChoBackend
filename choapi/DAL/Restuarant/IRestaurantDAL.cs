using choapi.Models;

namespace choapi.DAL
{
    public interface IRestaurantDAL
    {
        Restaurants Add(Restaurants model);

        Restaurants Update(Restaurants model);

        RestaurantImages AddImage(RestaurantImages model);

        List<RestaurantImages>? AddImages(List<RestaurantImages> moel);

        Restaurants? GetRestaurant(int id);

        RestaurantImages? GetRestaurantImage(int id);

        List<RestaurantImages>? GetRestaurantImages(int id);

        List<Restaurants>? GetRestaurants(int? userId);

        RestaurantImages UpdateImage(RestaurantImages model);

        void DeleteImage(RestaurantImages model);

        Menus Add(Menus model);

        List<Menus>? GetMenus(int restaurantId);

        Menus? GetMenu(int id);

        Menus UpdateMenu(Menus model);

        void DeleteMenu(Menus model);

        Availability Add(Availability model);

        Availability? GetAvailability(int id);

        void DeleteAvailability(Availability model);

        Availability UpdateAvailability(Availability model);

        List<Availability>? GetAvailabilities(int establishmentId);

        NonOperatingHours Add(NonOperatingHours model);

        void Delete(NonOperatingHours model);

        NonOperatingHours Update(NonOperatingHours model);

        NonOperatingHours? GetNonOperatingHours(int id);

        List<NonOperatingHours>? GetNonOperatingHoursByEstablishmentId(int id);

        RestaurantCuisines Add(RestaurantCuisines model);

        RestaurantCuisines UpdateCuisine(RestaurantCuisines model);

        void DeleteCuisine(RestaurantCuisines model);

        RestaurantCuisines? GetRestaurantCuisine(int id);

        List<RestaurantCuisines>? GetRestaurantCuisines(int? restaurantId);

        RestaurantBookType Add(RestaurantBookType model);

        List<RestaurantBookType>? GetBookTypes(int? restaurantId);

        RestaurantBookType? GetBookType(int id);

        RestaurantBookType UpdateBookType(RestaurantBookType model);
    }
}
