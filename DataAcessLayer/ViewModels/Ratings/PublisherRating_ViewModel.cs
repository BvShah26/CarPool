using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAcessLayer.ViewModels.Ratings
{
    public class PublisherRating_ViewModel
    {
        [Required]
        public int PublisherId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Rating { get; set; }
    }

   
}
