using Apis.Data;
using DataAcessLayer.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPreference : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public UserPreference(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> AddPreference(User_TravelPreference preference)
        {
            if(preference !=null)
            {
                await _context.User_TravelPreferences.AddAsync(preference);
                await _context.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }
    }
}
