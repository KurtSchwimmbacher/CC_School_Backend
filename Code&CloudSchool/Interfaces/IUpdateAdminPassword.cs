using System;
using Code_CloudSchool.DTOs;

namespace Code_CloudSchool.Interfaces;

public interface IUpdateAdminPassword
{
    Task<bool> UpdateAdminPassword(string AdminEmail, StudentPasswordDTO PasswordDTO);
}
