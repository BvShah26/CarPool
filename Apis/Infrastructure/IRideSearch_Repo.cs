using DataAcessLayer.Models.Rides;
using DataAcessLayer.ViewModels;
using DataAcessLayer.ViewModels.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Infrastructure
{
    //Baki
    public interface IRideSearch_Repo
    {
        Task<List<PublishRide>> GetRides(SearchRide search);
        Task<UserRideDetailsViewModel> RideDetails(int id);

        Task GetRideRate(int id);

        Task VerifyRide(int RideId);
    }
}
