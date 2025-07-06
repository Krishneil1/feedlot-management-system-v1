// -------------------------------------------------------------------------------------------------
//
// UpdateAnimalCommand.cs -- The UpdateAnimalCommand.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Application.Commands;

using FeedlotApi.Domain.DTOs;
using MediatR;

public class UpdateAnimalCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public AnimalDto Animal { get; set; } = null!;
}
