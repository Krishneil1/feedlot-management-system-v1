// -------------------------------------------------------------------------------------------------
//
// GetBookingByPublicIdQueryHandler.cs -- The GetBookingByPublicIdQueryHandler.cs class.
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

public class GetBookingByPublicIdQueryHandler : IRequestHandler<GetBookingByPublicIdQuery, BookingDto?>
{
    private readonly IBookingService _service;
    private readonly IMapper _mapper;

    public GetBookingByPublicIdQueryHandler(IBookingService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<BookingDto?> Handle(GetBookingByPublicIdQuery request, CancellationToken cancellationToken)
    {
        var booking = await _service.GetBookingByPublicIdAsync(request.PublicId, cancellationToken);
        return booking == null ? null : _mapper.Map<BookingDto>(booking);
    }
}
