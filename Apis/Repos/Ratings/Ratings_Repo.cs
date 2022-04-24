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
            return _context.PartnerRatings.Where(x => x.PartnerId == UserId).Select(x => x.Rating).Average();
        }

        public double GetPublisherRating(int PublisherId)
        {
            return _context.PublisherRatings.Where(x => x.PublisherId == PublisherId).Select(x => x.Rating).Average();
        }

        public async Task<bool> HasRatedPartner(int PartnerId, int UserId)
        {
            var result = await _context.PartnerRatings.Where(x => x.PartnerId == PartnerId && x.UserId == UserId).FirstOrDefaultAsync();
            if (result == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> HasRatedPublisher(int PublisherId, int UserId)
        {
            var result = await _context.PublisherRatings.Where(x => x.PublisherId == PublisherId && x.UserId == UserId).FirstOrDefaultAsync();
            if (result == null)
            {
                return false;
            }
            return true;
        }
    }
}
}
