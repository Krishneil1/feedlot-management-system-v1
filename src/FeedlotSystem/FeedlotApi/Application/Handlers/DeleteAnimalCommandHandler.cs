// -------------------------------------------------------------------------------------------------
//
// DeleteAnimalCommandHandler.cs -- The DeleteAnimalCommandHandler.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Application.Handlers;

using FeedlotApi.Application.Commands;
using FeedlotApi.Infrastructure.Interfaces;
using MediatR;

public class DeleteAnimalCommandHandler : IRequestHandler<DeleteAnimalCommand, Unit>
{
    private readonly IAnimalService _service;

    public DeleteAnimalCommandHandler(IAnimalService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(DeleteAnimalCommand request, CancellationToken cancellationToken)
    {
        await _service.DeleteAnimalAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}
