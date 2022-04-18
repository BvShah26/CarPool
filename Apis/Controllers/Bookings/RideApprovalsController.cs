using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Apis.Data;
using DataAcessLayer.Models.Rides;
using Apis.Infrastructure.Bookings;

namespace Apis.Controllers.Bookings
{
    [Route("api/[controller]")]
    [ApiController]
    public class RideApprovalsController : ControllerBase
    {
        private readonly IRideApproval_Repo _RideRequest;

        public RideApprovalsController(IRideApproval_Repo RideRequest)
        {
            _RideRequest = RideRequest;
        }


        // GET: api/RideApprovals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RideApproval>> GetRideApproval(int id)
        {

            var rideApproval = await _RideRequest.GetRequest(id);

            if (rideApproval == null)
            {
                return NotFound();
            }

            return Ok(rideApproval);
        }


        // PUT: api/RideApprovals/5
        [HttpPut("{id}")]
        //public async Task<IActionResult> PutRideApproval(int id, RideApproval rideApproval)
        //{
        //    if (id != rideApproval.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(rideApproval).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RideApprovalExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        [HttpPut("UpdateStatus/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, RideApproval rideApproval)
        {
            try
            {
                await _RideRequest.UpdateStatus(id, rideApproval);
                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //RideApproval request = (await GetRideApproval(id)).Value;
            //request.IsApproved = rideApproval.IsApproved;
            //request.IsRejected = (rideApproval.IsApproved == false) ? true : false;

            //_context.RideApprovals.Update(request);
            //await _context.SaveChangesAsync();

        }


        [HttpGet("GetUserRideRequest/{id}/{UserId}")]
        public async Task<IActionResult> GetUserRideRequest(int id, int UserId)
        {
            var requests = await _RideRequest.GetUserRideRequest(id, UserId);
            return Ok(requests);
        }

        // POST: api/RideApprovals
        [HttpPost("Request")]
        public async Task<ActionResult<RideApproval>> PostRideApproval(RideApproval rideApproval)
        {
            var record = await _RideRequest.NewRequest(rideApproval);
            return Ok(record);
            //_context.RideApprovals.Add(rideApproval);
            //await _context.SaveChangesAsync();

            //return Ok();
            //return CreatedAtAction("GetRideApproval", new { id = rideApproval.Id }, rideApproval);
        }

        // DELETE: api/RideApprovals/5
        //[HttpDelete("{id}")]

        //public async Task<ActionResult<RideApproval>> DeleteRideApproval(int id)
        //{
        //    var rideApproval = await _context.RideApprovals.FindAsync(id);
        //    if (rideApproval == null)
        //    {
        //        return NotFound();
        //    }
        //    _context.RideApprovals.Remove(rideApproval);
        //    await _context.SaveChangesAsync();
        //    return rideApproval;
        //}

    }
    
}
