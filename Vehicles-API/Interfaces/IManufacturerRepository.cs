using Vehicles_API.Models;
using Vehicles_API.ViewModels.Manufacturer;

namespace Vehicles_API.Interfaces;

public interface IManufacturerRepository
{
    public Task AddManufacturerAsync(PostManufacturerViewModel model);
    public Task<List<ManufacturerViewModel>> ListManufacturersAsync();
    public Task<ManufacturerViewModel?> GetManufacturerByIdAsync(int id);
    public Task<List<ManufacturerWithVehiclesViewModel>> ListManufacturersVehicles();
    public Task<ManufacturerWithVehiclesViewModel?> ListManufacturersVehicles(int id);
    public Task UpdateManufacturerAsync(int id, PostManufacturerViewModel model);
    public Task DeleteManufacturerAsync(int id);
    public Task<bool> SaveAllAsync();
}