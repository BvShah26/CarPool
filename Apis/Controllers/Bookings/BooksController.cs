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
using Apis.Infrastructure.Bookings;

namespace Apis.Controllers.Bookings
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooking_Repo _bookingRepo;


        public BooksController(IBooking_Repo repo)
        {
            _bookingRepo = repo;
        }

        
        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookings()
        {
            try
            {
                var records = await _bookingRepo.GetAllBookings();
                return Ok(records);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        
        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBooking(int id)
        {
            var record = await _bookingRepo.GetBooking(id);

            if (record == null)
            {
                return NotFound();
            }

            return Ok(record);
        }

        [HttpGet("GetBookByRide/{id}")]
        public async Task<ActionResult<List<Book>>> GetBookByRide(int id)
        {
            var records = await _bookingRepo.GetRideBookings(id);

            if (records == null)
            {
                return NotFound();
            }
            return Ok(records);
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        //public async Task<IActionResult> PutBook(int id, Book book)
        //{
        //    if (id != book.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(book).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BookExists(id))
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

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            try
            {
                Book newBooking = await _bookingRepo.AddNewBooking(book);
                return CreatedAtAction("GetBooking", new { id = newBooking.Id }, newBooking);
                //return Ok(newBooking);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                throw;
            }
        }

        private bool BookExists(int id)
        {
            return _bookingRepo.isBookingExists(id);
        }
    }
}
