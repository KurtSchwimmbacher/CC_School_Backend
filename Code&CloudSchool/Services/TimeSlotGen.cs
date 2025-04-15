using System;
using Code_CloudSchool.Data;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Services;

public class TimeSlotGen : ITimeSlotGen
{

    private readonly AppDBContext _context;

    public TimeSlotGen(AppDBContext context)
    {
        _context = context;
    }

public async Task<List<TimeSlot>> GenerateDefaultWeeklySlotsAsync()
{
    if (await _context.TimeSlots.AnyAsync())
        return await GetAllSlotsAsync();

    var slots = new List<TimeSlot>();
    var startHour = 8;
    var endHour = 16;

    foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
    {
        if (day is DayOfWeek.Saturday or DayOfWeek.Sunday) continue;

        for (int hour = startHour; hour < endHour; hour += 2) //2 hour slots
        {
            slots.Add(new TimeSlot
            {
                Day = day,
                StartTime = TimeSpan.FromHours(hour),
                EndTime = TimeSpan.FromHours(hour + 2)
            });
        }
    }

    _context.TimeSlots.AddRange(slots);
    await _context.SaveChangesAsync();

    return slots;
}


    public async Task<List<TimeSlot>> GetAllSlotsAsync()
    {
        var sortedTimeSlots = await _context.TimeSlots
            .OrderBy(slot => slot.Day)
            .ThenBy(slot => slot.StartTime)
            .ToListAsync();

        return sortedTimeSlots;
    }
}
