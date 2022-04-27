using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.ViewModels.Client
{
    public class UserChangePassword
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
