using Apis.Data;
using DataAcessLayer.Models.Preferences;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreferenceTypeController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public PreferenceTypeController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: PreferenceTypes

        // GET: api/<PreferenceTypeController>
        [HttpGet]
        public List<PreferenceType> Get()
        {
            var obj =_context.PreferenceTypes.ToList();
            return obj;
        }

        // GET api/<PreferenceTypeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PreferenceTypeController>
        [HttpPost]
        public void Post([FromBody] PreferenceType Type)
        {
            _context.PreferenceTypes.Add(Type);
            _context.SaveChanges();
            return;
        }

        // PUT api/<PreferenceTypeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PreferenceTypeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
