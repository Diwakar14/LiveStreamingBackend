using Glocomx.DTOs.Request;
using Glocomx.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Glocomx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {

        private readonly GlocomxDbContext _context;

        public ChatController(GlocomxDbContext context)
        {
            _context = context;
        }

        [HttpGet("{streamId}")]
        public async Task<ActionResult<IEnumerable<Chat>>> GetChats(string streamId)
        {
            return await _context.Chats
                .Where(item => item.StreamId == streamId)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Chat>> PostChats(ChatDTO chats)
        {
            var newChat = new Chat
            {
                StreamId = chats.StreamId,
                Message = chats.Message,
                UserId = chats.UserId
            };

            _context.Chats.Add(newChat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostChats", new { id = newChat.Id }, newChat);
        }
    }
}
