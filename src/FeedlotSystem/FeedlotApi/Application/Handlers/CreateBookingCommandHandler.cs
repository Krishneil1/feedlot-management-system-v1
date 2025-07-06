// -------------------------------------------------------------------------------------------------
//
// CreateBookingCommandHandler.cs -- The CreateBookingCommandHandler.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Application.Handlers;

using AutoMapper;
using FeedlotApi.Application.Commands;
using FeedlotApi.Domain.DTOs;
using FeedlotApi.Infrastructure.Interfaces;
using MediatR;

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, int>
{
    private readonly IBookingService _bookingService;
    private readonly IMapper _mapper;

    public CreateBookingCommandHandler(IBookingService bookingService, IMapper mapper)
    {
        _bookingService = bookingService;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var bookingDto = _mapper.Map<BookingDto>(request.Booking);

        // Optional: ensure synced flags
        bookingDto.Status = "Pending";
        if (bookingDto.Animals != null)
        {
            foreach (var animal in bookingDto.Animals)
            {
                // Optionally handle flags
            }
        }

        return await _bookingService.CreateBookingAsync(bookingDto, cancellationToken);
    }
}
