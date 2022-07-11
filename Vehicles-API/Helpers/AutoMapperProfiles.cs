using AutoMapper;
using Vehicles_API.ViewModels;
using Vehicles_API.Models;
using Vehicles_API.ViewModels.Manufacturer;

namespace Vehicles_API.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        // Map från -> till
        CreateMap<PostVehicleViewModel, Vehicle>();
        CreateMap<Vehicle, VehicleViewModel>()
            .ForMember( dest => dest.VehicleId, options => options.
            MapFrom(src => src.Id));
            // .ForMember( dest => dest.VehicleName, options => options.
            // MapFrom(src => string.Concat(src.Make, " ", src.Model)));

        CreateMap<PostManufacturerViewModel, Manufacturer>();
        CreateMap<Manufacturer, ManufacturerViewModel>();
    }
}