using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Apis.Data;
using DataAcessLayer.Models.VehicleModels;

namespace Apis.Controllers.Vehicles
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public VehiclesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Vehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehciles()
        {
            return await _context.Vehciles.ToListAsync();
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            var vehicle = await _context.Vehciles.FindAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        // PUT: api/Vehicles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle(int id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
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

        // POST: api/Vehicles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
        {
            _context.Vehciles.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicle", new { id = vehicle.Id }, vehicle);
        }

        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vehicle>> DeleteVehicle(int id)
        {
            var vehicle = await _context.Vehciles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehciles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return vehicle;
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehciles.Any(e => e.Id == id);
        }
    }
}
