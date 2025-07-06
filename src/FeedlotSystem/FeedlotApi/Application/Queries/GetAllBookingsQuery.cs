// -------------------------------------------------------------------------------------------------
//
// GetAllBookingsQuery.cs -- The GetAllBookingsQuery.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Application.Queries;

using FeedlotApi.Domain.DTOs;
using MediatR;

public class GetAllBookingsQuery : IRequest<List<BookingDto>> { }
