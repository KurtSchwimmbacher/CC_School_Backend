using System;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Interfaces;

public interface LecturerAuthService
{
    public Task<string> HashPassword (string password);

    public Task<string> GenerateLecturerId ();

    public Task<string> GenerateEmailAdress(string LectName);

    public Task<bool> RegisterLecturer ( LecturerReg lecturer);

    public Task<string> EmailExists (string LecEmail);
}
