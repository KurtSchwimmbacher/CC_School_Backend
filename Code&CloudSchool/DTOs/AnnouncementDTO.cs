using System;

namespace Code_CloudSchool.DTOs
{
    public class AnnouncementDTO
    {
        public int AnnouncementId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int LecturerId { get; set; }
        public string LecturerName { get; set; } = string.Empty; // For display
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty; // For display
    }
}