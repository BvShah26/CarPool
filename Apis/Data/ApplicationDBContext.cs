﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAcessLayer.Models.Booking;
using DataAcessLayer.Models.Chat;
using DataAcessLayer.Models.Preferences;
using DataAcessLayer.Models.Rides;
using DataAcessLayer.Models.Users;
using DataAcessLayer.Models.VehicleModels;
using DataAcessLayer.ViewModels;
using DataAcessLayer.ViewModels.Client;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Apis.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }


        // Vehciles
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleBrand> Vehcile_Brand { get; set; }
        public DbSet<VehicleColor> Vehicle_Color { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }


        // Rides
        public DbSet<PublishRide> Publish_Rides { get; set; }
        public DbSet<RideApproval> RideApprovals { get; set; }


        // Preferences
        public DbSet<PreferenceType> PreferenceTypes { get; set; }
        public DbSet<TravelPreference> TravelPreferences { get; set; }


        //Bookings

        public DbSet<Book> Bookings { get; set; }
        public DbSet<CancellationReason> CancellationReasons { get; set; }


        // Users
        public DbSet<ClientUsers> ClientUsers { get; set; }
        public DbSet<User_TravelPreference> User_TravelPreferences { get; set; }
        public DbSet<Uservehicle> Uservehicles { get; set; }

        //public DbSet<RideDetailsView> RideDetails { get; set; }
        //public DbSet<UserRideViewModal> UserRides { get; set; }

        //chats

        public DbSet<ChatRoom> ChatRoom { get; set; }
        public DbSet<ChatMessages> ChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<PublishRide>().HasOne(item => item.Vehicle).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Vehicle>().ToTable("Vehicles");

            modelBuilder.Entity<ChatRoom>()
                .HasOne(r => r.Rider)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChatRoom>()
                .HasOne(r => r.Publisher)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
