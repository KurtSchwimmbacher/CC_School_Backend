using System;
using Code_CloudSchool.DTOs;

namespace Code_CloudSchool.Interfaces;

public interface IStudentReEnroll
{
    Task<bool> UpdateStudentYearLevel(string studentNumber, StudentReEnrollDTO studentReEnrollDTO);

}
