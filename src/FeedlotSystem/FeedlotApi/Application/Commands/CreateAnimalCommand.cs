// -------------------------------------------------------------------------------------------------
//
// AddAnimalCommand.cs -- The AddAnimalCommand.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Application.Commands;

using FeedlotApi.Domain.DTOs;
using MediatR;

public class CreateAnimalCommand : IRequest<int>
{
    public AnimalDto AnimalDto { get; set; } = null!; 
}
