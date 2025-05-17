using Code_CloudSchool.Data;
using Code_CloudSchool.Interface;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Models;
using Code_CloudSchool.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;



var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// Removed AddOpenApi as it is not a valid method

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve; //adds a check in ef to avoid any object loops
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Avien .env loader 
DotNetEnv.Env.Load();

// connection to DB String here
// var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
//local connection string --> 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// add health checks 
builder.Services.AddHealthChecks();

// add db context to services 
builder.Services.AddDbContext<AppDBContext>(options => options.UseNpgsql(connectionString));

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Removed app.MapOpenApi(); as it is not a valid method for WebApplication
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers(); //controller based on api endpoints -> so that we can use the controller to get the data from the db

// to expose a simple /health endpoint
app.MapHealthChecks("/health");

// Tell Kestrel to listen on port 80 (important for Docker)
app.Run("http://0.0.0.0:80");

//app.Run("http://localhost:80"); // for local testing
