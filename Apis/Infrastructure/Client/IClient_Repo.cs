using DataAcessLayer.Models.Users;
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
    }
}
