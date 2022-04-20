using Apis.Infrastructure.Chat;
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
        private readonly IChatMessages_Repo _chatMessages_Repo;

        public ChatMessageController(IChatMessages_Repo messageRepo)
        {
            _chatMessages_Repo = messageRepo;
        }

        [HttpGet("GetRoomMessages/{RoomId}")]
        public async Task<IActionResult> GetRoomMessages(int RoomId)
        {
            try
            {
                var messages = await _chatMessages_Repo.GetRoomMessages(RoomId);
                if (messages == null)
                {
                    return NotFound();
                }
                return Ok(messages);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

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
                var res = await _chatMessages_Repo.NewMessage(message);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }
    }
}
