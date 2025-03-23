using Code_CloudSchool.Data2;
using Code_CloudSchool.Interfaces;
using Code_CloudSchool.LectInterface;
using Code_CloudSchool.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppContext") ?? throw new InvalidOperationException("Connection string 'AppContext' not found.")));

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
