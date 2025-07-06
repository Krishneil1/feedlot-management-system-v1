// -------------------------------------------------------------------------------------------------
//
// CreateBookingCommand.cs -- The CreateBookingCommand.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Application.Commands;

using FeedlotApi.Domain.DTOs;
using MediatR;

public class CreateBookingCommand : IRequest<int>
{
    public BookingDto Booking { get; set; } = null!;
}

