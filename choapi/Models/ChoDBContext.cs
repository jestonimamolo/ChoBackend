﻿using Microsoft.EntityFrameworkCore;

namespace choapi.Models
{
    public class ChoDBContext : DbContext
    {
        public ChoDBContext(DbContextOptions<ChoDBContext> options) : base(options) { }

        public virtual DbSet<Category> Category { get; set; }

        public virtual DbSet<Users> Users { get; set; }

        public virtual DbSet<Establishment> Establishment { get; set; }

        public virtual DbSet<EstablishmentImages> EstablishmentImages { get; set; }

        public virtual DbSet<Menus> Menus { get; set; }

        public virtual DbSet<Availability> Availability { get; set; }

        public virtual DbSet<NonOperatingHours> NonOperatingHours { get; set; }

        public virtual DbSet<EstablishmentCuisines> EstablishmentCuisines { get; set; }

        public virtual DbSet<Cuisines> Cuisines { get; set; }

        public virtual DbSet<Bookings> Bookings { get; set; }

        public virtual DbSet<EstablishmentBookType> EstablishmentBookType { get; set; }

        public virtual DbSet<EstablishmentTable> EstablishmentTable { get; set; }

        public virtual DbSet<Credits> Credits { get; set; }

        public virtual DbSet<Transaction> Transaction { get; set; }

        public virtual DbSet<Manager> Manager { get; set; }

        public virtual DbSet<Promotion> Promotion { get; set; }

        public virtual DbSet<FCMNotification> FCMNotification { get; set; }

        public virtual DbSet<SaveEstablishment> SaveEstablishment { get; set; }

        public virtual DbSet<Review> Review { get; set; }
        
        public virtual DbSet<CardDetails> CardDetails { get; set; }

        public virtual DbSet<AppInfo> AppInfo { get; set; }

        public virtual DbSet<Notification> Notification { get; set; }

        public virtual DbSet<Loyalty> Loyalty { get; set; }

        public virtual DbSet<Invite> Invite { get; set; }

        public virtual DbSet<Payment> Payment { get; set; }
    }
}
