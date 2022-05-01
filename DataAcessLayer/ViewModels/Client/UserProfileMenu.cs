using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.ViewModels.Client
{
    public class UserProfileMenu
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserProfile { get; set; }
        public string bio { get; set; }


        public ICollection<UserVehicles_ViewModel> Vehicles { get; set; }
        public List<string> Preference { get; set; }

    }
}
