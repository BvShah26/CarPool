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
    public class VehicleColorsController : ControllerBase
    {
        private readonly IVehicleColor_repo _vehicleColor_Repo;

        public VehicleColorsController( IVehicleColor_repo vehicleColor_Repo)
        {
            _vehicleColor_Repo = vehicleColor_Repo;
        }

        // GET: api/VehicleColors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleColor>>> GetVehicle_Color()
        {
            return await _vehicleColor_Repo.GetVehicleColors();
        }

        // GET: api/VehicleColors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleColor>> GetVehicleColor(int id)
        {
            var vehicleColor = await _vehicleColor_Repo.GetVehicleColor_ById(id);

            if (vehicleColor == null)
            {
                return NotFound();
            }

            return vehicleColor;
        }

        [HttpGet("Search/{SearchValue}")]
        public async Task<List<VehicleColor>> Search(string SearchValue)
        {
            return await _vehicleColor_Repo.Search_vehicleColor(SearchValue);
        }

        // PUT: api/VehicleColors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleColor(int id, VehicleColor vehicleColor)
        {
            if (id != vehicleColor.Id)
            {
                return BadRequest();
            }

            try
            {
                await _vehicleColor_Repo.Update_vehicleColor(id, vehicleColor);
                return CreatedAtAction("GetVehicleColor", new { id = vehicleColor.Id }, vehicleColor);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_vehicleColor_Repo.Color_Exists(id))
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
        [HttpPost]
        public async Task<ActionResult<VehicleColor>> PostVehicleColor(VehicleColor vehicleColor)
        {
            await _vehicleColor_Repo.Add_vehicleColor(vehicleColor);

            return CreatedAtAction("GetVehicleColor", new { id = vehicleColor.Id }, vehicleColor);
        }

        // DELETE: api/VehicleColors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VehicleColor>> DeleteVehicleColor(int id)
        {
            var vehicleColor = await _vehicleColor_Repo.Delete_vehicleColor(id);
            if (vehicleColor == null)
            {
                return NotFound();
            }

            return vehicleColor;
        }
    }
}
