using System;
using Code_CloudSchool.DTOs;

namespace Code_CloudSchool.Interfaces;

public interface IStudentStatus
{
    Task<bool> UpdateStudentStatus(string studentNumber, UpdateStudentStatusDTO statusDTO);
}
