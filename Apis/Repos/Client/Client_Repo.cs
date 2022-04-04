using Apis.Data;
using Apis.Infrastructure.Client;
using DataAcessLayer.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Repos.Client
{
    public class Client_Repo : IClient_Repo
    {
        private readonly ApplicationDBContext _context;
        public Client_Repo(ApplicationDBContext context)
        {
            _context = context;
        }
        public List<ClientUsers> GetUsers()
        {
            return _context.Users.ToList();
        }

        public ClientUsers RegisterUser(ClientUsers clientUsers)
        {
            clientUsers.RegistrationDate = DateTime.Now;
            var result = _context.Users.Add(clientUsers);
            _context.SaveChanges();
            return result.Entity;

        }
    }
}
