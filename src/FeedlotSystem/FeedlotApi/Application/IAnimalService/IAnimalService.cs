// -------------------------------------------------------------------------------------------------
//
// IAnimalService.cs -- The IAnimalService.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Application.IAnimalService;

using FeedlotApi.Domain.Entities;

public interface IAnimalService
{
    Task<int> AddAnimalAsync(Animal animal, CancellationToken cancellationToken);
}
