using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAcessLayer.ViewModels
{
    public class ClientLoginView
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(15, ErrorMessage = "Must be between 8 and 15 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string  Password { get; set; }



        public string ReturnUrl { get; set; }
        //public Boolean RememberMe { get; set; }
    }
}
