using AutoMapper;
using BetaLogistics.Models;
using BetaLogistics.Models.Dto;

namespace QuickLogistics
{
    public class MappingConfig 
    {
        public static MapperConfiguration RegisterMaps()
        {
             var mappingConfig = new MapperConfiguration(config =>
             {
                 config.CreateMap<Vehicle, VehicleDto>().ReverseMap();
                 config.CreateMap<Vehicle, CreateVehicleDto>().ReverseMap();
                 config.CreateMap<Vehicle, UpdateVehicleDto>().ReverseMap();
                 

                 config.CreateMap<Driver, DriverDto>().ReverseMap();
                 config.CreateMap<Driver, CreateDriverDto>().ReverseMap();
                 config.CreateMap<Driver, UpdateDriverDto>().ReverseMap();

                 config.CreateMap<Customer, CustomerDto>().ReverseMap();
                 config.CreateMap<Customer, CreateCustomerDto>().ReverseMap();
                 config.CreateMap<Customer, UpdateCustomerDto>().ReverseMap();

                 config.CreateMap<Shipment, ShipmentDto>().ReverseMap();
                 config.CreateMap<Shipment, CreateShipmentDto>().ReverseMap();
                 config.CreateMap<UpdateShipmentDto, Shipment>().ReverseMap();

                 config.CreateMap<ApplicationUser, UserDto>().ReverseMap();
             });
            return mappingConfig;
        }
    }
}
