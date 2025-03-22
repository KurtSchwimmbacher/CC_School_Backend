using System;
using Code_CloudSchool.Models;

namespace Code_CloudSchool.Interface;

public interface IAuthAnnouncement
{
    Task<Announcements>CreateAnnouncementAsync(Announcements announcements);
    Task<Announcements>GetAnnouncementsAsync(Guid id); // Get announcement by id
}
