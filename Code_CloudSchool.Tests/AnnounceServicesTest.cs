using System;
using Code_CloudSchool.Data;
using Code_CloudSchool.Models;
using Code_CloudSchool.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Code_CloudSchool.Tests;

public class AnnounceServicesTest
{

    private readonly AnnounceServices _service;

    public AnnounceServicesTest()
    {
        // use in memory db for testing
        var options = new DbContextOptionsBuilder<AppDBContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new AppDBContext(options);

        // seed in memory db
        context.Announcements.Add(new Announcements { AnnouncementId = 1, Title = "Test", Description = "Testing functions" });
        context.SaveChanges();

        // pass in memory context to the service
        _service = new AnnounceServices(context);
    }



    // Test Case 1: announcement created successfully 
    [Fact]
    public async Task CreateAnnouncementAsync_Created_ReturnsAnnouncement()
    {
        // Arrange
        var newAnnouncement = new Announcements
        {
            AnnouncementId = 3,
            Title = "Test Announcement",
            Description = "Test Content"
        };

        // Act
        var createdAnnouncement = await _service.CreateAnnouncementAsync(newAnnouncement);

        // Assert
        // not null
        Assert.NotNull(createdAnnouncement);
        // title matches
        Assert.Equal(newAnnouncement.Title, createdAnnouncement.Title);
        // description matches
        Assert.Equal(newAnnouncement.Description, createdAnnouncement.Description);
        // ID matches
        Assert.Equal(newAnnouncement.AnnouncementId, createdAnnouncement.AnnouncementId);

    }



    // Test Case 2: throws exception if save changes fails
    [Fact]
    public async Task CreateAnnouncementAsync_SaveChangesFails_ThrowsException()
    {
        // Arrange 
        var options = new DbContextOptionsBuilder<AppDBContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        var mockContext = new Mock<AppDBContext>(options);
        var mockAnnouncementsSet = new Mock<DbSet<Announcements>>();

        // Set up the Announcements DbSet in the mocked context
        mockContext.Setup(c => c.Announcements).Returns(mockAnnouncementsSet.Object);

        // Simulate SaveChangesAsync throwing an exception
        mockContext
            .Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("SaveChange failed"));

        var service = new AnnounceServices(mockContext.Object);

        var newAnnouncement = new Announcements
        {
            AnnouncementId = 4,
            Title = "Test",
            Description = "test content"
        };

        // Act
        var exception = await Assert.ThrowsAsync<Exception>(() => service.CreateAnnouncementAsync(newAnnouncement));

        // Assert
        Assert.Equal("SaveChange failed", exception.Message);

    }



    // Test Case 3: Returns an announcement correctly

    // FUNCTIONALITY NOT YET IMPLEMENTED

    // [Fact]
    // public async Task GetAnnouncementsAsync_ReturnsAnnouncement()
    // {
    //     // Arrange
    //     var announcementId = 1; 
    //     var expectedTitle = "Test";
    //     var expectedDescription = "Testing functions";

    //     // Act
    //     var announcement = await _service.GetAnnouncementsAsync(new Announcements { AnnouncementId = announcementId });

    //     // Assert
    //     // Ensure the announcement is not null
    //     Assert.NotNull(announcement); 
    //     // Verify the title matches
    //     Assert.Equal(expectedTitle, announcement.Title); 
    //     // Verify the description matches
    //     Assert.Equal(expectedDescription, announcement.Description); 
    //     // Verify the ID matches
    //     Assert.Equal(announcementId, announcement.AnnouncementId); 
    // }

    // Test Case 4: returns null if no announcements found
}
