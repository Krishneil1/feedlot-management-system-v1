// -------------------------------------------------------------------------------------------------
//
// AnimalDto.cs -- The AnimalDto.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Domain.DTOs;

public class AnimalDto
{
    public string TagId { get; set; } = string.Empty;
    public string Breed { get; set; } = string.Empty;
    public int BookingId { get; set; }
    public DateTime DateOfBirth { get; set; }
}

