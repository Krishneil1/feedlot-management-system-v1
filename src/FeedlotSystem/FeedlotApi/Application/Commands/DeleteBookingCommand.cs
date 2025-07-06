// -------------------------------------------------------------------------------------------------
//
// DeleteBookingCommand.cs -- The DeleteBookingCommand.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Application.Commands;

using MediatR;

public class DeleteBookingCommand : IRequest<Unit>
{
    public int Id { get; set; }

    public DeleteBookingCommand(int id) => Id = id;
}
