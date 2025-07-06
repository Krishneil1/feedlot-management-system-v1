// -------------------------------------------------------------------------------------------------
//
// AnimalService.cs -- The AnimalService.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Infrastructure.Services;

using AutoMapper;
using FeedlotApi.Data;
using FeedlotApi.Domain.DTOs;
using FeedlotApi.Domain.Entities;
using FeedlotApi.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

public class AnimalService : IAnimalService
{
    private readonly FeedlotDbContext _context;
    private readonly IMapper _mapper;
    public AnimalService(FeedlotDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
    public async Task<Animal> GetAnimalByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Animals.FindAsync(new object[] { id }, cancellationToken);
    }
    public async Task UpdateAnimalAsync(int id, AnimalDto dto, CancellationToken cancellationToken)
    {
        var existing = await _context.Animals.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (existing == null)
            throw new KeyNotFoundException($"Animal with ID {id} not found.");

        _mapper.Map(dto, existing);

        await _context.SaveChangesAsync(cancellationToken);
    }

}
