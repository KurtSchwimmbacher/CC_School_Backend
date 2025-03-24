using System;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.LectInterface;

public interface AuthLectService
{
    public Task<bool> RegisterLecturer(LecturerReg lecturer);

    public Task<string> HashPassword(string password);

    public Task<LecturerReg?> EmailExists(string email);

    public Task<LecturerReg?> LoginLecturer(string email, string password);

    public Task<LecturerReg?> GetLecturerByEmail(string email);

     
}
