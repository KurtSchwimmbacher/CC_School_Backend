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
    public class LecturerRegController : ControllerBase
    {
        private readonly AppDBContext _context;

        public LecturerRegController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/LecturerReg
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LecturerReg>>> GetLecturerReg()
        {
            return await _context.Lecturers.ToListAsync();
        }

        // GET: api/LecturerReg/5
        [HttpGet("{id}")]
        public IActionResult GetLecturer(int id)
        {
            var lecturer = new LecturerDTO
            {
                Id = id,
                LectName = "John",
                LecLastName = "Doe",
                LecEmail = "john.doe@example.com",
                PhoneNumber = "123-456-7890",
                Department = "Computer Science",
                DateOfJoining = DateTime.Now,
                IsActive = true
            };

            return Ok(lecturer);  // Return the DTO as the response
        }

        // PUT: api/LecturerReg/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLecturerReg(int id, LecturerReg lecturerReg)
        {
            if (id != lecturerReg.Id)
            {
                return BadRequest();
            }

            _context.Entry(lecturerReg).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LecturerRegExists(id))
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

        // POST: api/LecturerReg
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LecturerReg>> PostLecturerReg(LecturerReg lecturerReg)
        {
            _context.Lecturers.Add(lecturerReg);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLecturerReg", new { id = lecturerReg.Id }, lecturerReg);
        }
        

        // DELETE: api/LecturerReg/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLecturerReg(int id)
        {
            var lecturerReg = await _context.Lecturers.FindAsync(id);
            if (lecturerReg == null)
            {
                return NotFound();
            }

            _context.Lecturers.Remove(lecturerReg);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LecturerRegExists(int id)
        {
            return _context.Lecturers.Any(e => e.Id == id);
        }

        
    }

    internal class LecturerDTO
    {
        public int Id { get; set; }
        public string LectName { get; set; }
        public string LecLastName { get; set; }
        public string LecEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string Department { get; set; }
        public DateTime DateOfJoining { get; set; }
        public bool IsActive { get; set; }
    }
}

