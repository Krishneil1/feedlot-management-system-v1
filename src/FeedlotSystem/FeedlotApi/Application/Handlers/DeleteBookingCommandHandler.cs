// -------------------------------------------------------------------------------------------------
//
// DeleteBookingCommandHandler.cs -- The DeleteBookingCommandHandler.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Application.Handlers;

using FeedlotApi.Application.Commands;
using FeedlotApi.Infrastructure.Interfaces;
using MediatR;

public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, Unit>
{
    private readonly IBookingService _service;

    public DeleteBookingCommandHandler(IBookingService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
    {
        await _service.DeleteBookingAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}
