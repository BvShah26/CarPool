using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Apis.Data;
using DataAcessLayer.Models.VehicleModels;
using Apis.Infrastructure.Vehicles;

namespace Apis.Controllers.Vehicles
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicle_repo _vehicle_Repo;

        public VehiclesController(IVehicle_repo vehicle_Repo)
        {
            _vehicle_Repo = vehicle_Repo;
        }

        // GET: api/Vehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            //return await _context.Vehicles.Include(item => item.VehicleBrand).Select(item => new { Vehicle = item. }).ToListAsync();
            return await _vehicle_Repo.GetVehicles();
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            var vehicle = await _vehicle_Repo.GetVehicle_ById(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        [HttpGet("search/{SearchValue}")]
        public async Task<List<Vehicle>> search(string SearchValue)
        {
            return await _vehicle_Repo.Search_Vehicle(SearchValue);
        }

        // PUT: api/Vehicles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle(int id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return BadRequest();
            }

            try
            {
                await _vehicle_Repo.Update_Vehicle(id, vehicle);
                return CreatedAtAction("GetVehicle", new { id = vehicle.Id }, vehicle);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_vehicle_Repo.Vehicle_Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // POST: api/Vehicles
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
        {
            await _vehicle_Repo.Add_Vehicle(vehicle);

            return CreatedAtAction("GetVehicle", new { id = vehicle.Id }, vehicle);
        }

        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vehicle>> DeleteVehicle(int id)
        {
            var vehicle = await _vehicle_Repo.Delete_Vehicle(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return vehicle;
        }
    }
}
