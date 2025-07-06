// -------------------------------------------------------------------------------------------------
//
// IAnimalService.cs -- The IAnimalService.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApi.Infrastructure.Interfaces;

using FeedlotApi.Domain.Entities;

public interface IAnimalService
{
    Task<int> AddAnimalAsync(Animal animal, CancellationToken cancellationToken);
    Task<List<Animal>> GetAllAnimalsAsync(CancellationToken cancellationToken);

}
