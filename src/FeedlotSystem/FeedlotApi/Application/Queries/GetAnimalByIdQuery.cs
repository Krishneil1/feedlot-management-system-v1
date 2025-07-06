// -------------------------------------------------------------------------------------------------
//
// GetAnimalByIdQuery.cs -- The GetAnimalByIdQuery.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Application.Queries;

using FeedlotApi.Domain.DTOs;
using MediatR;

public class GetAnimalByIdQuery : IRequest<AnimalDto>
{
    public int Id { get; set; }
    public GetAnimalByIdQuery(int id) => Id = id;
}
