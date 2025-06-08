using Code_CloudSchool.Data;
using Code_CloudSchool.DTOs;
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
                .Where(cls => cls.TimeSlotId != null)
                .Include(cls => cls.TimeSlot)
                .Include(cls => cls.Student)
                .Include(cls => cls.Lecturers)
                .Select(cls => new TimetableDTO
                {
                    ClassID = cls.classID,
                    ClassName = cls.className,
                    ClassDescription = cls.classDescription,
                    TimeSlotId = cls.TimeSlotId.Value,
                    Day = cls.TimeSlot.Day.ToString(),
                    StartTime = cls.TimeSlot.StartTime.ToString(@"hh\:mm"),
                    EndTime = cls.TimeSlot.EndTime.ToString(@"hh\:mm"),
                    Students = cls.Student.Select(s => s.Name + " " + s.LastName).ToList(),
                    Lecturers = cls.Lecturers.Select(l => l.Name + " " + l.LastName).ToList()
                })
                .ToListAsync();

            return Ok(scheduled);
        }


        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetStudentTimetable(Guid studentId)
        {
            var student = await _context.Students
                .Include(s => s.Classes)
                    .ThenInclude(c => c.TimeSlot)
                .Include(s => s.Classes)
                    .ThenInclude(c => c.Lecturers)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null) return NotFound("Student not found");

            var studentTimetable = student.Classes
                .Where(c => c.TimeSlotId != null)
                .Select(cls => new TimetableDTO
                {
                    ClassID = cls.classID,
                    ClassName = cls.className,
                    ClassDescription = cls.classDescription,
                    TimeSlotId = cls.TimeSlotId.Value,
                    Day = cls.TimeSlot.Day.ToString(),
                    StartTime = cls.TimeSlot.StartTime.ToString(@"hh\:mm"),
                    EndTime = cls.TimeSlot.EndTime.ToString(@"hh\:mm"),
                    Students = cls.Student.Select(s => s.Name + " " + s.LastName).ToList(),
                    Lecturers = cls.Lecturers.Select(l => l.Name + " " + l.LastName).ToList()
                })
                .ToList();

            return Ok(studentTimetable);
        }


    }
}
