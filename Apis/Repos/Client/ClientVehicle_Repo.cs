using Apis.Data;
using Apis.Infrastructure.Client;
using DataAcessLayer.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Repos.Client
{
    public class ClientVehicle_Repo : IClientVehicle_Repo
    {
        private readonly ApplicationDBContext _context;
        public ClientVehicle_Repo(ApplicationDBContext context)
        {
            _context = context;
        }

        // Update Vehicle Image [ Implement ]



        public async Task<Uservehicle> AddVehicle(Uservehicle vehicle)
        {
            var result = await _context.Uservehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteUserVehicle(int UserVehicleId)
        {
            Uservehicle uservehicle = await _context.Uservehicles.FindAsync(UserVehicleId);
            if(uservehicle == null)
            {
                return false;
            }
            uservehicle.IsDeleted = true;

            _context.Uservehicles.Update(uservehicle);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Uservehicle>> GetUservehicleByUser(int UserId)
        {
            var rec = await _context.Uservehicles.Where(x => x.UserOwnerId == UserId)
                .Where(x => x.IsDeleted == false)
                .Include(item => item.Vehicle)
                .Include(item => item.Color)

                .ToListAsync();
            return rec;
        }

        public async Task<List<Uservehicle>> GetUservehicles()
        {
            return await _context.Uservehicles.ToListAsync();
        }

        public async Task<bool> HasAnyVehicle(int UserId)
        {
            var rec = await _context.Uservehicles.FirstOrDefaultAsync(x => x.UserOwnerId == UserId);
            if(rec!=null)
            {
                return true;

            }
            return false;
        }
    }
}
