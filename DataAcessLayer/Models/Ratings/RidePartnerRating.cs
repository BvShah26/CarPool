using DataAcessLayer.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.Models.Ratings
{
    public class RidePartnerRating
    {
        public int Id { get; set; }
        public int PartnerId { get; set; }
        public ClientUsers Partner { get; set; }

        public int UserId { get; set; }
        public ClientUsers User { get; set; }

        public PartnerRatingTitle Rate { get; set; }

    }
    public enum PartnerRatingTitle
    {
        Worst = 1,
        Disappointing = 2,
        Okay = 3,
        Good = 4,
        Excellent = 5,
    }
}
