// -------------------------------------------------------------------------------------------------
//
// GetAllAnimalsQuery.cs -- The GetAllAnimalsQuery.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Application.Queries;

using FeedlotApi.Domain.Entities;
using MediatR;

public class GetAllAnimalsQuery : IRequest<List<Animal>> { }
