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
        public async Task<Uservehicle> AddVehicle(Uservehicle vehicle)
        {
            var result = await _context.Uservehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<List<Uservehicle>> GetUservehicleByUser(int id)
        {
            var rec = await _context.Uservehicles.Where(x => x.UserOwnerId == id).Include(item => item.Vehicle)
                .Include(item => item.Color)
                .ToListAsync();
            return rec;
        }

        public async Task<List<Uservehicle>> GetUservehicles()
        {
            return await _context.Uservehicles.ToListAsync();

        }
    }
}
