using Apis.Data;
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
    public class ChatRoomController : ControllerBase
    {
        private readonly IChatRoom_Repo _chatRoom_Repo;
 
        public ChatRoomController(IChatRoom_Repo _Repo)
        {
            _chatRoom_Repo = _Repo;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int RideId, int PublisherId, int UserId)
        {

            int RoomId = await _chatRoom_Repo.GetRoomIdByRiders(RideId, PublisherId, UserId);
            return Ok(RoomId);
        }

        [HttpGet("UserRooms/{UserId}")]
        public async Task<IActionResult> UserRooms(int UserId)
        {
            List<ChatRoom> rooms = await _chatRoom_Repo.GetUser_ChatRooms(UserId);
            return Ok(rooms);
        }


        [HttpPost("CreateRoom")]
        public async Task<IActionResult> CreateRoom(ChatRoom chatRoom)
        {
            if (chatRoom == null)
            {
                return BadRequest();
            }
            try
            {
                var res = await _chatRoom_Repo.CreateRoom(chatRoom);
                return Ok(res);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
