using FeedlotApp.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace FeedlotApp.Sync;

public class SyncService
{
    private readonly HttpClient _httpClient;
    private const string ApiUrl = "http://192.168.68.100:5282/api/animal";

    public SyncService()
    {
        _httpClient = new HttpClient();
    }

    public async Task SyncAnimalsAsync()
    {
        var db = App.FLDatabase;
        var unsyncedAnimals = await db.GetUnsyncedAnimalsAsync();

        foreach (var animal in unsyncedAnimals)
        {
            try
            {
                string json = JsonSerializer.Serialize(animal, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = false
                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(ApiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    animal.Synced = true;
                    await db.UpdateAnimalAsync(animal); // ✅ Ensure this method exists
                    System.Diagnostics.Debug.WriteLine($"[Sync OK] Animal {animal.TagId} synced.");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"[Sync Failed] Animal {animal.TagId}, Status: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Sync Error] Animal {animal.TagId}, Error: {ex.Message}");
            }
        }
    }
}
