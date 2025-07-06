// -------------------------------------------------------------------------------------------------
// FLDatabase.cs -- Local SQLite database for FeedlotApp
// -------------------------------------------------------------------------------------------------

namespace FeedlotApp.Data;

using FeedlotApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class FLDatabase
{
    private readonly SQLiteAsyncConnection _database;

    public FLDatabase(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _ = InitializeAsync(); // fire and forget safely
    }

    private async Task InitializeAsync()
    {
        try
        {
            await _database.CreateTableAsync<Animal>();
        }
        catch (Exception ex)
        {
            // TODO: Replace with ILogger or alert system
            System.Diagnostics.Debug.WriteLine($"[DB Init Error] {ex.Message}");
        }
    }

    public Task<List<Animal>> GetAllAnimalsAsync() =>
        _database.Table<Animal>().ToListAsync();

    public Task<List<Animal>> GetUnsyncedAnimalsAsync() =>
        _database.Table<Animal>().Where(a => !a.Synced).ToListAsync();

    public Task<int> SaveAnimalAsync(Animal animal) =>
        _database.InsertAsync(animal);

    public Task<int> UpdateAnimalAsync(Animal animal) =>
        _database.UpdateAsync(animal);

    public Task<int> DeleteAnimalAsync(Animal animal) =>
        _database.DeleteAsync(animal);
}
