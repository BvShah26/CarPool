using DataAcessLayer.Models.Rides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Infrastructure.Bookings
{
    public interface IRideApproval_Repo
    {
        Task UpdateStatus(int id, RequestStaus RequestStatus);

        Task<RideApproval> GetRequest(int id);

        Task<RideApproval> NewRequest(RideApproval rideApproval);

        Task<RideApproval> GetUserRideRequest(int RideId,int UserId);

    }
}
