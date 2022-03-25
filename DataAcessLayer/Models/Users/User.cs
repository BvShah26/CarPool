using System;
using System.Collections.Generic;
using System.Text;
using DataAcessLayer.Models.Location;

namespace DataAcessLayer.Models.Users
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }


        //public City City { get; set; } // To Be Remove
        //public int CityId { get; set; } //To Be Remove

        public string Address { get; set; }
        public Gender Gender { get; set; }

        public long MobileNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int LicenseNumber { get; set; } //?
        public string ProfileImage { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Bio { get; set; }
        public DateTime RegistrationDate { get; set; } //DateTime.Now()

        public ICollection<Uservehicle> Vehicles { get; set; }
    }

    public enum Gender
    {
        Female = 1,
        Male = 2
    }
}
