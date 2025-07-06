// -------------------------------------------------------------------------------------------------
//
// Booking.cs -- The Booking.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApp.Models;

using SQLite;

public class Booking : ISyncable
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string BookingNumber { get; set; } = string.Empty;

    public DateTime BookingDate { get; set; }

    public string VendorName { get; set; } = string.Empty;

    public string Property { get; set; } = string.Empty;

    public string TruckReg { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public string? Notes { get; set; }

    public bool Synced { get; set; } = false;
}
