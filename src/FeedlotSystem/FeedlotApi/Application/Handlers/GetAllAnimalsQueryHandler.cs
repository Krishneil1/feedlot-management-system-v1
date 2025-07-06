// -------------------------------------------------------------------------------------------------
//
// GetAllAnimalsQueryHandler.cs -- The GetAllAnimalsQueryHandler.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Application.Handlers;

using FeedlotApi.Application.Queries;
using FeedlotApi.Domain.Entities;
using FeedlotApi.Infrastructure.Interfaces;
using MediatR;

public class GetAllAnimalsQueryHandler : IRequestHandler<GetAllAnimalsQuery, List<Animal>>
{
    private readonly IAnimalService _animalService;

    public GetAllAnimalsQueryHandler(IAnimalService animalService)
    {
        _animalService = animalService;
    }

    public async Task<List<Animal>> Handle(GetAllAnimalsQuery request, CancellationToken cancellationToken)
    {
        return await _animalService.GetAllAnimalsAsync(cancellationToken);
    }
}
