using System;
using System.Collections.Generic;
using System.Text;
using DataAcessLayer.Models.Users;

namespace DataAcessLayer.Models.Preferences
{
    public class TravelPreference
    {
        public int Id { get; set; }


        public PreferenceType Type { get; set; }
        public int TypeId { get; set; }


        public string Title { get; set; }

        public User_TravelPreference UserPreference{ get; set; }
    }
}
