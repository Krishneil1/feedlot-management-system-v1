// -------------------------------------------------------------------------------------------------
//
// AddAnimalCommandHandler.cs -- The AddAnimalCommandHandler.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Application.Handlers;

using AutoMapper;
using FeedlotApi.Application.Commands;
using FeedlotApi.Domain.Entities;
using FeedlotApi.Infrastructure.Interfaces;
using MediatR;

public class CreateAnimalCommandHandler : IRequestHandler<CreateAnimalCommand, int>
{
    private readonly IAnimalService _animalService;
    private readonly IMapper _mapper;

    public CreateAnimalCommandHandler(IAnimalService animalService, IMapper mapper)
    {
        _animalService = animalService;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateAnimalCommand request, CancellationToken cancellationToken)
    {
        var animal = _mapper.Map<Animal>(request.AnimalDto);
        return await _animalService.AddAnimalAsync(animal, cancellationToken);
    }
}
