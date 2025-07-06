// -------------------------------------------------------------------------------------------------
//
// Booking.cs -- The Booking.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApp.Models;

using SQLite;

public class Booking
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Reference { get; set; }

    public DateTime Date { get; set; }
}
