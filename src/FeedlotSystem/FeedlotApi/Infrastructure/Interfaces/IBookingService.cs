// -------------------------------------------------------------------------------------------------
//
// IBookingService.cs -- The IBookingService.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Infrastructure.Interfaces;

using FeedlotApi.Domain.DTOs;
using FeedlotApi.Domain.Entities;

public interface IBookingService
{
    Task<int> CreateBookingAsync(BookingDto dto, CancellationToken cancellationToken);
    Task<List<Booking>> GetAllBookingsAsync(CancellationToken cancellationToken);
    Task<Booking> GetBookingByIdAsync(int id, CancellationToken cancellationToken);
    Task UpdateBookingAsync(int id, BookingDto dto, CancellationToken cancellationToken);

}
