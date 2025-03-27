using System;

namespace Code_CloudSchool.DTOs
{
    public class LecturerRegDTO
    {
        public int Id { get; set; }
        public string LectName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public DateTime DateOfJoining { get; set; }
        public bool IsActive { get; set; } = true;
    }
}