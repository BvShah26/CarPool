using Apis.Infrastructure.Client;
using DataAcessLayer.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Apis.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientUserController : ControllerBase
    {
        private readonly IClient_Repo _Repo;

        public ClientUserController(IClient_Repo repo)
        {
            _Repo = repo;
        }
        // GET: api/<ClientUserController>
        [HttpGet]
        public IEnumerable<ClientUsers> Get()
        {
            return _Repo.GetUsers();
        }

        // GET api/<ClientUserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ClientUserController>
        [HttpPost]
        public ClientUsers Post(ClientUsers clientUsers)
        {
            return _Repo.RegisterUser(clientUsers);
        }

        // PUT api/<ClientUserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClientUserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
