// -------------------------------------------------------------------------------------------------
//
// Booking.cs -- The Booking.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Domain.Entities;

public class Booking
{
    public int Id { get; set; }
    public string BookingNumber { get; set; } = string.Empty;
    public DateTime BookingDate { get; set; }
    public string VendorName { get; set; } = string.Empty;
    public string Property { get; set; } = string.Empty;
    public string TruckReg { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public string? Notes { get; set; }
    public Guid PublicId { get; set; } = Guid.NewGuid(); 

    public List<Animal> Animals { get; set; } = new();
}
