using Apis.Data;
using Apis.Infrastructure.Bookings;
using DataAcessLayer.Models.Rides;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Repos.Bookings
{
    public class RideApproval_Repo : IRideApproval_Repo
    {
        private ApplicationDBContext _context;
        public RideApproval_Repo(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<RideApproval> GetRequest(int id)
        {
            //Make ViewModel

            return await _context.RideApprovals.Include(x => x.User).Include(x => x.Ride).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<RideApproval> GetUserRideRequest(int RideId, int UserId)
        {
            return await _context.RideApprovals.Where(x => x.RideId == RideId && x.UserId == UserId).FirstOrDefaultAsync();
        }

        public async Task<RideApproval> NewRequest(RideApproval rideApproval)
        {
            try
            {

                var rideRequest = _context.RideApprovals.Add(rideApproval);

                await _context.SaveChangesAsync();
                return rideRequest.Entity;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task UpdateStatus(int id, RequestStaus RequestStatus)
        {
            RideApproval request = await GetRequest(id);

            if (request == null)
            {
                throw new Exception("Record Not Found");
            }
            request.Status = RequestStatus;
            _context.RideApprovals.Update(request);
            await _context.SaveChangesAsync();
        }
    }
}
