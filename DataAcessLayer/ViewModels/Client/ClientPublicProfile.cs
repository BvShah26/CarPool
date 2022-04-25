using DataAcessLayer.Models.Preferences;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.ViewModels.Client
{
    public class ClientPublicProfile
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string ProfileImage { get; set; }


        public int TotalRides { get; set; }
        public DateTime RegistrationDate { get; set; }

        public List<string> Preferences { get; set; }
        public double PublisherRating { get; set; }
        public double PartnerRating { get; set; }

    }
}
