using Apis.Data;
using Apis.Infrastructure.Client;
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
        private readonly IClient_Repo _userRepo;
        public UserPreference(ApplicationDBContext context, IClient_Repo userRepo)
        {
            _context = context;
            _userRepo = userRepo;
        }
        [HttpPost]
        public async Task<IActionResult> AddPreference(User_TravelPreference preference)
        {

            if (preference != null)
            {
                try
                {
                    await _userRepo.SavePreference(preference);
                    return Ok();

                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest();
        }

        [HttpGet("GetSelectedPreferences/{TypeId}/{UserId}")]
        public async Task<IActionResult> GetSelectedPreferences(int TypeId, int UserId)
        {
            var res = await _userRepo.GetUserPreferences(TypeId, UserId);
            return Ok(res);
        }
    }
}
