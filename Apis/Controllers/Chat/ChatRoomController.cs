﻿using Apis.Data;
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
        private readonly ApplicationDBContext _context;

        public ChatRoomController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int RideId, int PublisherId, int UserId)
        {
            int RoomId = await _context.ChatRoom.Where(x => x.RideId == RideId
            && x.PublisherId == PublisherId
            && x.RiderId == UserId
            ).Select(x => x.Id).FirstOrDefaultAsync();



            if (RoomId != 0)
            {
                var Messages = await _context.ChatMessages.Where(x => x.RoomId == RoomId).ToListAsync();
                return Ok(new { Messages = Messages, RoomId = RoomId });
            }
            return Ok(RoomId);
        }

        [HttpGet("UserRooms/{UserId}")]
        public async Task<IActionResult> UserRooms(int UserId)
        {
            List<ChatRoom> rooms = await _context.ChatRoom.Where(x => x.PublisherId == UserId
            || x.RiderId == UserId
            )
                .Include(x => x.Publisher)
                .Include(y => y.Ride)
                .Include(x => x.Rider)
                .ToListAsync();
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
                var res = await _context.ChatRoom.AddAsync(chatRoom);
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