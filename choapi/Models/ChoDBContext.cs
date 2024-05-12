using Microsoft.EntityFrameworkCore;

namespace choapi.Models
{
    public class ChoDBContext : DbContext
    {
        public ChoDBContext(DbContextOptions<ChoDBContext> options) : base(options) { }

        public virtual DbSet<Users> Users { get; set; }

        public virtual DbSet<Restaurants> Restaurants { get; set; }

        public virtual DbSet<RestaurantImages> RestaurantImages { get; set; }

        public virtual DbSet<Menus> Menus { get; set; }

        public virtual DbSet<RestaurantAvailability> RestaurantAvailability { get; set; }

        public virtual DbSet<NonOperatingHours> NonOperatingHours { get; set; }

        public virtual DbSet<RestaurantCuisines> RestaurantCuisines { get; set; }

        public virtual DbSet<Cuisines> Cuisines { get; set; }

        public virtual DbSet<Bookings> Bookings { get; set; }

        public virtual DbSet<RestaurantBookType> RestaurantBookType { get; set; }

        public virtual DbSet<RestaurantTable> RestaurantTable { get; set; }

        public virtual DbSet<Credits> Credits { get; set; }

        public virtual DbSet<Transaction> Transaction { get; set; }

        public virtual DbSet<Manager> Manager { get; set; }

        public virtual DbSet<Promotion> Promotion { get; set; }

        public virtual DbSet<FCMNotification> FCMNotification { get; set; }

        public virtual DbSet<Payment> Payment { get; set; }


    }
}
