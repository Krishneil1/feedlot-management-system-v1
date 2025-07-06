// -------------------------------------------------------------------------------------------------
//
// GetAnimalByIdHandler.cs -- The GetAnimalByIdHandler.cs class.
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

public class GetAnimalByIdHandler : IRequestHandler<GetAnimalByIdQuery, AnimalDto>
{
    private readonly IAnimalService _service;
    private readonly IMapper _mapper;

    public GetAnimalByIdHandler(IAnimalService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<AnimalDto> Handle(GetAnimalByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _service.GetAnimalByIdAsync(request.Id, cancellationToken);
        return _mapper.Map<AnimalDto>(entity);
    }
}
