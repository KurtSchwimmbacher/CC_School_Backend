using Code_CloudSchool.Data;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// connection to DB String here
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// add db context to services 
builder.Services.AddDbContext<AppDBContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<IStudentAuth, StudentAuthService>();
builder.Services.AddScoped<IStudentStatus, StudentStatusService>();
builder.Services.AddScoped<IStudentReEnroll, StudentReEnrollService>();
builder.Services.AddScoped<IUpdateStudentPassword, StudentPasswordService>();
builder.Services.AddScoped<IAssignmentService, AssignmentService>();
builder.Services.AddScoped<ISubmissionService, SubmissionService>();
builder.Services.AddScoped<IGradeService, GradeService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// controller based api endpoints
app.MapControllers();
app.Run();


