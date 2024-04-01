using choapi.Models;

namespace choapi.DAL
{
    public interface IBookingDAL
    {
        Booking? Add(Booking model);

        Booking? GetBooking(int id);

        void DeleteBooking(int id);
    }
}
