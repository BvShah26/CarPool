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
using Apis.Infrastructure.Vehicles;

namespace Apis.Controllers.Vehicles
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleBrandsController : ControllerBase
    {
        private readonly IVehcileBrand _vehicleBrandRepo;

        public VehicleBrandsController(IVehcileBrand vehicleBrandRepo)
        {
            _vehicleBrandRepo = vehicleBrandRepo;
        }

        // GET: api/VehicleBrands
        [HttpGet]
        public async Task<List<VehicleBrand>> GetVehcile_Brand()
        {
            return await _vehicleBrandRepo.GetVehicleBrands();
        }

        // GET: api/VehicleBrands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleBrand>> GetVehicleBrand(int id)
        {
            var vehicleBrand = await _vehicleBrandRepo.GetVehicleBrand_ById(id);

            if (vehicleBrand == null)
            {
                return NotFound();
            }

            return vehicleBrand;
        }



        [HttpGet("Search_Vehicles/{PageSize}/{SearchValue?}")]
        public async Task<ActionResult<IEnumerable<VehicleBrand>>> Search_Vehicles(int PageSize, string SearchValue)
        {
            return await _vehicleBrandRepo.Serahc_VehicleBrand(PageSize, SearchValue);
        }



        // PUT: api/VehicleBrands/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleBrand(int id, VehicleBrand vehicleBrand)
        {
            if (id != vehicleBrand.Id)
            {
                return BadRequest();
            }


            //_context.Entry(vehicleBrand).State = EntityState.Modified;

            try
            {
                await _vehicleBrandRepo.Update_Brand(id, vehicleBrand);
                return CreatedAtAction("GetVehicleBrand", new { id = vehicleBrand.Id }, vehicleBrand);

            }
            catch (Exception)
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
        }

        // POST: api/VehicleBrands

        [HttpPost]
        public async Task<ActionResult<VehicleBrand>> PostVehicleBrand(VehicleBrand vehicleBrand)
        {
            await _vehicleBrandRepo.Add_Brand(vehicleBrand);

            return CreatedAtAction("GetVehicleBrand", new { id = vehicleBrand.Id }, vehicleBrand);
        }

        // DELETE: api/VehicleBrands/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VehicleBrand>> DeleteVehicleBrand(int id)
        {
            var vehicleBrand = await _vehicleBrandRepo.Delete_Brand(id);
            if (vehicleBrand == null)
            {
                return NotFound();
            }

            return vehicleBrand;
        }

        private bool VehicleBrandExists(int id)
        {
            return _vehicleBrandRepo.Brand_Exists(id);
        }
    }
}
