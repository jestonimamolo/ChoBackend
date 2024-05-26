using choapi.Models;

namespace choapi.DAL
{
    public interface IRestaurantDAL
    {
        Restaurants Add(Restaurants model);

        Restaurants Update(Restaurants model);

        EstablishmentImages AddImage(EstablishmentImages model);

        List<EstablishmentImages>? AddImages(List<EstablishmentImages> moel);

        Restaurants? GetRestaurant(int id);

        EstablishmentImages? GetEstablishmentImage(int id);

        List<EstablishmentImages>? GetEstablishmentImages(int id);

        List<Restaurants>? GetRestaurants(int? userId);

        EstablishmentImages UpdateImage(EstablishmentImages model);

        void DeleteImage(EstablishmentImages model);

        Menus Add(Menus model);

        List<Menus>? GetMenus(int establishmentId);

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

        EstablishmentCuisines Add(EstablishmentCuisines model);

        EstablishmentCuisines UpdateCuisine(EstablishmentCuisines model);

        void DeleteCuisine(EstablishmentCuisines model);

        EstablishmentCuisines? GetEstablishmentCuisine(int id);

        List<EstablishmentCuisines>? GetEstablishmentCuisines(int? restaurantId);

        EstablishmentBookType Add(EstablishmentBookType model);

        List<EstablishmentBookType>? GetBookTypes(int? restaurantId);

        EstablishmentBookType? GetBookType(int id);

        EstablishmentBookType UpdateBookType(EstablishmentBookType model);
    }
}
