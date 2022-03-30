using Apis.Data;
using Apis.Infrastructure.Vehicles;
using DataAcessLayer.Models.VehicleModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Repos.Vehicles
{
    public class VehicleColor_Repo : IVehicleColor_repo
    {
        private readonly ApplicationDBContext _context;

        public VehicleColor_Repo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<VehicleColor> Add_vehicleColor(VehicleColor vehicleColor)
        {
             var result = await _context.Vehicle_Color.AddAsync(vehicleColor);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public bool Color_Exists(int id)
        {
            return _context.Vehicle_Color.Any(e => e.Id == id);

        }

        public async Task<VehicleColor> Delete_vehicleColor(int id)
        {
            VehicleColor data = await _context.Vehicle_Color.FindAsync(id);
            if (data != null)
            {
                _context.Vehicle_Color.Remove(data);
                await _context.SaveChangesAsync();
            }

            return data;
        }

        public async Task<List<VehicleColor>> GetVehicleColors()
        {
            return await _context.Vehicle_Color.ToListAsync();

        }

        public async Task<VehicleColor> GetVehicleColor_ById(int id)
        {
            return await _context.Vehicle_Color.FindAsync(id);

        }

        public async Task<List<VehicleColor>> Search_vehicleColor(string SearchValue)
        {
            List<VehicleColor> searchResult = await _context.Vehicle_Color.ToListAsync();
            if (!String.IsNullOrEmpty(SearchValue))
            {
                searchResult = await _context.Vehicle_Color.Where(item => item.Color.Contains(SearchValue)).ToListAsync();
            }
            return searchResult;
        }

        public async Task<VehicleColor> Update_vehicleColor(int id, VehicleColor vehicleColor)
        {
            VehicleColor record = await GetVehicleColor_ById(id);
            if (record != null)
            {
                record.Color = vehicleColor.Color;
            }
            _context.Vehicle_Color.Update(record);
            await _context.SaveChangesAsync();
            return record;
        }
    }
}
