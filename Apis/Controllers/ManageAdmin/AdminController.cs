using Apis.Data;
using DataAcessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Controllers.ManageAdmin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public AdminController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpPost("Login")]
        public IActionResult LoginUser(AdminModel admin)
        {
            AdminModel record = _context.Admin.FirstOrDefault(item => item.EmailAddress == admin.EmailAddress && item.Password == admin.Password);
            if (record != null)
            {

                return Ok(record);
            }
            return Unauthorized();
        }

    }
}
