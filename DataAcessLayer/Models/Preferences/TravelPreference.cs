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


        public ICollection<User_TravelPreference> User_preference { get; set; } = new HashSet<User_TravelPreference>();
    }
}
