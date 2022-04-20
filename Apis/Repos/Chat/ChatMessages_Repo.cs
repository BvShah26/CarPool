using Apis.Data;
using Apis.Infrastructure.Chat;
using DataAcessLayer.Models.Chat;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Repos.Chat
{
    public class ChatMessages_Repo : IChatMessages_Repo
    {
        private ApplicationDBContext _context;
        public ChatMessages_Repo(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<ChatMessages>> GetRoomMessages(int RoomId)
        {
            List<ChatMessages> rec = await _context.ChatMessages.Where(x => x.RoomId == RoomId)
               .Include(x => x.Sender).ToListAsync();
            return rec;
        }

        public async Task<ChatMessages> NewMessage(ChatMessages message)
        {
            var res = await _context.ChatMessages.AddAsync(message);
            await _context.SaveChangesAsync();
            return res.Entity;
        }
    }
}
