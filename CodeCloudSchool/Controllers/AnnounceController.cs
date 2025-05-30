using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Code_CloudSchool.Models;
using Code_CloudSchool.Data;

namespace Code_CloudSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnounceController : ControllerBase
    {
        private readonly AppDBContext _context;

        public AnnounceController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Announce
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Announcements>>> GetAnnouncements()
        {
            return await _context.Announcements.ToListAsync();
        }

        // GET: api/Announce/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Announcements>> GetAnnouncements(int id)
        {
            var announcements = await _context.Announcements.FindAsync(id);

            if (announcements == null)
            {
                return NotFound();
            }

            return announcements;
        }

        // PUT: api/Announce/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnnouncements(int id, Announcements announcements)
        {
            if (id != announcements.AnnouncementId)
            {
                return BadRequest();
            }

            _context.Entry(announcements).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnouncementsExists(id))
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

        // POST: api/Announce
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("course/{courseId}")]
        public async Task<ActionResult<Announcements>> PostAnnouncements(int courseId, [FromBody] Announcements announcements)
        {
            // Validate that the course exists
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null)
            {
                return NotFound($"Course with ID {courseId} not found.");
            }

            // set the course Id for the assignment
            announcements.CourseId = courseId;

            _context.Announcements.Add(announcements);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnnouncements", new { id = announcements.AnnouncementId }, announcements);
        }

        // DELETE: api/Announce/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncements(int id)
        {
            var announcements = await _context.Announcements.FindAsync(id);
            if (announcements == null)
            {
                return NotFound();
            }

            _context.Announcements.Remove(announcements);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnnouncementsExists(int id)
        {
            return _context.Announcements.Any(e => e.AnnouncementId == id);
        }
    }
}
