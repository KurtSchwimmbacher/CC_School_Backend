using System;
using Code_CloudSchool.DTOs;

namespace Code_CloudSchool.Interfaces;

public interface IUpdateStudentPassword
{
    Task<bool> UpdateStudentPassword(string studentNumber, StudentPasswordDTO studentPasswordDTO);
}
