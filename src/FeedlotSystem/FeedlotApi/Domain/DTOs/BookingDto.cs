// -------------------------------------------------------------------------------------------------
//
// BookingDto.cs -- The BookingDto.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Domain.DTOs;

public class BookingDto
{
    public string BookingNumber { get; set; } = string.Empty;
    public DateTime BookingDate { get; set; }
    public string VendorName { get; set; } = string.Empty;
    public string Property { get; set; } = string.Empty;
    public string TruckReg { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public string? Notes { get; set; }

    public Guid PublicId { get; set; }
    public List<AnimalDto> Animals { get; set; } = new();
}
