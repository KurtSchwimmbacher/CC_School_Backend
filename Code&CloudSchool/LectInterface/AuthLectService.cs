using System;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.LectInterface;

public interface AuthLectService
{
    public Task<bool> RegisterLecturer(LecturerReg lecturer);

     
}
