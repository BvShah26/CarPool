using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Apis.Data;
using DataAcessLayer.Models.Rides;

namespace Apis.Controllers.Bookings
{
    [Route("api/[controller]")]
    [ApiController]
    public class RideApprovalsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public RideApprovalsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/RideApprovals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RideApproval>>> GetRideApprovals()
        {
            return await _context.RideApprovals.ToListAsync();
        }

        // GET: api/RideApprovals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RideApproval>> GetRideApproval(int id)
        {
            var rideApproval = await _context.RideApprovals.FindAsync(id);

            if (rideApproval == null)
            {
                return NotFound();
            }

            return rideApproval;
        }


        // PUT: api/RideApprovals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRideApproval(int id, RideApproval rideApproval)
        {
            if (id != rideApproval.Id)
            {
                return BadRequest();
            }

            _context.Entry(rideApproval).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RideApprovalExists(id))
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

        [HttpPut("UpdateStatus/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, RideApproval rideApproval)
        {
            RideApproval request = (await GetRideApproval(id)).Value;
            request.IsApproved = rideApproval.IsApproved;
            request.IsRejected = (rideApproval.IsApproved == false) ? true : false;

            _context.RideApprovals.Update(request);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/RideApprovals
        [HttpPost("Request")]
        public async Task<ActionResult<RideApproval>> PostRideApproval(RideApproval rideApproval)
        {
            _context.RideApprovals.Add(rideApproval);
            await _context.SaveChangesAsync();

            return Ok();
            //return CreatedAtAction("GetRideApproval", new { id = rideApproval.Id }, rideApproval);
        }

        // DELETE: api/RideApprovals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RideApproval>> DeleteRideApproval(int id)
        {
            var rideApproval = await _context.RideApprovals.FindAsync(id);
            if (rideApproval == null)
            {
                return NotFound();
            }

            _context.RideApprovals.Remove(rideApproval);
            await _context.SaveChangesAsync();

            return rideApproval;
        }

        private bool RideApprovalExists(int id)
        {
            return _context.RideApprovals.Any(e => e.Id == id);
        }
    }
}
