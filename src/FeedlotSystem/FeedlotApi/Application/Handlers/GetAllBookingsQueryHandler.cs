// -------------------------------------------------------------------------------------------------
//
// GetAllBookingsQueryHandler.cs -- The GetAllBookingsQueryHandler.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Application.Handlers;

using AutoMapper;
using FeedlotApi.Application.Queries;
using FeedlotApi.Domain.DTOs;
using FeedlotApi.Infrastructure.Interfaces;
using MediatR;

public class GetAllBookingsQueryHandler : IRequestHandler<GetAllBookingsQuery, List<BookingDto>>
{
    private readonly IBookingService _bookingService;
    private readonly IMapper _mapper;

    public GetAllBookingsQueryHandler(IBookingService bookingService, IMapper mapper)
    {
        _bookingService = bookingService;
        _mapper = mapper;
    }

    public async Task<List<BookingDto>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _bookingService.GetAllBookingsAsync(cancellationToken);
        return _mapper.Map<List<BookingDto>>(entities);
    }
}
