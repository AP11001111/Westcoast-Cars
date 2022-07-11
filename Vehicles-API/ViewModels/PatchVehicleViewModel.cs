using System.ComponentModel.DataAnnotations;

namespace Vehicles_API.ViewModels;

public class PatchVehicleViewModel
{
    [Required]
    public int Mileage { get; set; }
    [Required]
    public int ModelYear { get; set; }    
}