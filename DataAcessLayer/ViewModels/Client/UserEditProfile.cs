using DataAcessLayer.Helper;
using DataAcessLayer.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.ViewModels.Client
{
    public class UserEditProfile
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
        public Gender Gender { get; set; }

        public long MobileNumber { get; set; }

        [MinimumAgeAttribute(18)]
        public DateTime BirthDate { get; set; }
        //public int LicenseNumber { get; set; }
        public string Bio { get; set; }
    }
}
