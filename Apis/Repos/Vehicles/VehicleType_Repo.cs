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
    public class VehicleType_Repo : IVehicleType_repo
    {

        private readonly ApplicationDBContext _context;

        public VehicleType_Repo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<VehicleType> Add_vehicleType(VehicleType vehicleType)
        {
            var result = await _context.VehicleTypes.AddAsync(vehicleType);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<VehicleType> Delete_vehicleType(int id)
        {
            VehicleType data = await _context.VehicleTypes.FindAsync(id);
            if (data != null)
            {
                _context.VehicleTypes.Remove(data);
                await _context.SaveChangesAsync();
            }

            return data;
        }

        public async Task<List<VehicleType>> GetVehicleTypes()
        {
            return await _context.VehicleTypes.ToListAsync();

        }

        public async Task<VehicleType> GetVehicleType_ById(int id)
        {
            return await _context.VehicleTypes.FindAsync(id);

        }

        public async Task<List<VehicleType>> Search_vehicleType(string SearchValue)
        {
            List<VehicleType> searchResult = await _context.VehicleTypes.ToListAsync();
            if (!String.IsNullOrEmpty(SearchValue))
            {
                searchResult = await _context.VehicleTypes.Where(item => item.Title.Contains(SearchValue)).ToListAsync();
            }
            return searchResult;
        }

        public bool Type_Exists(int id)
        {
            return _context.VehicleTypes.Any(e => e.Id == id);

        }

        public async Task<VehicleType> Update_vehicleType(int id, VehicleType vehicleType)
        {
            VehicleType record = await GetVehicleType_ById(id);
            if (record != null)
            {
                record.Title = vehicleType.Title;
            }
            _context.VehicleTypes.Update(record);
            await _context.SaveChangesAsync();
            return record;
        }
    }
}
