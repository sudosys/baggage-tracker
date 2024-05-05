using AutoMapper;
using BaggageTrackerApi.Entities;
using BaggageTrackerApi.Entities.DTOs;

namespace BaggageTrackerApi;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<User, UserSlimDto>();
        CreateMap<Flight, FlightDto>();
        CreateMap<Baggage, BaggageDto>();
    }
}