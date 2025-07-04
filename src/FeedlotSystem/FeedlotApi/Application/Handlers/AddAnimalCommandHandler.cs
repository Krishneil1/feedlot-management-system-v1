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
using FeedlotApi.Persistence;
using MediatR;

public class AddAnimalCommandHandler : IRequestHandler<AddAnimalCommand, int>
{
    private readonly FeedlotDbContext _context;

    public AddAnimalCommandHandler(FeedlotDbContext context)
    {
        _context = context;
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

        _context.Animals.Add(animal);
        await _context.SaveChangesAsync(cancellationToken);

        return animal.Id;
    }
}
