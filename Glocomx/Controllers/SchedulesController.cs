using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Glocomx.DTOs.Request;
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
            string hostId = Request.Query["hostId"];
            if(hostId != null)
            {
                return await _context.Schedules.Include(tags => tags.Tags).Where(sc => sc.HostId == hostId).ToListAsync();
            }
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
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
                Description = schedule.Description,
                Thumbnail = schedule.Thumbnail,
                Tags = schedule.Tags,
                Host = new UserResponseDTO
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    ProfilePic = user.ProfilePic,
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
        public async Task<ActionResult<Schedule>> PostSchedule([FromForm] ScheduleDTO schedule)
        {

            string dbFilePath = "";
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    dbFilePath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                else 
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

            var jsonTags = JsonSerializer.Deserialize<ICollection<Tags>>(schedule.Tags);
            
            Schedule newSchedule = new Schedule {
                Title = schedule.Title,
                Thumbnail = dbFilePath,
                Description = schedule.Description,
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
                HostId = schedule.HostId,
                Tags = jsonTags
            };


            _context.Schedules.Add(newSchedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchedule", new { id = schedule.Id }, schedule);
        }

        // DELETE: api/Schedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var schedule = await _context.Schedules.Include(t => t.Tags).FirstOrDefaultAsync(t => t.Id == id);
            var tags = schedule.Tags;

            if (schedule == null)
            {
                return NotFound();
            }

            _context.Tags.RemoveRange(schedule.Tags);
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
