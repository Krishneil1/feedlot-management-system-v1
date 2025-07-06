// -------------------------------------------------------------------------------------------------
//
// ISyncService.cs -- The ISyncService.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApp.Interfaces;

public interface ISyncService
{
    Task SyncAnimalsAsync();
    Task SyncBookingsAsync();
}
