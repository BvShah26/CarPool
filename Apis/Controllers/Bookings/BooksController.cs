using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Apis.Data;
using DataAcessLayer.Models.Booking;

namespace Apis.Controllers.Bookings
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public BooksController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookings()
        {
            return await _context.Bookings.Include(x => x.Publish_Ride.Publisher).Include(y => y.Rider).ToListAsync();
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

            //New Booking Added
            var result = await _context.Bookings.AddAsync(book);

            //Checking For IsCompletlyBooked Status

            var TotalSeat_Occuiped = _context.Bookings.Sum(x => x.SeatQty);
            int MaxSeats = book.Publish_Ride.MaxPassengers;


            if(TotalSeat_Occuiped == int.MaxValue)
            {
                //Call Api of Publish Ride Controller to update status


                //we should call api for this 
                //and update status from it's own api

            }

            //Doing IsCompletly Booked
            //Doing this to improve performance 

            var data = _context.Publish_Rides.Where(x => x.PublisherId == book.Publish_RideId);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
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
