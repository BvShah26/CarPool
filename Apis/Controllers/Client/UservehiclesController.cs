using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Apis.Data;
using DataAcessLayer.Models.Users;
using Apis.Infrastructure.Client;

namespace Apis.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class UservehiclesController : ControllerBase
    {
        private readonly IClientVehicle_Repo _Repo;

        public UservehiclesController(IClientVehicle_Repo Repo)
        {
            _Repo = Repo;
        }

        // GET: api/Uservehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Uservehicle>>> GetUservehicles()
        {
            return await _Repo.GetUservehicles();
        }

        // GET: api/Uservehicles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Uservehicle>>> GetUservehicle(int id)
        {
            var uservehicle = await _Repo.GetUservehicleByUser(id);

            if (uservehicle == null)
            {
                return NotFound();
            }

            return uservehicle;
        }


        [HttpGet("HasVehicle/{id}")]
        public async Task<IActionResult> HasVehicle(int id)
        {
            bool uservehicle = await _Repo.HasAnyVehicle(id);
            return Ok(uservehicle);
        }



        // POST: api/Uservehicles
        [HttpPost]
        public async Task<ActionResult<Uservehicle>> PostUservehicle(Uservehicle uservehicle)
        {
            var record = await _Repo.AddVehicle(uservehicle);
           
            return CreatedAtAction("GetUservehicle", new { id = record.Id }, record);
        }

        // DELETE: api/Uservehicles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Uservehicle>> DeleteUservehicle(int id)
        {
            Boolean res =await _Repo.DeleteUserVehicle(id);
            if(res == true)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
