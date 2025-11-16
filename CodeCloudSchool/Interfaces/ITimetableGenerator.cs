using System;
using Code_CloudSchool.DTOs;

namespace Code_CloudSchool.Interfaces;

public interface ITimetableGenerator
{
    public Task<TimetableGenerationResultDTO> GenerateTimetableAsync();
}
