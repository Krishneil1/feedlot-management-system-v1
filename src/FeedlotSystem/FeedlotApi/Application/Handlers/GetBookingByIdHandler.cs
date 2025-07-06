// -------------------------------------------------------------------------------------------------
//
// GetBookingByIdHandler.cs -- The GetBookingByIdHandler.cs class.
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

public class GetBookingByIdHandler : IRequestHandler<GetBookingByIdQuery, BookingDto>
{
    private readonly IBookingService _service;
    private readonly IMapper _mapper;

    public GetBookingByIdHandler(IBookingService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<BookingDto> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _service.GetBookingByIdAsync(request.Id, cancellationToken);
        return _mapper.Map<BookingDto>(entity);
    }
}
