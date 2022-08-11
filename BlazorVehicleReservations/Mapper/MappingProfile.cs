﻿using AutoMapper;
using BlazorVehicleReservations.Shared;
using BlazorVehicleReservations.Shared.Models.Dto;

namespace BlazorVehicleReservations.API.Mapper
{

    //Ovo je samo primjer nekakvog mapiranja
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Vehicle, VehicleDto>()
                .ForMember(dest => dest.VehicleId, opt => opt.MapFrom(src => src.Id))
                    .ReverseMap();
            CreateMap<Client, ClientDto>()
                .ForMember(dest => dest.LName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.FName, opt => opt.MapFrom(src => src.FirstName))
                    .ReverseMap();
            CreateMap<Reservation, ReservationDto>()
                .ForMember(dest => dest.ReservationId, opt => opt.MapFrom(src => src.Id))
                    .ReverseMap();
        }
    }
}