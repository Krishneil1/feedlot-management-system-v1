// -------------------------------------------------------------------------------------------------
//
// GetBookingByIdQuery.cs -- The GetBookingByIdQuery.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Application.Queries;

using FeedlotApi.Domain.DTOs;
using MediatR;

public class GetBookingByIdQuery : IRequest<BookingDto>
{
    public int Id { get; set; }
    public GetBookingByIdQuery(int id) => Id = id;
}
