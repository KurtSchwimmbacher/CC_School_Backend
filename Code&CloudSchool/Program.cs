using Code_CloudSchool.Data;
using Code_CloudSchool.Interface;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;



var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
//var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
//local connection string --> var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDBContext>(options => options.UseNpgsql(connectionString));


// add db context to services 
builder.Services.AddDbContext<AppDBContext>(options => options.UseNpgsql(connectionString));

Console.WriteLine("Connected to DB: " + builder.Configuration.GetConnectionString("DefaultConnection"));
// Add services to the container.
builder.Services.AddScoped<IStudentAuth, StudentAuthService>();
builder.Services.AddScoped<IStudentStatus, StudentStatusService>();
builder.Services.AddScoped<IStudentReEnroll, StudentReEnrollService>();
builder.Services.AddScoped<IUpdateStudentPassword, StudentPasswordService>();
builder.Services.AddScoped<ILecturerAuth, LecturerAuthService>();

builder.Services.AddScoped<IAssignmentService, AssignmentService>();
builder.Services.AddScoped<ISubmissionService, SubmissionService>();
builder.Services.AddScoped<IGradeService, GradeService>();

builder.Services.AddScoped<IAuthAnnouncement, AnnounceServices>();
builder.Services.AddScoped<ILAuthService, LAuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers(); //controller based on api endpoints -> so that we can use the controller to get the data from the db

app.Run();
