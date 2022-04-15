using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DataAcessLayer.Models.Booking;
using DataAcessLayer.Models.Chat;
using DataAcessLayer.Models.Location;
using DataAcessLayer.Models.Rides;

namespace DataAcessLayer.Models.Users
{
    public class ClientUsers
    {
        public int Id { get; set; }

        public string Name { get; set; }


        //public City City { get; set; } // To Be Remove
        //public int CityId { get; set; } //To Be Remove

        public string Address { get; set; }
        public Gender? Gender { get; set; }

        public long MobileNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int LicenseNumber { get; set; }
        public string ProfileImage { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Bio { get; set; }
        public DateTime RegistrationDate { get; set; } //DateTime.Now()

        public ICollection<Uservehicle> Vehicles { get; set; } = new HashSet<Uservehicle>();

        public ICollection<User_TravelPreference> UserPreference { get; set; } = new HashSet<User_TravelPreference>();

        public ICollection<PublishRide> Published_Rides{ get; set; } = new HashSet<PublishRide>();
        public ICollection<Book> BookingDetails { get; set; } = new HashSet<Book>();

        public ICollection<RideApproval> RideApprovals { get; set; } = new HashSet<RideApproval>();
        public ICollection<ChatRoom> ChatRooms { get; set; } = new HashSet<ChatRoom>();
    }

    public enum Gender
    {
        Female,
        Male
    }
}
