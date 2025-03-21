// Program.cs
using Code_CloudSchool.Data;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design ⁠(); // Add support for controllers.
builder.Services.AddEndpointsApiExplorer(); // Add support for API exploration.
builder.Services.AddSwaggerGen(); // Add support for Swagger documentation.

// Register DbContext with the PostgreSQL connection string from appsettings.json.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)); // Use Npgsql for PostgreSQL.

// Register services for dependency injection.
builder.Services.AddScoped<IAssignmentService, AssignmentService>();
builder.Services.AddScoped<ISubmissionService, SubmissionService>();
builder.Services.AddScoped<IGradeService, GradeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable Swagger middleware.
    app.UseSwaggerUI(); // Enable Swagger UI middleware.
}

app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS.
app.UseAuthorization(); // Enable authorization middleware.
app.MapControllers(); // Map controller routes.

app.Run(); // Run the application.