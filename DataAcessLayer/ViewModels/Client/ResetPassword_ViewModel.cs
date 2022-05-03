using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAcessLayer.ViewModels.Client
{
    public class ResetPassword_ViewModel
    {

        public string Email { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword", ErrorMessage = "Password And ConfirmPassword Must be Same")]
        
        public string ConfirmPassword { get; set; }
    }
}

