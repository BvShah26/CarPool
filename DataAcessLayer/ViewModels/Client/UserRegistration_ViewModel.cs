using DataAcessLayer.Helper;
using DataAcessLayer.Models.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.ViewModels.Client
{
    public class UserRegistration_ViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
        public Gender? Gender { get; set; }

        public long MobileNumber { get; set; }

        [MinimumAgeAttribute(18)]
        public DateTime BirthDate { get; set; }
        //public int LicenseNumber { get; set; }
        public IFormFile ProfileImage { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
    }
}
