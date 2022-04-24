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

        Task<bool> HasRatedPublisher(int PublisherId, int UserId);
        Task<bool> HasRatedPartner(int PartnerId, int UserId);
    }
}
