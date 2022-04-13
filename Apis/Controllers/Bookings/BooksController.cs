using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Apis.Data;
using DataAcessLayer.Models.Booking;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace Apis.Controllers.Bookings
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IConfiguration _config;


        public BooksController(ApplicationDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookings()
        {

            return await _context.Bookings.Include(x => x.Publish_Ride.Publisher).Include(y => y.Rider).ToListAsync();

            //Just To Make Easy ( JourneyDate >= DateTime.Now )
            //return await _context.Bookings
            //    .Include(x => x.Publish_Ride).ThenInclude(publishRide => publishRide.Publisher)
            //    .Include(y => y.Rider).ToListAsync();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Bookings.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }
        [HttpGet("GetBookByRide/{id}")]
        public async Task<ActionResult<List<Book>>> GetBookByRide(int id)
        {
            var book = await _context.Bookings.Where(x => x.Publish_RideId == id).Include(x => x.Rider).ToListAsync();

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: api/Books
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {

            int MaxPassenger = book.Publish_Ride.MaxPassengers;


            book.Publish_Ride = null;
            //As it overrides PublishRideId to 0


            //New Booking Added
            var result =  await _context.Bookings.AddAsync(book);
            await _context.SaveChangesAsync();
            //SaveChanges here so able to get correct Total_Occuiped


            //Checking For IsCompletlyBooked Status

            int TotalSeat_Occuiped = _context.Bookings.Where(x => x.Publish_RideId == book.Publish_RideId).Sum(x => x.SeatQty);

            if (TotalSeat_Occuiped == MaxPassenger)
            {
                HttpClient httpClient = new HttpClient();

                httpClient.BaseAddress = new Uri(_config.GetValue<string>("proxyUrl"));

                HttpResponseMessage responseMessage = httpClient.GetAsync($"PublishRides/ChangeBookStatus/{book.Publish_RideId}").Result;

                if (!responseMessage.IsSuccessStatusCode)
                {
                    throw new Exception("Fail : change Booking status ");
                }
            }


            return Ok();

            // Doing IsCompletly Booked
            // Doing this to improve performance


            //return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await _context.Bookings.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(book);
            await _context.SaveChangesAsync();

            return book;
        }

        private bool BookExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}
