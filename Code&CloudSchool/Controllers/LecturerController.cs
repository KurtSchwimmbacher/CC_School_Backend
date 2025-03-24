using Code_CloudSchool.Data;
using Code_CloudSchool.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturerController : ControllerBase
    {

        private readonly AppDbContext _context;

        public LecturerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/LecturerReg
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LecturerReg>>> GetLecturerReg()
        {
            return await _context.Lecturer.ToListAsync();
        }

        // GET: api/LecturerReg/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LecturerReg>> GetLecturerReg(int id)
        {
            var lecturerReg = await _context.Lecturer.FindAsync(id);

            if (lecturerReg == null)
            {
                return NotFound();
            }

            return lecturerReg;
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
            _context.Lecturer.Add(lecturerReg);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLecturerReg", new { id = lecturerReg.Id }, lecturerReg);
        }


                // DELETE: api/LecturerReg/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLecturerReg(int id)
        {
            var lecturerReg = await _context.Lecturer.FindAsync(id);
            if (lecturerReg == null)
            {
                return NotFound();
            }

            _context.Lecturer.Remove(lecturerReg);
            await _context.SaveChangesAsync();

            return NoContent();
        }




        private bool LecturerRegExists(int id)
        {
            return _context.Lecturer.Any(e => e.Id == id);
        }


    }
}
