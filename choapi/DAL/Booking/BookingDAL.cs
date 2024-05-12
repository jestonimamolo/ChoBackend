﻿using choapi.Models;

namespace choapi.DAL
{
    public class BookingDAL : IBookingDAL
    {
        private readonly ChoDBContext _context;

        public BookingDAL(ChoDBContext choDBContext)
        {
            _context = choDBContext;
        }

        public Bookings Add(Bookings model)
        {
            _context.Bookings.Add(model);

            _context.SaveChanges();

            return model;
        }

        public Bookings Update(Bookings model)
        {
            _context.Bookings.Update(model);

            _context.SaveChanges();

            return model;
        }

        public Bookings? GetBooking(int id)
        {
            return _context.Bookings.FirstOrDefault(r => r.Booking_Id == id && r.Is_Deleted != true);
        }

        public void DeleteBooking(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(r => r.Booking_Id == id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
            }
        }

        public List<Bookings>? GetRestaurantBookings(int id)
        {
            return _context.Bookings.Where(b => b.Restaurant_Id == id && b.Is_Deleted != true).ToList();
        }

        public List<Bookings>? GetUserBookings(int id)
        {
            return _context.Bookings.Where(b => b.User_Id == id && b.Is_Deleted != true).ToList();
        }


    }
}
