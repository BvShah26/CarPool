using DataAcessLayer.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.Models.Ratings
{
    public class PublisherRatings
    {
        public int Id { get; set; }
        public int PublisherId { get; set; }
        public ClientUsers Publisher { get; set; }
        public int UserId { get; set; }
        public ClientUsers User { get; set; }

        public RatingTitle Rating { get; set; }
    }
    public enum RatingTitle
    {
        Excellent = 3,
        Average = 2,
        Poor = 1
    }
}
