using System;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Interface;

// Define an interface for authentication and announcement-related operations
public interface IAuthAnnouncement
{
    Task<Announcements> CreateAnnouncementAsync(Announcements announcements);
    Task<Announcements?> GetAnnouncementsAsync(int id);
    Task<IEnumerable<Announcements>> GetAnnouncementsByCourseAsync(int courseId);
}