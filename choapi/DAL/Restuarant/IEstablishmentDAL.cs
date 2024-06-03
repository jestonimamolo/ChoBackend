using choapi.Models;

namespace choapi.DAL
{
    public interface IEstablishmentDAL
    {
        Establishment Add(Establishment model);

        Establishment Update(Establishment model);

        EstablishmentImages AddImage(EstablishmentImages model);

        List<EstablishmentImages>? AddImages(List<EstablishmentImages> moel);

        Establishment? GetEstablishment(int id);

        EstablishmentImages? GetEstablishmentImage(int id);

        List<EstablishmentImages>? GetEstablishmentImages(int id);

        List<Establishment>? GetEstablishments(int? userId);

        List<Establishment>? GetEstablishmentsByCategoryId(int categoryId);

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

        List<EstablishmentCuisines>? GetEstablishmentCuisines(int? establishmentId);

        EstablishmentBookType Add(EstablishmentBookType model);

        List<EstablishmentBookType>? GetBookTypes(int? establishmentId);

        EstablishmentBookType? GetBookType(int id);

        EstablishmentBookType UpdateBookType(EstablishmentBookType model);

        Establishment? GetRestaurant(int categoryId, int id);

        List<Establishment>? GetRestaurants(int categoryId, int? userId);

        List<Establishment>? GetEstablishmentsPromotedByCategoryId(int categoryId, bool isPromoted);

        List<Establishment>? GetRestaurantsSearch(int categoryId, string keywords);
    }
}
