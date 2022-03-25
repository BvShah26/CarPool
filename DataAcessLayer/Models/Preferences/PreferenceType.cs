using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.Models.Preferences
{
    public class PreferenceType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<TravelPreference> Preferences { get; set; }
    }
}
