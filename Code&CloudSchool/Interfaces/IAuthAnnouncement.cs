using System;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Interface;

// Define an interface for authentication and announcement-related operations
public interface IAuthAnnouncement
{
    // Asynchronously creates a new announcement and returns the created announcement object
    Task<Announcements> CreateAnnouncementAsync(Announcements announcements);

    // Asynchronously retrieves an announcement by its unique identifier (ID)
    Task<Announcements> GetAnnouncementsAsync(Announcements announcements);
}