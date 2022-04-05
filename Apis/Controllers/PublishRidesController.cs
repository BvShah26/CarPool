using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Apis.Data;
using DataAcessLayer.Models.Rides;

namespace Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishRidesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public PublishRidesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/PublishRides
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublishRide>>> GetPublish_Rides()
        {
            return await _context.Publish_Rides.ToListAsync();
        }

        // GET: api/PublishRides/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublishRide>> GetPublishRide(int id)
        {
            var publishRide = await _context.Publish_Rides.FindAsync(id);

            if (publishRide == null)
            {
                return NotFound();
            }

            return publishRide;
        }

        // PUT: api/PublishRides/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublishRide(int id, PublishRide publishRide)
        {
            if (id != publishRide.Id)
            {
                return BadRequest();
            }

            _context.Entry(publishRide).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublishRideExists(id))
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

        // POST: api/PublishRides
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PublishRide>> PostPublishRide(PublishRide publishRide)
        {
            _context.Publish_Rides.Add(publishRide);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPublishRide", new { id = publishRide.Id }, publishRide);
        }

        // DELETE: api/PublishRides/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PublishRide>> DeletePublishRide(int id)
        {
            var publishRide = await _context.Publish_Rides.FindAsync(id);
            if (publishRide == null)
            {
                return NotFound();
            }

            _context.Publish_Rides.Remove(publishRide);
            await _context.SaveChangesAsync();

            return publishRide;
        }

        private bool PublishRideExists(int id)
        {
            return _context.Publish_Rides.Any(e => e.Id == id);
        }
    }
}
