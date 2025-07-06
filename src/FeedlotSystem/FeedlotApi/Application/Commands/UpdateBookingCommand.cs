// -------------------------------------------------------------------------------------------------
//
// UpdateBookingCommand.cs -- The UpdateBookingCommand.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Application.Commands;

using FeedlotApi.Domain.DTOs;
using MediatR;

public class UpdateBookingCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public BookingDto Booking { get; set; } = null!;
}
