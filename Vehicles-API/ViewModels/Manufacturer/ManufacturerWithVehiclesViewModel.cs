namespace Vehicles_API.ViewModels.Manufacturer;

public class ManufacturerWithVehiclesViewModel
{
    public int ManufacturerId { get; set; }
    public string? Name { get; set; }
    public List<VehicleViewModel> Vehicles { get; set; } = new List<VehicleViewModel>();

}