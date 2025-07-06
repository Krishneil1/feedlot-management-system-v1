// -------------------------------------------------------------------------------------------------
//
// AddAnimalCommandHandler.cs -- The AddAnimalCommandHandler.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Application.Handlers;

using FeedlotApi.Application.Commands;
using FeedlotApi.Domain.Entities;
using MediatR;
using FeedlotApi.Application.IAnimalService;

public class AddAnimalCommandHandler : IRequestHandler<AddAnimalCommand, int>
{
    private readonly IAnimalService _animalService;

    public AddAnimalCommandHandler(IAnimalService animalService)
    {
        _animalService = animalService;
    }

    public async Task<int> Handle(AddAnimalCommand request, CancellationToken cancellationToken)
    {
        var animal = new Animal
        {
            TagId = request.TagId,
            Breed = request.Breed,
            DateOfBirth = request.DateOfBirth,
            Synced = true
        };

        return await _animalService.AddAnimalAsync(animal, cancellationToken);
    }
}
