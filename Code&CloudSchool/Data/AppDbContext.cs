using System;
using Microsoft.EntityFrameworkCore;

namespace Code_CloudSchool.Data;

// This class is used to connect to the database
public class AppDbContext: DbContext
{

    // Constructor - use all the base options for db context
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
    }

    // list the tables / relationships

    
    
}
