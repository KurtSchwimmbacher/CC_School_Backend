using Code_CloudSchool.Data;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Data2;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.LectInterface;
using Code_CloudSchool.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<ILAuthService, LAuthService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// connection to DB String here
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// add db context to services 
builder.Services.AddDbContext<AppContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<IStudentAuth, StudentAuthService>();
builder.Services.AddScoped<IStudentStatus, StudentStatusService>();
builder.Services.AddScoped<IStudentReEnroll, StudentReEnrollService>();
builder.Services.AddScoped<IUpdateStudentPassword, StudentPasswordService>();



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
