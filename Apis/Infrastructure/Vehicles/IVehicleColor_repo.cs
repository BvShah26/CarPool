using DataAcessLayer.Models.VehicleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Infrastructure.Vehicles
{
    public interface IVehicleColor_repo
    {
        Task<List<VehicleColor>> GetVehicleColors();
        Task<VehicleColor> GetVehicleColor_ById(int id);
        Task<List<VehicleColor>> Search_vehicleColor(string SearchValue);

        Task<VehicleColor> Add_vehicleColor(VehicleColor vehicleColor);
        Task<VehicleColor> Update_vehicleColor(int id, VehicleColor vehicleColor);
        Task<VehicleColor> Delete_vehicleColor(int id);
        bool Color_Exists(int id);
    }
}
