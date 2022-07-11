using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Vehicles_API.Data;
using Vehicles_API.Interfaces;
using Vehicles_API.Models;
using Vehicles_API.ViewModels;

namespace Vehicles_API.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly VehicleContext _context;
        private readonly IMapper _mapper;
    public VehicleRepository(VehicleContext context, IMapper mapper)
    {
            _mapper = mapper;
            _context = context;
    }

    public async Task AddVehicleAsync(PostVehicleViewModel model)
    {
        var vehicleToAdd = _mapper.Map<Vehicle>(model);
        await _context.Vehicles.AddAsync(vehicleToAdd);
    }

    public async Task DeleteVehicleAsync(int id)
    {
        var response = await _context.Vehicles.FindAsync(id);
        if (response is not null)
        {
            _context.Vehicles.Remove(response);
        }
    }

    public async Task<VehicleViewModel?> GetVehicleAsync(int id)
    {
        // return await _context.Vehicles.Where(v => v.Id == id)
        //     .Select(Vehicle => new VehicleViewModel{
        //         VehicleId = Vehicle.Id,
        //         RegNo = Vehicle.RegNo,
        //         VehicleName = string.Concat(Vehicle.Make, " ", Vehicle.Model),
        //         ModelYear = Vehicle.ModelYear,
        //         Mileage = Vehicle.Mileage
        //     }).SingleOrDefaultAsync();

        return await _context.Vehicles.Where(v => v.Id == id)
            .ProjectTo<VehicleViewModel>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    public async Task<VehicleViewModel?> GetVehicleAsync(string regNumber)
    {
        // return await _context.Vehicles.Where(v => v.RegNo!.ToLower() == regNumber.ToLower())
        //     .Select(Vehicle => new VehicleViewModel{
        //         VehicleId = Vehicle.Id,
        //         RegNo = Vehicle.RegNo,
        //         VehicleName = string.Concat(Vehicle.Make, " ", Vehicle.Model),
        //         ModelYear = Vehicle.ModelYear,
        //         Mileage = Vehicle.Mileage
        //     }).SingleOrDefaultAsync();

         return await _context.Vehicles
            .Where(v => v.RegNo!.ToLower() == regNumber.ToLower())
            .ProjectTo<VehicleViewModel>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    // public async Task<List<VehicleViewModel>> GetVehicleByMakeAsync(string make)
    // {
    //     return await _context.Vehicles
    //         .Where(v => v.Make!.ToLower() == make.ToLower())
    //         .ProjectTo<VehicleViewModel>(_mapper.ConfigurationProvider)
    //         .ToListAsync();
    // }

    public async Task<List<VehicleViewModel>> ListAllVehiclesAsync()
    {
        // var response = await _context.Vehicles.ToListAsync();
        // return _mapper.Map<List<VehicleViewModel>>(response);
        return await _context.Vehicles.ProjectTo<VehicleViewModel>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task UpdateVehicleAsync(int id, PostVehicleViewModel model)
    {
        var vehicle = await _context.Vehicles.FindAsync(id);
        if (vehicle is null)
        {
            throw new Exception($"Vi kunde inte hitta något fordon med id: {id}");
        }

        // _mapper.Map<PostVehicleViewModel, Vehicle>(model, vehicle);
        vehicle.RegNo = model.RegNo;
        // vehicle.Make = model.Make;
        vehicle.Model = model.Model;
        vehicle.ModelYear = model.ModelYear;
        vehicle.Mileage = model.Mileage;

        _context.Vehicles.Update(vehicle);
    }

    public async Task UpdateVehicleAsync(int id, PatchVehicleViewModel model)
    {
        var vehicle = await _context.Vehicles.FindAsync(id);
        if (vehicle is null)
        {
            throw new Exception($"Vi kunde inte hitta något fordon med id: {id}");
        }

        vehicle.ModelYear = model.ModelYear;
        vehicle.Mileage = model.Mileage;

        _context.Vehicles.Update(vehicle);
    }
}