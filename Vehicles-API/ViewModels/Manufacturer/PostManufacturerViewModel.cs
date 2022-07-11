using System.ComponentModel.DataAnnotations;

namespace Vehicles_API.ViewModels.Manufacturer;

public class PostManufacturerViewModel
{
    [Required]
    public string? Name { get; set; }
}