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
    public class VehicleColorsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public VehicleColorsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/VehicleColors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleColor>>> GetVehicle_Color()
        {
            return await _context.Vehicle_Color.ToListAsync();
        }

        // GET: api/VehicleColors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleColor>> GetVehicleColor(int id)
        {
            var vehicleColor = await _context.Vehicle_Color.FindAsync(id);

            if (vehicleColor == null)
            {
                return NotFound();
            }

            return vehicleColor;
        }

        // PUT: api/VehicleColors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleColor(int id, VehicleColor vehicleColor)
        {
            if (id != vehicleColor.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicleColor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleColorExists(id))
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

        // POST: api/VehicleColors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<VehicleColor>> PostVehicleColor(VehicleColor vehicleColor)
        {
            _context.Vehicle_Color.Add(vehicleColor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicleColor", new { id = vehicleColor.Id }, vehicleColor);
        }

        // DELETE: api/VehicleColors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VehicleColor>> DeleteVehicleColor(int id)
        {
            var vehicleColor = await _context.Vehicle_Color.FindAsync(id);
            if (vehicleColor == null)
            {
                return NotFound();
            }

            _context.Vehicle_Color.Remove(vehicleColor);
            await _context.SaveChangesAsync();

            return vehicleColor;
        }

        private bool VehicleColorExists(int id)
        {
            return _context.Vehicle_Color.Any(e => e.Id == id);
        }
    }
}
