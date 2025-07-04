using FeedlotApp.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FeedlotApp.Sync;

public class SyncService
{
    private readonly HttpClient _httpClient = new();
    private const string ApiUrl = "https://your-api-url.com/api/animal";

    public async Task SyncAnimalsAsync()
    {
        var unsyncedAnimals = await ((App)App.Current).FLDatabase.GetUnsyncedAnimalsAsync();

        foreach (var animal in unsyncedAnimals)
        {
            try
            {
                string json = JsonSerializer.Serialize(animal);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(ApiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    animal.Synced = true;
                    await ((App)App.Current).FLDatabase.UpdateAnimalAsync(animal);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"[Sync Failed] Status: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Sync Error] {ex.Message}");
            }
        }
    }
}
