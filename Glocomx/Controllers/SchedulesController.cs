using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glocomx.DTOs.Response;
using Glocomx.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Glocomx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly GlocomxDbContext _context;

        public SchedulesController(GlocomxDbContext context)
        {
            _context = context;
        }

        // GET: api/Schedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetSchedules()
        {
            return await _context.Schedules.Include(tags => tags.Tags).ToListAsync();
        }

        // GET: api/Schedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleReponseDTO>> GetSchedule(int id)
        {
            var schedule = await _context.Schedules.Include(tag => tag.Tags).FirstOrDefaultAsync(i => i.Id == id);
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == schedule.HostId);

            if (schedule == null || user == null)
            {
                return NotFound();
            }

            var response = new ScheduleReponseDTO
            {
                Id = schedule.Id,
                Title = schedule.Title,
                LiveSessionId = schedule.LiveSessionId,
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
                Description = schedule.Description,
                Tags = schedule.Tags,
                Host = new UserResponseDTO
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Id = user.Id
                }
            };

            

            return response;
        }

        // PUT: api/Schedules/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchedule(int id, Schedule schedule)
        {
            if (id != schedule.Id)
            {
                return BadRequest();
            }

            _context.Entry(schedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Schedules
        [HttpPost]
        public async Task<ActionResult<Schedule>> PostSchedule(Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchedule", new { id = schedule.Id }, schedule);
        }

        // DELETE: api/Schedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.Id == id);
        }
    }
}
