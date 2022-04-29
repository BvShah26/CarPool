using Apis.Data;
using Apis.Infrastructure.Ratings;
using DataAcessLayer.Models.Ratings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Repos.Ratings
{
    public class Ratings_Repo : IRatings_Repo
    {
        private readonly ApplicationDBContext _context;
        public Ratings_Repo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task AddPartnerRating(RidePartnerRating ratings)
        {
            await _context.PartnerRatings.AddAsync(ratings);
            await _context.SaveChangesAsync();
        }

        public async Task AddPublisherRating(PublisherRatings ratings)
        {
            await _context.PublisherRatings.AddAsync(ratings);
            await _context.SaveChangesAsync();
        }

        public double GetPatrtnerRatings(int UserId)
        {

            var rec = _context.PartnerRatings.Where(x => x.PartnerId == UserId).Select(x => (int) x.Rate).ToList();
            if (rec.Count > 0)
            {
                double averageRating = rec.Average();
                return averageRating;
            }
            return 0;
        }

        public double GetPublisherRating(int PublisherId)
        {

            var rec = _context.PublisherRatings.Where(x => x.PublisherId == PublisherId).Select(x => (int) x.Rating).ToList();
            if (rec.Count > 0)
            {
                
                double averageRating = rec.Average();
                return averageRating;
            }
            return 0;
        }

        public bool HasRatedPartner(int PartnerId, int UserId)
        {
            var result = _context.PartnerRatings.Where(x => x.PartnerId == PartnerId && x.UserId == UserId).FirstOrDefault();
            if (result == null)
            {
                return false;
            }
            return true;
        }

        public bool HasRatedPublisher(int PublisherId, int UserId)
        {
            var result =  _context.PublisherRatings.Where(x => x.PublisherId == PublisherId && x.UserId == UserId).FirstOrDefault();
            if (result == null)
            {
                return false;
            }
            return true;
        }

        public  bool HasRated_AllPartner(int RideId, int SessionUserId)
        {

            List<int> PartnerId =  _context.Bookings.Where(x => x.Publish_RideId == RideId && x.IsCancelled == false).Select(x => x.RiderId).ToList();
            if (PartnerId.Count > 0)
            {
                foreach (int partner in PartnerId)
                {
                    var rated = _context.PartnerRatings.Where(x => x.PartnerId == partner && x.UserId == SessionUserId).FirstOrDefault();
                    if (rated == null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

