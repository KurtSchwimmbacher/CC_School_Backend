using System;
using Code_CloudSchool.Data;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Services;

public class TimetableGeneratorService : ITimetableGenerator
{

    private readonly ITimeSlotGen _timeSlotService;
    private readonly AppDBContext _context;

    public TimetableGeneratorService(ITimeSlotGen timeSlotGen, AppDBContext context)
    {
        _timeSlotService = timeSlotGen;
        _context = context;
    }

    public async Task GenerateTimetableAsync()
    {
       try
        {
            var classes = await _context.Classes
                .Include(c => c.Lecturers)
                .Include(c => c.Student)
                .Where(c => c.TimeSlotId == null)
                .ToListAsync();

            var timeSlots = await _timeSlotService.GetAllSlotsAsync();


            // Preload already scheduled classes (to avoid repeated queries)
            var scheduledClasses = await _context.Classes
                .Where(c => c.TimeSlotId != null)
                .Include(c => c.Lecturers)
                .Include(c => c.Student)
                .ToListAsync();


            foreach (var cls in classes)
            {
                foreach (var slot in timeSlots)
                {
                    if (!HasLecturerConflict(cls, slot, scheduledClasses) &&
                        !HasStudentConflict(cls, slot, scheduledClasses) &&
                        !ExceedsMaxDailyLimit(cls, slot, scheduledClasses))
                        
                    {
                        cls.TimeSlotId = slot.TimeSlotId;
                        scheduledClasses.Add(cls); 
                        Console.WriteLine($"Class {cls.classID} assigned to TimeSlot {slot.TimeSlotId}");
                        break;
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Log the exception (e.g., using a logging framework)
            Console.WriteLine($"Error generating timetable: {ex.Message}");
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
                .Where(c => c.TimeSlot?.Day == day)
                .Count(c => c.Lecturers.Any(l => l.UserId == lecturer.UserId));

            if (dailyLecturerClasses >= 3)
                return true;
        }

        // For each student in this class, check how many classes they have on the same day
        foreach (var student in cls.Student)
        {
            var dailyStudentClasses = scheduledClasses
                .Where(c => c.TimeSlot?.Day == day)
                .Count(c => c.Student.Any(s => s.UserId == student.UserId));

            if (dailyStudentClasses >= 3)
                return true;
        }

        return false;
    }



}
