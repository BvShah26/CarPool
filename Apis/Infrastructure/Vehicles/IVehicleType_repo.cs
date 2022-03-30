using DataAcessLayer.Models.VehicleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Infrastructure.Vehicles
{
    public interface IVehicleType_repo
    {
        Task<List<VehicleType>> GetVehicleTypes();
        Task<VehicleType> GetVehicleType_ById(int id);
        Task<List<VehicleType>> Search_vehicleType(string SearchValue);

        Task<VehicleType> Add_vehicleType(VehicleType vehicleType);
        Task<VehicleType> Update_vehicleType(int id, VehicleType vehicleType);
        Task<VehicleType> Delete_vehicleType(int id);
        bool Type_Exists(int id);
    }
}
