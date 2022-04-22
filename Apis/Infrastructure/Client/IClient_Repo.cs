using DataAcessLayer.Models.Users;
using DataAcessLayer.ViewModels.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Infrastructure.Client
{
    public interface IClient_Repo
    {
        List<ClientUsers> GetUsers();

        ClientUsers RegisterUser(ClientUsers clientUsers);

        ClientUsers LoginUser(ClientUsers clientUsers);

        Task<ClientPublicProfile> PublicProfile(int UserId);
    }
}
