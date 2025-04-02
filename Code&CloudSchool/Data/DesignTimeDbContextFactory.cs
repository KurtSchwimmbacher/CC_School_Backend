using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;

namespace Code_CloudSchool.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDBContext>
{
    // This method is required by the interface - EF Core calls it during migrations
    public AppDBContext CreateDbContext(string[] args)
    {
        // Build a configuration object that can read settings files
        IConfigurationRoot configuration = new ConfigurationBuilder()
            // Look in the current project folder
            .SetBasePath(Directory.GetCurrentDirectory())
            // Load settings from appsettings.json
            .AddJsonFile("appsettings.json")
            // Finalise the configuration
            .Build();

        // Prepare options for creating the database context
        var builder = new DbContextOptionsBuilder<AppDBContext>();
        
        // Get the connection string from appsettings.json
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        // Tell it to use PostgreSQL with our connection string
        builder.UseNpgsql(connectionString);

        // Create and return the actual database context
        return new AppDBContext(builder.Options);
    }
}
