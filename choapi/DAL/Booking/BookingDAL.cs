using choapi.Models;

namespace choapi.DAL
{
    public class BookingDAL : IBookingDAL
    {
        private readonly ChoDBContext _context;

        public BookingDAL(ChoDBContext choDBContext)
        {
            _context = choDBContext;
        }

        public Booking? Add(Booking model)
        {
            if (model != null)
            {
                _context.Booking.Add(model);

                _context.SaveChanges();
            }
            return model;
        }

        public Booking? GetBooking(int id)
        {
            return _context.Booking.FirstOrDefault(r => r.Booking_Id == id);
        }

        public void DeleteBooking(int id)
        {
            var booking = _context.Booking.FirstOrDefault(r => r.Booking_Id == id);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
            }
        }
    }
}
