using DataAcessLayer.Models.VehicleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Infrastructure.Vehicles
{
    public interface IVehcileBrand
    {
        // get requests
        Task<List<VehicleBrand>> GetVehicleBrands();
        Task<VehicleBrand> GetVehicleBrand_ById(int id);
        Task<List<VehicleBrand>> Serahc_VehicleBrand(int PageSize, string SearchValue);

        //Post Request
        Task<VehicleBrand> Add_Brand(VehicleBrand vehicleBrand);

        //Delete Request
        Task<VehicleBrand> Delete_Brand(int id);


        //Put request
        Task<VehicleBrand> Update_Brand(int id,VehicleBrand vehicleBrand);


        bool Brand_Exists(int id);

    }
}
