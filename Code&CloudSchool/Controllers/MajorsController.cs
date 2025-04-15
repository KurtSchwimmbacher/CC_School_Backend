using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Code_CloudSchool.Data;
using Code_CloudSchool.Models;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.DTOs;

namespace Code_CloudSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MajorsController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IMajorServices _majorServices;

        public MajorsController(AppDBContext context, IMajorServices majorServices)
        {
            _context = context;
            _majorServices = majorServices;
        }

        // GET: api/Majors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Majors>>> GetMajors()
        {
            return await _context.Majors.ToListAsync();
        }

        // GET: api/Majors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Majors>> GetMajors(int id)
        {
            var majors = await _context.Majors.FindAsync(id);

            if (majors == null)
            {
                return NotFound();
            }

            return majors;
        }

        [HttpGet("getMajorDetails/{id}")]
        public async Task<ActionResult<MajorDetailsDTO>> GetMajorDetails(int id)
        {
            var majorDetails = await _majorServices.GetMajorDetails(id);

            if (majorDetails == null)
            {
                return NotFound();
            }

            return Ok(majorDetails);
        }

        [HttpGet("getCredits/{id}")]
        public async Task<ActionResult<MajorCreditsDTO>> GetMajorCreditsAsync(int id)
        {
            var majorCredits = await _majorServices.GetMajorCreditsAsync(id);

            if (majorCredits == default)
            {
                return NotFound();
            }

            return Ok(majorCredits);
        }

        [HttpGet("getCoursesByMajor/{id}")]

        public async Task<ActionResult> GetCoursesByMajorAsync(int id)
        {
            var coursesByMajors = await _majorServices.GetCoursesByMajorAsync(id);

            if (coursesByMajors == null || !coursesByMajors.Any())
            {
                return NotFound();
            }

            return Ok(coursesByMajors);
        }
        [HttpGet("getStudentsByMajor/{id}")]

        public async Task<ActionResult> GetStudentsByMajorAsync(int id)
        {
            var studentsByMajors = await _majorServices.GetCoursesByMajorAsync(id);

            if (studentsByMajors == null || !studentsByMajors.Any())
            {
                return NotFound();
            }

            return Ok(studentsByMajors);
        }

        // PUT: api/Majors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMajors(int id, Majors majors)
        {
            if (id != majors.Id)
            {
                return BadRequest();
            }

            _context.Entry(majors).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MajorsExists(id))
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

        [HttpPut("updateMajorDetails/{id}")]
        public async Task<ActionResult> UpdateMajorDetailsAsync(int id, [FromBody] MajorDetailsDTO detailsDTO)
        {

            if (id <= 0 || detailsDTO == null)
            {
                return BadRequest("Invalid input parameters");
            }

            var existingMajor = await _context.Majors.FindAsync(id);

            if (existingMajor == null)
            {
                return NotFound($"major with Id: {id} does not exist");
            }

            bool hasChanged = false;

            if (existingMajor.MajorName != detailsDTO.MajorName)
            {
                existingMajor.MajorName = detailsDTO.MajorName;

                hasChanged = true;
            }

            if (existingMajor.MajorCode != detailsDTO.MajorCode)
            {
                existingMajor.MajorCode = detailsDTO.MajorCode;

                hasChanged = true;
            }

            if (existingMajor.MajorDescription != detailsDTO.MajorDescription)
            {
                if (detailsDTO.MajorDescription != null)
                {
                    existingMajor.MajorDescription = detailsDTO.MajorDescription;
                }

                hasChanged = true;
            }

            if (hasChanged)
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {

                    return StatusCode(500, "an error occured while updating the major");
                }
            }

            return NoContent();
        }

        [HttpPut("updateMajorCredits/{id}")]
        public async Task<IActionResult> UpdateMajorCreditsAsync(int id, MajorCreditsDTO creditsDTO)
        {
            var result = await _majorServices.UpdateMajorCreditsAsync(id, creditsDTO);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Majors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Majors>> PostMajors(Majors majors)
        {
            _context.Majors.Add(majors);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMajors", new { id = majors.Id }, majors);
        }

        // DELETE: api/Majors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMajors(int id)
        {
            var majors = await _context.Majors.FindAsync(id);
            if (majors == null)
            {
                return NotFound();
            }

            _context.Majors.Remove(majors);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MajorsExists(int id)
        {
            return _context.Majors.Any(e => e.Id == id);
        }
    }
}
