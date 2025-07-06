// -------------------------------------------------------------------------------------------------
//
// MappingProfile.cs -- The MappingProfile.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Application.Mapping;

using AutoMapper;
using FeedlotApi.Application.Commands;
using FeedlotApi.Domain.DTOs;
using FeedlotApi.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // DTO ↔ Entity
        CreateMap<AnimalDto, Animal>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Booking, opt => opt.Ignore());
        CreateMap<Animal, AnimalDto>();

        CreateMap<BookingDto, Booking>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Animals, opt => opt.Ignore())
            .ForMember(dest => dest.PublicId, opt => opt.MapFrom(src => src.PublicId));
        CreateMap<Booking, BookingDto>();

        // ✅ Command → DTO (used in CreateBookingCommandHandler)
        CreateMap<CreateBookingCommand, BookingDto>()
            .ForMember(dest => dest.Animals, opt => opt.MapFrom(src => src.Booking.Animals));

        CreateMap<AnimalDto, Animal>(); // in case AnimalDto is used deeper
    }
}
