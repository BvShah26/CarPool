using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAcessLayer.ViewModels.Client
{
    public class UserChangePassword
    {
        public int UserId { get; set; }
        [Required]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "NewPassword And ConfirmPassword Must be Same")]
        public string ConfirmPassword { get; set; }
    }
}
