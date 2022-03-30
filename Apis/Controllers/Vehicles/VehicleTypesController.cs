using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAcessLayer.Models.VehicleModels;
using Apis.Infrastructure.Vehicles;

namespace Apis.Controllers.Vehicles
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypesController : ControllerBase
    {
        private readonly IVehicleType_repo _vehicleType_Repo;

        public VehicleTypesController(IVehicleType_repo vehicleType_Repo)
        {
            _vehicleType_Repo = vehicleType_Repo;
        }

        // GET: api/VehicleTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleType>>> GetVehicleTypes()
        {
            return await _vehicleType_Repo.GetVehicleTypes();
        }

        // GET: api/VehicleTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleType>> GetVehicleType(int id)
        {
            var vehicleType = await _vehicleType_Repo.GetVehicleType_ById(id);

            if (vehicleType == null)
            {
                return NotFound();
            }

            return vehicleType;
        }

        [HttpGet("Search/{SearchValue}")]
        public async Task<List<VehicleType>> Search(string SearchValue)
        {
            return await _vehicleType_Repo.Search_vehicleType(SearchValue);
        }

        // PUT: api/VehicleTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleType(int id, VehicleType vehicleType)
        {
            if (id != vehicleType.Id)
            {
                return BadRequest();
            }


            try
            {
                await _vehicleType_Repo.Update_vehicleType(id, vehicleType);
                return CreatedAtAction("GetVehicleType", new { id = vehicleType.Id }, vehicleType);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/VehicleTypes
        
        [HttpPost]
        public async Task<ActionResult<VehicleType>> PostVehicleType(VehicleType vehicleType)
        {
            await _vehicleType_Repo.Add_vehicleType(vehicleType);

            return CreatedAtAction("GetVehicleType", new { id = vehicleType.Id }, vehicleType);
        }

        // DELETE: api/VehicleTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VehicleType>> DeleteVehicleType(int id)
        {
            var vehicleType = await _vehicleType_Repo.Delete_vehicleType(id);
            if (vehicleType == null)
            {
                return NotFound();
            }
            return vehicleType;
        }

        

        private bool VehicleTypeExists(int id)
        {
            return _vehicleType_Repo.Type_Exists(id);
        }
    }
}
