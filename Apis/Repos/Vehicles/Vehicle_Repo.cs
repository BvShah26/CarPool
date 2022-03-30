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
    public class Vehicle_Repo : IVehicle_repo
    {
        private readonly ApplicationDBContext _context;

        public Vehicle_Repo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Vehicle> Add_Vehicle(Vehicle Vehicle)
        {
            var result = await _context.Vehicles.AddAsync(Vehicle);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Vehicle> Delete_Vehicle(int id)
        {
            Vehicle data = await _context.Vehicles.FindAsync(id);
            if (data != null)
            {
                _context.Vehicles.Remove(data);
                await _context.SaveChangesAsync();
            }
            return data;
        }

        public async Task<List<Vehicle>> GetVehicles()
        {
            return await _context.Vehicles.Include(item => item.VehicleBrand).Include(item => item.VehicleType).ToListAsync();

        }

        public async Task<Vehicle> GetVehicle_ById(int id)
        {
            return await _context.Vehicles.FindAsync(id);
        }

        public async Task<List<Vehicle>> Search_Vehicle(string SearchValue)
        {
            List<Vehicle> searchResult = new List<Vehicle>();
            if (!String.IsNullOrEmpty(SearchValue))
            {
                searchResult = await _context.Vehicles.Where(item => item.Name.Contains(SearchValue) || item.VehicleBrand.Name.Contains(SearchValue) || 
                item.VehicleType.Title.Contains(SearchValue)).ToListAsync();
            }
            else
            {
                searchResult = await GetVehicles();
            }

            return searchResult;
        }

        public async Task<Vehicle> Update_Vehicle(int id, Vehicle Vehicle)
        {
            Vehicle record = await GetVehicle_ById(id);
            if (record != null)
            {
                record.Name = Vehicle.Name;
                record.VehicleBrandId = Vehicle.VehicleBrandId;
                record.VehicleTypeId = Vehicle.VehicleTypeId;
            }
            _context.Vehicles.Update(record);
            await _context.SaveChangesAsync();
            return record;
        }

        public bool Vehicle_Exists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }
    }
}
