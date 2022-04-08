﻿using Apis.Data;
using DataAcessLayer.Models.Rides;
using DataAcessLayer.Models.Users;
using DataAcessLayer.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Controllers.Rides
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchRideController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public SearchRideController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> GetRides(SearchRide search)
        {
            List<PublishRide> rec = await _context.Publish_Rides.Where(item => item.JourneyDate == search.JourneyDate
           && item.Departure_City == search.Departure_City
           && item.Destination_City == search.Destination_City
           && item.MaxPassengers >= search.SeatCount
           && item.IsCompletelyBooked == false
           && item.IsCancelled == false
            ).Include(item => item.Publisher).ToListAsync();

            return Ok(rec);
        }

        [HttpGet("RideDetails/{id}")]
        public async Task<PublishRide> RideDetails(int id)
        {
            PublishRide ride = await _context.Publish_Rides.Where(item => item.Id == id).Include(x => x.Publisher).FirstOrDefaultAsync();
            //Uservehicle uservehicle = _context.
            return ride;
        }

        //public async Task<PublishRide> RideDetails(int RideId)
        //{

        //}

    }
}
