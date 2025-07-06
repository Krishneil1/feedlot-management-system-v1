// -------------------------------------------------------------------------------------------------
//
// Animal.cs -- The Animal.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApp.Models;
using System;
using SQLite;

public class Animal : ISyncable
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string TagId { get; set; } = string.Empty;
    public string Breed { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }

    public bool Synced { get; set; } = false;
}


