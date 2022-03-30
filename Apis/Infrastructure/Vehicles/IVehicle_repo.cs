using DataAcessLayer.Models.VehicleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Infrastructure.Vehicles
{
    public interface IVehicle_repo
    {
        Task<List<Vehicle>> GetVehicles();
        Task<Vehicle> GetVehicle_ById(int id);
        Task<List<Vehicle>> Search_Vehicle(string SearchValue);

        Task<Vehicle> Add_Vehicle(Vehicle Vehicle);
        Task<Vehicle> Update_Vehicle(int id, Vehicle Vehicle);
        Task<Vehicle> Delete_Vehicle(int id);
        bool Vehicle_Exists(int id);
    }
}
