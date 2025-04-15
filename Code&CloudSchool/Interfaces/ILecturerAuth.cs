using System;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Interfaces;

public interface ILecturerAuth
{
    public Task<string> GenerateEmailAddress(string LectName);

    public Task<bool> RegisterLecturer(LecturerReg lecturer);

    public Task<string> HashPassword(string password);

    public Task<LecturerReg?> EmailExists(string email);

    public Task<LecturerReg?> LoginLecturer(string email, string password);

    public Task<LecturerReg?> GetLecturerByEmail(string email);


}
