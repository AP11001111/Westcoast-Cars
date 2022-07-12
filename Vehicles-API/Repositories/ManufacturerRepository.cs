using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Vehicles_API.Data;
using Vehicles_API.Interfaces;
using Vehicles_API.Models;
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

    public Task<List<ManufacturerViewModel>> ListManufacturersAsync()
    {
        return _context.Manufacturers
            .ProjectTo<ManufacturerViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}