using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Apis.Data;
using DataAcessLayer.Models.VehicleModels;
using Apis.Helper;

namespace Apis.Controllers.Vehicles
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleBrandsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public VehicleBrandsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/VehicleBrands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleBrand>>> GetVehcile_Brand()
        {
            return await _context.Vehcile_Brand.ToListAsync();
        }

        // GET: api/VehicleBrands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleBrand>> GetVehicleBrand(int id)
        {
            var vehicleBrand = await _context.Vehcile_Brand.FindAsync(id);

            if (vehicleBrand == null)
            {
                return NotFound();
            }

            return vehicleBrand;
        }

        [HttpGet("Search_Vehicles/{PageSize}/{SearchValue?}")]
        public async Task<ActionResult<IEnumerable<VehicleBrand>>> Search_Vehicles(int PageSize, string SearchValue)
        {
            int PageNumber = 1;
            var searchResult = _context.Vehcile_Brand.AsNoTracking();
            if (!String.IsNullOrEmpty(SearchValue))
            {
                searchResult = _context.Vehcile_Brand.Where(item => item.Name.Contains(SearchValue));
            }
            return await Paginated<VehicleBrand>.CreateAsync(searchResult, PageNumber, PageSize);

        }



        // PUT: api/VehicleBrands/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleBrand(int id, VehicleBrand vehicleBrand)
        {
            if (id != vehicleBrand.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicleBrand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleBrandExists(id))
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

        // POST: api/VehicleBrands

        [HttpPost]
        public async Task<ActionResult<VehicleBrand>> PostVehicleBrand(VehicleBrand vehicleBrand)
        {
            _context.Vehcile_Brand.Add(vehicleBrand);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicleBrand", new { id = vehicleBrand.Id }, vehicleBrand);
        }

        // DELETE: api/VehicleBrands/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VehicleBrand>> DeleteVehicleBrand(int id)
        {
            var vehicleBrand = await _context.Vehcile_Brand.FindAsync(id);
            if (vehicleBrand == null)
            {
                return NotFound();
            }

            _context.Vehcile_Brand.Remove(vehicleBrand);
            await _context.SaveChangesAsync();

            return vehicleBrand;
        }

        private bool VehicleBrandExists(int id)
        {
            return _context.Vehcile_Brand.Any(e => e.Id == id);
        }
    }
}
