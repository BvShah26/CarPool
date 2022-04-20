using DataAcessLayer.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Infrastructure.Client
{
    public interface IClientVehicle_Repo
    {
        Task<List<Uservehicle>> GetUservehicles();
        Task<List<Uservehicle>> GetUservehicleByUser(int UserId);

        Task<Uservehicle> AddVehicle(Uservehicle vehicle);
        public Task<bool> DeleteUserVehicle(int UserVehicleId);

    }
}
