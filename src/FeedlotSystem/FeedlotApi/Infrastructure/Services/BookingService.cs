// -------------------------------------------------------------------------------------------------
//
// BookingService.cs -- The BookingService.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Infrastructure.Services;

using AutoMapper;
using FeedlotApi.Data;
using FeedlotApi.Domain.DTOs;
using FeedlotApi.Domain.Entities;
using FeedlotApi.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

public class BookingService : IBookingService
{
    private readonly FeedlotDbContext _context;
    private readonly IMapper _mapper;

    public BookingService(FeedlotDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> CreateBookingAsync(BookingDto dto, CancellationToken cancellationToken)
    {
        var booking = _mapper.Map<Booking>(dto);

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync(cancellationToken);

        return booking.Id;
    }
    public async Task<List<Booking>> GetAllBookingsAsync(CancellationToken cancellationToken)
    {
        return await _context.Bookings
            .Include(b => b.Animals)
            .ToListAsync(cancellationToken);
    }
    public async Task<Booking> GetBookingByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Bookings
            .Include(b => b.Animals)
            .FirstAsync(b => b.Id == id, cancellationToken);
    }
    public async Task UpdateBookingAsync(int id, BookingDto dto, CancellationToken cancellationToken)
    {
        var existing = await _context.Bookings
            .Include(b => b.Animals)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        if (existing == null)
            throw new KeyNotFoundException($"Booking with ID {id} not found.");

        // Use AutoMapper to map the main booking fields
        _mapper.Map(dto, existing);

        // Replace animal list (AutoMapper can map each AnimalDto → Animal)
        _context.Animals.RemoveRange(existing.Animals); // optional: smarter merge logic
        existing.Animals = _mapper.Map<List<Animal>>(dto.Animals);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
