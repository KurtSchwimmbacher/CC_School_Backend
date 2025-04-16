using Code_CloudSchool.Data;
using Code_CloudSchool.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimetableController : ControllerBase
    {

        private readonly ITimetableGenerator _timetableService;

        private readonly ITimeSlotGen _timeSlotService;
        private readonly AppDBContext _context;

        public TimetableController(ITimetableGenerator timetableService, AppDBContext context, ITimeSlotGen timeSlotService)
        {
            _timeSlotService = timeSlotService;
            _timetableService = timetableService;
            _context = context;
        }


        [HttpPost("generate-timeslots")]
        public async Task<IActionResult> GenerateTimeSlots()
        {
             try
            {
                var timeSlots = await _timeSlotService.GenerateDefaultWeeklySlotsAsync();
                return Ok(new
                {
                    Message = "Time slots generated successfully.",
                    TimeSlotCount = timeSlots.Count
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to generate time slots: {ex.Message}");
            }
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateTimetable()
        {
            try
            {
                await _timetableService.GenerateTimetableAsync();
                return Ok("Timetable generation successful.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Timetable generation failed: {ex.Message}");
            }
        }


        [HttpGet("scheduled")]
        public async Task<IActionResult> GetScheduledClasses()
        {
            var scheduled = await _context.Classes
                .Where(c => c.TimeSlotId != null)
                .Include(c => c.TimeSlot)
                .ToListAsync();

            return Ok(scheduled);
        }


    }
}
