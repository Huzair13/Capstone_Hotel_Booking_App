﻿using BookingServices.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingServices.Contexts
{
    public class HotelBookingContext :DbContext
    {
        public HotelBookingContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
           .Property(b => b.TotalAmount)
           .HasColumnType("decimal(18, 2)");
        }
    }
}