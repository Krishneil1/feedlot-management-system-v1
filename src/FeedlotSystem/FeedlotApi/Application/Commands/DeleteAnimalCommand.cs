// -------------------------------------------------------------------------------------------------
//
// DeleteAnimalCommand.cs -- The DeleteAnimalCommand.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Application.Commands;

using MediatR;

public class DeleteAnimalCommand : IRequest<Unit>
{
    public int Id { get; set; }

    public DeleteAnimalCommand(int id) => Id = id;
}
