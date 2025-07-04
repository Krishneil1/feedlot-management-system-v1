// -------------------------------------------------------------------------------------------------
//
// AddAnimalCommand.cs -- The AddAnimalCommand.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Application.Commands;

using MediatR;

public class AddAnimalCommand : IRequest<int>
{
    public string TagId { get; set; } = string.Empty;
    public string Breed { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
}
