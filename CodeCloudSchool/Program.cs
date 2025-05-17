using Code_CloudSchool.Data;
using Code_CloudSchool.Interface;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Code_CloudSchool.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

// Load .env (for local dev)
DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Setup DB connection string
var connectionString = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString("DefaultConnection")
    : Environment.GetEnvironmentVariable("DB_CONNECTION");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("DB_CONNECTION environment variable is not set.");
}

// Register services
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IStudentAuth, StudentAuthService>();
builder.Services.AddScoped<IStudentStatus, StudentStatusService>();
builder.Services.AddScoped<IStudentReEnroll, StudentReEnrollService>();
builder.Services.AddScoped<IUpdateStudentPassword, StudentPasswordService>();
builder.Services.AddScoped<ILecturerAuth, LecturerAuthService>();
builder.Services.AddScoped<IAssignmentService, AssignmentService>();
builder.Services.AddScoped<ISubmissionService, SubmissionService>();
builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddScoped<IAdminAuth, AdminAuthService>();
builder.Services.AddScoped<IAuthAnnouncement, AnnounceServices>();
builder.Services.AddScoped<ILAuthService, LAuthService>();
builder.Services.AddScoped<IUpdateAdminPassword, AdminPasswordService>();
builder.Services.AddScoped<IMajorServices, MajorServices>();
builder.Services.AddScoped<ICourseServices, CoursesServices>();
builder.Services.AddScoped<IClassesServices, ClassesServices>();
builder.Services.AddScoped<ITimeSlotGen, TimeSlotGen>();
builder.Services.AddScoped<ITimetableGenerator, TimetableGeneratorService>();

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapHealthChecks("/health");

// Important for Docker/Render: Listen on port 80
app.Run("http://0.0.0.0:80");
