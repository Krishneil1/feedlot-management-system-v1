// -------------------------------------------------------------------------------------------------
//
// AnimalService.cs -- The AnimalService.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Infrastructure.Services;

using FeedlotApi.Data;
using FeedlotApi.Domain.Entities;
using FeedlotApi.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

public class AnimalService : IAnimalService
{
    private readonly FeedlotDbContext _context;

    public AnimalService(FeedlotDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddAnimalAsync(Animal animal, CancellationToken cancellationToken)
    {
        _context.Animals.Add(animal);
        await _context.SaveChangesAsync(cancellationToken);
        return animal.Id;
    }
    public async Task<List<Animal>> GetAllAnimalsAsync(CancellationToken cancellationToken)
    {
        return await _context.Animals.ToListAsync(cancellationToken);
    }
}
