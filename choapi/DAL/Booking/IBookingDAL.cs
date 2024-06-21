using choapi.Models;

namespace choapi.DAL
{
    public interface IBookingDAL
    {
        Bookings Add(Bookings model);

        Bookings Update(Bookings model);

        Bookings? GetBooking(int id);

        void DeleteBooking(int id);

        List<Bookings>? GetEstablishmentBookings(int id);

        List<Bookings>? GetUserBookings(int id);

        List<Bookings>? GetEstablishmentBookingsByDateCreated(int id, DateTime from, DateTime to);

        List<Bookings>? GetEstablishmentBookingsByMonthlyReport(int id, int month, int year, string? status);

        List<Bookings>? GetEstablishmentBookingsByDateFilter(int id, DateTime date);
    }
}
