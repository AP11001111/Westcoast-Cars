using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Vehicles_API.Data;
using Vehicles_API.Interfaces;
using Vehicles_API.Models;
using Vehicles_API.ViewModels;
using Vehicles_API.ViewModels.Manufacturer;

namespace Vehicles_API.Repositories;

public class ManufacturerRepository : IManufacturerRepository
{
        private readonly VehicleContext _context;
        private readonly IMapper _mapper;
    public ManufacturerRepository( VehicleContext context, IMapper mapper)
    {
            _mapper = mapper;
            _context = context;
    }

    public async Task AddManufacturerAsync(PostManufacturerViewModel model)
    {
        var make = _mapper.Map<Manufacturer>(model);
        await _context.Manufacturers.AddAsync(make);
    }

    public async Task DeleteManufacturerAsync(int id)
    {
        var manufacturer = await _context.Manufacturers.FindAsync(id);
        if (manufacturer is not null)
        {
            _context.Manufacturers.Remove(manufacturer);
        }
        else
        {
            
            throw new Exception($"Manufacturer med id: {id} finns inte i systemet");
        }
    }

    public async Task<ManufacturerViewModel?> GetManufacturerByIdAsync(int id)
    {
        return await _context.Manufacturers.Where(m => m.Id == id)
            .ProjectTo<ManufacturerViewModel>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    public Task<List<ManufacturerViewModel>> ListManufacturersAsync()
    {
        return _context.Manufacturers
            .ProjectTo<ManufacturerViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<List<ManufacturerWithVehiclesViewModel>> ListManufacturersVehicles()
    {
        return await _context.Manufacturers.Include(c => c.Vehicles)
        // Steg 1. Projicera tillverkarens data till viewmodel
        .Select(m => new ManufacturerWithVehiclesViewModel{
            ManufacturerId = m.Id,
            Name = m.Name,
            Vehicles = m.Vehicles.Select(v => new VehicleViewModel{
            // Steg 2. Projicera andra änden av vår join(vehicle data) till en VehicleViewModel
                VehicleId = v.Id,
                RegNo = v.RegNo,
                VehicleName = string.Concat(v.Manufacturer.Name, " ", v.Model),
                ModelYear = v.ModelYear,
                Mileage = v.Mileage
            }).ToList()
        })
        .ToListAsync();
    }

    public async Task<ManufacturerWithVehiclesViewModel?> ListManufacturersVehicles(int id)
    {
        return await _context.Manufacturers
        .Where(c => c.Id == id)
        .Include(c => c.Vehicles)
        // Steg 1. Projicera tillverkarens data till viewmodel
        .Select(m => new ManufacturerWithVehiclesViewModel{
            ManufacturerId = m.Id,
            Name = m.Name,
            Vehicles = m.Vehicles.Select(v => new VehicleViewModel{
            // Steg 2. Projicera andra änden av vår join(vehicle data) till en VehicleViewModel
                VehicleId = v.Id,
                RegNo = v.RegNo,
                VehicleName = string.Concat(v.Manufacturer.Name, " ", v.Model),
                ModelYear = v.ModelYear,
                Mileage = v.Mileage
            }).ToList()
        })
        .SingleOrDefaultAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task UpdateManufacturerAsync(int id, PostManufacturerViewModel model)
    {
        var manufacturer = await _context.Manufacturers.FindAsync(id);
        if (manufacturer is null)
        {
            throw new Exception($"Manufacturer med id: {id} finns inte i systemet");
        }
        manufacturer.Name = model.Name;
        _context.Update(manufacturer);
    }
}