// -------------------------------------------------------------------------------------------------
//
// MappingProfile.cs -- The MappingProfile.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Application.Mapping;

using AutoMapper;
using FeedlotApi.Domain.DTOs;
using FeedlotApi.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AnimalDto, Animal>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // avoid overwriting IDs on update

        CreateMap<BookingDto, Booking>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // don't map IDs
            .ForMember(dest => dest.Animals, opt => opt.Ignore()); // handle manually

    }
}
