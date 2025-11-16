using System;
using Code_CloudSchool.Data;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Code_CloudSchool.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Code_CloudSchool.Services;

public class TimetableGeneratorService : ITimetableGenerator
{

    private readonly ITimeSlotGen _timeSlotService;
    private readonly AppDBContext _context;
    private readonly ILogger<TimetableGeneratorService> _logger;

    public TimetableGeneratorService(ITimeSlotGen timeSlotGen, AppDBContext context, ILogger<TimetableGeneratorService> logger)
    {
        _timeSlotService = timeSlotGen;
        _context = context;
        _logger = logger;
    }

    public async Task<TimetableGenerationResultDTO> GenerateTimetableAsync()
    {
        var result = new TimetableGenerationResultDTO();
        
        try
        {
            var classes = await _context.Classes
                .Include(c => c.Lecturers)
                .Include(c => c.Student)
                .Where(c => c.TimeSlotId == null)
                .ToListAsync();

            result.TotalClassesProcessed = classes.Count;

            if (classes.Count == 0)
            {
                result.Message = "No unscheduled classes found.";
                _logger.LogInformation("Timetable generation: No unscheduled classes to process.");
                return result;
            }

            var timeSlots = await _timeSlotService.GetAllSlotsAsync();

            if (timeSlots.Count == 0)
            {
                result.Message = "No time slots available. Please generate time slots first.";
                _logger.LogWarning("Timetable generation failed: No time slots available.");
                return result;
            }

            // Preload already scheduled classes (to avoid repeated queries)
            // CRITICAL FIX: Include TimeSlot navigation property
            var scheduledClasses = await _context.Classes
                .Where(c => c.TimeSlotId != null)
                .Include(c => c.TimeSlot)  // FIXED: Added missing TimeSlot include
                .Include(c => c.Lecturers)
                .Include(c => c.Student)
                .ToListAsync();

            foreach (var cls in classes)
            {
                // Validation: Skip classes with no students or lecturers
                if (cls.Student == null || cls.Student.Count == 0 || 
                    cls.Lecturers == null || cls.Lecturers.Count == 0)
                {
                    result.SkippedEmptyClasses++;
                    result.SkippedClassIds.Add(cls.classID);
                    _logger.LogWarning($"Skipping class {cls.classID} ({cls.className}): No students or lecturers assigned.");
                    continue;
                }

                bool assigned = false;
                foreach (var slot in timeSlots)
                {
                    if (!HasLecturerConflict(cls, slot, scheduledClasses) &&
                        !HasStudentConflict(cls, slot, scheduledClasses) &&
                        !ExceedsMaxDailyLimit(cls, slot, scheduledClasses))
                    {
                        cls.TimeSlotId = slot.TimeSlotId;
                        cls.TimeSlot = slot; // Set navigation property for in-memory checks
                        // Add to scheduled list so future checks include this
                        scheduledClasses.Add(cls);
                        result.SuccessfullyScheduled++;
                        assigned = true;
                        _logger.LogInformation($"Class {cls.classID} ({cls.className}) assigned to TimeSlot {slot.TimeSlotId} ({slot.Day} {slot.StartTime}-{slot.EndTime})");
                        break;
                    }
                }

                if (!assigned)
                {
                    result.UnassignedClasses++;
                    result.UnassignedClassIds.Add(cls.classID);
                    _logger.LogWarning($"Could not assign class {cls.classID} ({cls.className}): No available time slots without conflicts.");
                }
            }

            await _context.SaveChangesAsync();

            // Build result message
            if (result.UnassignedClasses > 0)
            {
                result.Message = $"Timetable generation completed. {result.SuccessfullyScheduled} classes scheduled, {result.UnassignedClasses} classes could not be assigned.";
            }
            else if (result.SkippedEmptyClasses > 0)
            {
                result.Message = $"Timetable generation completed. {result.SuccessfullyScheduled} classes scheduled, {result.SkippedEmptyClasses} empty classes skipped.";
            }
            else
            {
                result.Message = $"Timetable generation successful. All {result.SuccessfullyScheduled} classes have been scheduled.";
            }

            _logger.LogInformation($"Timetable generation completed: {result.SuccessfullyScheduled} scheduled, {result.UnassignedClasses} unassigned, {result.SkippedEmptyClasses} skipped.");
            
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating timetable: {Message}", ex.Message);
            result.Message = $"Timetable generation failed: {ex.Message}";
            throw;
        }
    }



    private bool HasLecturerConflict(Classes cls, TimeSlot slot, List<Classes> scheduledClasses)
    {
        return scheduledClasses
            .Where(c => c.TimeSlotId == slot.TimeSlotId && c.classID != cls.classID)
            .Any(c => c.Lecturers.Any(l => cls.Lecturers.Select(lx => lx.UserId).Contains(l.UserId)));
    }

    private bool HasStudentConflict(Classes cls, TimeSlot slot, List<Classes> scheduledClasses)
    {
        return scheduledClasses
            .Where(c => c.TimeSlotId == slot.TimeSlotId && c.classID != cls.classID)
            .Any(c => c.Student.Any(s => cls.Student.Select(sx => sx.UserId).Contains(s.UserId)));
    }




    private bool ExceedsMaxDailyLimit(Classes cls, TimeSlot slot, List<Classes> scheduledClasses)
    {
        var day = slot.Day;

        // For each lecturer in this class, check how many classes they have on the same day
        foreach (var lecturer in cls.Lecturers)
        {
            var dailyLecturerClasses = scheduledClasses
                .Where(c => c.TimeSlot != null && c.TimeSlot.Day == day)
                .Count(c => c.Lecturers.Any(l => l.UserId == lecturer.UserId));

            if (dailyLecturerClasses >= 3)
                return true;
        }

        // For each student in this class, check how many classes they have on the same day
        foreach (var student in cls.Student)
        {
            var dailyStudentClasses = scheduledClasses
                .Where(c => c.TimeSlot != null && c.TimeSlot.Day == day)
                .Count(c => c.Student.Any(s => s.UserId == student.UserId));

            if (dailyStudentClasses >= 3)
                return true;
        }

        return false;
    }



}
