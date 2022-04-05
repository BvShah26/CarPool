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
        private readonly ApplicationDBContext _context;
        private readonly IClientVehicle_Repo _Repo;

        public UservehiclesController(ApplicationDBContext context, IClientVehicle_Repo Repo)
        {
            _context = context;
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



        // PUT: api/Uservehicles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUservehicle(int id, Uservehicle uservehicle)
        {
            if (id != uservehicle.Id)
            {
                return BadRequest();
            }

            _context.Entry(uservehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UservehicleExists(id))
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

        // POST: api/Uservehicles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Uservehicle>> PostUservehicle(Uservehicle uservehicle)
        {
            _context.Uservehicles.Add(uservehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUservehicle", new { id = uservehicle.Id }, uservehicle);
        }

        // DELETE: api/Uservehicles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Uservehicle>> DeleteUservehicle(int id)
        {
            var uservehicle = await _context.Uservehicles.FindAsync(id);
            if (uservehicle == null)
            {
                return NotFound();
            }

            _context.Uservehicles.Remove(uservehicle);
            await _context.SaveChangesAsync();

            return uservehicle;
        }

        private bool UservehicleExists(int id)
        {
            return _context.Uservehicles.Any(e => e.Id == id);
        }
    }
}
