using Microsoft.EntityFrameworkCore;
using Vehicles_API.Data;
using Vehicles_API.Helpers;
using Vehicles_API.Interfaces;
using Vehicles_API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Skapa databas koppling
builder.Services.AddDbContext<VehicleContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"))
);

// Dependency injection för våra egna Interface och klasser...
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();

// Add automapper...
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
