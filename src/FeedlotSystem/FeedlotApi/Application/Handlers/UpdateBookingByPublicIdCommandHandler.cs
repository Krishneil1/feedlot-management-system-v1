// -------------------------------------------------------------------------------------------------
//
// UpdateBookingByPublicIdCommandHandler.cs -- The UpdateBookingByPublicIdCommandHandler.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Application.Handlers;

using FeedlotApi.Application.Commands;
using FeedlotApi.Infrastructure.Interfaces;
using MediatR;

public class UpdateBookingByPublicIdCommandHandler : IRequestHandler<UpdateBookingByPublicIdCommand, Unit>
{
    private readonly IBookingService _service;

    public UpdateBookingByPublicIdCommandHandler(IBookingService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(UpdateBookingByPublicIdCommand request, CancellationToken cancellationToken)
    {
        await _service.UpdateBookingByPublicIdAsync(request.PublicId, request.Booking, cancellationToken);
        return Unit.Value;
    }
}
