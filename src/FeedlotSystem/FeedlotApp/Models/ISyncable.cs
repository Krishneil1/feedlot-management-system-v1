// -------------------------------------------------------------------------------------------------
//
// ISyncable.cs -- The ISyncable.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApp.Models;

public interface ISyncable
{
    bool Synced { get; set; }
}
