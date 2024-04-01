using Microsoft.EntityFrameworkCore;

namespace choapi.Models
{
    public class ChoDBContext : DbContext
    {
        public ChoDBContext(DbContextOptions<ChoDBContext> options) : base(options) { }

        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<Restaurant> Restaurant { get; set; }

        public virtual DbSet<Booking> Booking { get; set; }

        public virtual DbSet<RestaurantTable> RestaurantTable { get; set; }


    }
}
