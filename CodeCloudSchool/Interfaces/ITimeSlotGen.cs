using System;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Interfaces;

public interface ITimeSlotGen
{
    public Task<List<TimeSlot>> GenerateDefaultWeeklySlotsAsync();

    public Task<List<TimeSlot>> GetAllSlotsAsync();
}
