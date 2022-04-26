using DataAcessLayer.Models.Ratings;
using System.Threading.Tasks;

namespace Apis.Infrastructure.Ratings
{
    public interface IRatings_Repo
    {
        double GetPatrtnerRatings(int UserId);
        double GetPublisherRating(int PublisherId);

        Task AddPublisherRating(PublisherRatings ratings);
        Task AddPartnerRating(RidePartnerRating ratings);

        bool HasRatedPublisher(int PublisherId, int UserId);
        Task<bool> HasRatedPartner(int PartnerId, int UserId);


        bool HasRated_AllPartner(int RideId, int SessionUserId);
    }
}
