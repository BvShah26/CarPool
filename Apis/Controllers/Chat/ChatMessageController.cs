using Apis.Data;
using DataAcessLayer.Models.Chat;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Controllers.Chat
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessageController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ChatMessageController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("GetRoomMessages/{RoomId}")]
        public async Task<IActionResult> GetRoomMessages(int RoomId)
        {
            List<ChatMessages> rec = await _context.ChatMessages.Where(x => x.RoomId == RoomId)
                .Include(x => x.Sender).ToListAsync();
            return Ok(rec);
        }

        [HttpPost]
        public async Task<IActionResult> ChatMessage(ChatMessages message)
        {
            if (message == null)
            {
                return BadRequest();
            }
            try
            {
                var res = await _context.ChatMessages.AddAsync(message);
                await _context.SaveChangesAsync();
                return Ok(res.Entity);
            }
            catch (Exception ex)
            {
                var a = ex;
            }
            return BadRequest();
        }
    }
}
