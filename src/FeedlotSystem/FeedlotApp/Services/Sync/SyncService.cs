
using System.Text;
using System.Text.Json;
using FeedlotApp.Interfaces;
using FeedlotApp.Models;

namespace FeedlotApp.Services.Sync;

public class SyncService : ISyncService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public SyncService()
    {
        _httpClient = new HttpClient();
        _baseUrl = App.Settings.ApiBaseUrl.TrimEnd('/');
    }

    public async Task SyncAnimalsAsync()
    {
        var db = App.FLDatabase;
        var unsyncedAnimals = await db.GetUnsyncedAnimalsAsync();

        if (!unsyncedAnimals.Any())
        {
            System.Diagnostics.Debug.WriteLine("[Sync] No unsynced animals to send.");
            return;
        }

        await SyncEntitiesAsync(unsyncedAnimals, $"{_baseUrl}/api/animal", async a => await db.UpdateAnimalAsync(a));
    }

    public async Task SyncBookingsAsync()
    {
        var db = App.FLDatabase;
        var unsyncedBookings = await db.GetUnsyncedBookingsAsync();

        if (!unsyncedBookings.Any())
        {
            System.Diagnostics.Debug.WriteLine("[Sync] No unsynced bookings to send.");
            return;
        }

        await SyncEntitiesAsync(unsyncedBookings, $"{_baseUrl}/api/booking", async b => await db.UpdateBookingAsync(b));
    }


    private async Task SyncEntitiesAsync<T>(
        IEnumerable<T> items,
        string endpoint,
        Func<T, Task> markSyncedAsync
    ) where T : ISyncable
    {
        foreach (var item in items)
        {
            try
            {
                var json = JsonSerializer.Serialize(item, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = false
                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(endpoint, content);
                Console.WriteLine("[POST BODY] " + json);

                if (response.IsSuccessStatusCode)
                {
                    item.Synced = true;
                    await markSyncedAsync(item);
                    System.Diagnostics.Debug.WriteLine($"[Sync OK] Synced {typeof(T).Name}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"[Sync Failed] {typeof(T).Name}, Status: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Sync Error] {typeof(T).Name}, Error: {ex.Message}");
            }
        }
    }
}
