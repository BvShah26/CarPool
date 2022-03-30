using Apis.Data;
using Apis.Helper;
using Apis.Infrastructure.Vehicles;
using DataAcessLayer.Models.VehicleModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Repos.Vehicles
{
    public class VehicleBrandRepo : IVehcileBrand
    {

        private readonly ApplicationDBContext _context;

        public VehicleBrandRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<VehicleBrand> Add_Brand(VehicleBrand vehicleBrand)
        {
            var result = await _context.Vehcile_Brand.AddAsync(vehicleBrand);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public bool Brand_Exists(int id)
        {
            return _context.Vehcile_Brand.Any(e => e.Id == id);
        }

        public async Task<VehicleBrand> Delete_Brand(int id)
        {
            VehicleBrand data = await _context.Vehcile_Brand.FindAsync(id);
            if (data != null)
            {
                _context.Vehcile_Brand.Remove(data);
                await _context.SaveChangesAsync();
            }

            return data;

        }

        public async Task<List<VehicleBrand>> GetVehicleBrands()
        {
            return await _context.Vehcile_Brand.ToListAsync();
        }

        public async Task<VehicleBrand> GetVehicleBrand_ById(int id)
        {
            return await _context.Vehcile_Brand.FindAsync(id);
        }

        public async Task<List<VehicleBrand>> Serahc_VehicleBrand(int PageSize, string SearchValue)
        {
            int PageNumber = 1;
            var searchResult = _context.Vehcile_Brand.AsNoTracking();
            if (!String.IsNullOrEmpty(SearchValue))
            {
                searchResult = _context.Vehcile_Brand.Where(item => item.Name.Contains(SearchValue));
            }
            return await Paginated<VehicleBrand>.CreateAsync(searchResult, PageNumber, PageSize);

        }

        public async Task<VehicleBrand> Update_Brand(int id, VehicleBrand vehicleBrand)
        {
            VehicleBrand record = await GetVehicleBrand_ById(id);
            if (record != null)
            {
                record.Name = vehicleBrand.Name;
            }
            _context.Vehcile_Brand.Update(record);
            await _context.SaveChangesAsync();
            return record;
        }
    }
}
