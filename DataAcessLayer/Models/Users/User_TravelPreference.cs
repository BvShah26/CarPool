using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DataAcessLayer.Models.Preferences;

namespace DataAcessLayer.Models.Users
{
    public class User_TravelPreference
    {
        [Key]
        public int Id { get; set; }


        public int Travel_PreferenceId { get; set; }
        public TravelPreference Travel_Preference { get; set; }



        public int UserId { get; set; }
        public User User { get; set; }


    }
}
