using Vehicles_API.Models;
using Vehicles_API.ViewModels;

namespace Vehicles_API.Interfaces
{
    public interface IVehicleRepository
    {
        public Task<List<VehicleViewModel>> ListAllVehiclesAsync();
        public Task<VehicleViewModel?> GetVehicleAsync(int id);
        public Task<VehicleViewModel?> GetVehicleAsync(string regNumber);
        public Task<List<VehicleViewModel>> GetVehicleByMakeAsync(string make);
        public Task AddVehicleAsync(PostVehicleViewModel model);
        public Task DeleteVehicle(int id);
        public Task UpdateVehicle(int id, PostVehicleViewModel model);
        public Task<bool> SaveAllAsync();

    }
}