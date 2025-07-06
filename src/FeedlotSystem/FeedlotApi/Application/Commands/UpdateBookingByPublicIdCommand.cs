// -------------------------------------------------------------------------------------------------
//
// UpdateBookingByPublicIdCommand.cs -- The UpdateBookingByPublicIdCommand.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Application.Commands;

using FeedlotApi.Domain.DTOs;
using MediatR;

public class UpdateBookingByPublicIdCommand : IRequest<Unit> 
{
    public Guid PublicId { get; set; }
    public BookingDto Booking { get; set; } = new();
}
