using Microsoft.EntityFrameworkCore;
using Vehicles_API.Models;

namespace Vehicles_API.Data;

public class VehicleContext : DbContext // 1. Ärv ifrån EFCore
{
    public DbSet<Vehicle> Vehicles => Set<Vehicle>(); // 2. Mappa minnesrepresentationen av vårt fordon till databas

    public DbSet<Manufacturer> Manufacturers => Set<Manufacturer>();
    public VehicleContext(DbContextOptions options) : base(options) 
    {
    } // 3. Skapa konstruktör för att ta hand om anslutnings konfigurationen

}