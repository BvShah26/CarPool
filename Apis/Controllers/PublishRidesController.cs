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
            //return await _context.Publish_Rides.Include(x => x.Publisher).ToListAsync();

            //Just to make work easy [ Checking Details ]
            return await _context.Publish_Rides.Include(x => x.Publisher).Where(x => x.JourneyDate >= DateTime.Now).ToListAsync();
        }


        [HttpGet("UserVehicles/{UserId}")]
        public async Task<ActionResult<IEnumerable<PublishRide>>> GetUser_PublishedRide(int UserId)
        {

            //validate timing as 12-04-2022:00-00>= 12-04-2022:09-44 = False
            //Should include time in jounery date ..  and calculate timing based on Distance Matrix
            //Don't Ask Destination Time from user [ Choice ]

            List<PublishRide> result = await _context.Publish_Rides.Where(item => item.PublisherId == UserId)
                .Where(x => x.JourneyDate >= DateTime.Now).Include(x => x.Ride_Approval)
                .Include(item => item.Vehicle).ThenInclude(item => item.Vehicle).ToListAsync();

           //Improvement To Do
           //Include Table Only If IsInstantApproval ==  False
            
            return Ok(result);
        }

        // GET: api/PublishRides/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublishRide>> GetPublishRide(int id)
        {
            //To Do : 
            //Include Ride_Approval only If InstantApproval == false

            //var publishRide = await _context.Publish_Rides.Include(x => x.Ride_Approval).FindAsync(id);


            //Include Table Tea
            var publishRide = await _context.Publish_Rides
                .Where(x => x.Id == id)
                .Include(x => x.Ride_Approval).ThenInclude(rideApproval => rideApproval.User)
                .Include(x => x.Booking).ThenInclude(bookings=> bookings.Rider)
                .FirstOrDefaultAsync();
            if (publishRide == null)
            {
                return NotFound();
            }

            return publishRide;
        }


        // PUT: api/PublishRides/5
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

        [HttpGet("ChangeBookStatus/{id}")]
        public async Task<IActionResult> ChangeBookStatus(int id)
        {
            PublishRide ride = (await GetPublishRide(id)).Value;
            ride.IsCompletelyBooked = true;
            await _context.SaveChangesAsync();
            return Ok();
        }

        // POST: api/PublishRides
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
