using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Code_CloudSchool.Data;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;
using Code_CloudSchool.DTOs;

namespace Code_CloudSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ModulesController(AppDBContext context)
        {
            _context = context;
        }

        // POST: api/Modules/course/3
        [HttpPost("course/{courseId}")]
        public async Task<IActionResult> CreateModule(int courseId, [FromBody] ModuleCreateDTO dto)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null) return NotFound();

            var module = new Modules
            {
                GroupTitle = dto.GroupTitle,
                Title = dto.Title,
                Description = dto.Description,
                SlideUrl = dto.SlideUrl,
                AdditionalResources = dto.AdditionalResources, // Assuming this is part of the DTO
                published = dto.Published,
                CourseId = courseId,
                Course = course // Optional: EF will track this via CourseId
            };

            _context.Modules.Add(module);
            await _context.SaveChangesAsync();

            return Ok(module);
        }



        // GET: api/Modules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Modules>> GetModule(int id)
        {
            var module = await _context.Modules
                .Include(m => m.Course)
                .FirstOrDefaultAsync(m => m.moduleId == id);

            if (module == null)
            {
                return NotFound();
            }

            return module;
        }

        // GET: api/Modules/by-course/3
        [HttpGet("by-course/{courseId}")]
        public async Task<ActionResult<IEnumerable<Modules>>> GetModulesForCourse(int courseId)
        {
            return await _context.Modules
                .Where(m => m.CourseId == courseId)
                .ToListAsync();
        }

        [HttpPut("course/{courseId}/module/{id}/publish")]
        public async Task<IActionResult> UpdateModulePublishedStatus(int courseId, int id, [FromBody] bool isPublished)
        {
            var module = await _context.Modules
                .FirstOrDefaultAsync(m => m.moduleId == id && m.CourseId == courseId);

            if (module == null)
                return NotFound("Module not found or does not belong to the given course");

            module.published = isPublished;

            _context.Modules.Update(module);
            await _context.SaveChangesAsync();

            return Ok(module);
        }

    }
}
