using Vehicles_API.Models;
using Vehicles_API.ViewModels.Manufacturer;

namespace Vehicles_API.Interfaces;

public interface IManufacturerRepository
{
    public Task AddManufacturerAsync(PostManufacturerViewModel model);
    public Task<List<ManufacturerViewModel>> ListManufacturersAsync();
    public Task<bool> SaveAllAsync();
}