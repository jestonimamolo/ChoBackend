using choapi.Models;

namespace choapi.DAL
{
    public interface IBookingDAL
    {
        Bookings Add(Bookings model);

        Bookings Update(Bookings model);

        Bookings? GetBooking(int id);

        void DeleteBooking(int id);

        List<Bookings>? GetRestaurantBookings(int id);

        List<Bookings>? GetUserBookings(int id);
    }
}
