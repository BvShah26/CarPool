﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAcessLayer.Models.Booking;
using DataAcessLayer.Models.Preferences;
using DataAcessLayer.Models.Rides;
using DataAcessLayer.Models.Users;
using DataAcessLayer.Models.VehicleModels;
using Microsoft.EntityFrameworkCore;

namespace Apis.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }


        // Vehciles
        public DbSet<Vehicle> Vehciles { get; set; }
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
        public DbSet<User> Users { get; set; }
        public DbSet<User_TravelPreference> User_TravelPreferences { get; set; }
        public DbSet<Uservehicle> Uservehicles { get; set; }

      
    }
}