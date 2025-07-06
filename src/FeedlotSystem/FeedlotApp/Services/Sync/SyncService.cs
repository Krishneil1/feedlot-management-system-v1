// -------------------------------------------------------------------------------------------------
//
// SyncService.cs -- Handles syncing Animal and Booking data to API without posting Ids.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

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

        foreach (var booking in unsyncedBookings)
        {
            try
            {
                // Wrap the booking in a "booking" property
                var payload = new
                {
                    booking = new
                    {
                        booking.BookingNumber,
                        booking.BookingDate,
                        booking.VendorName,
                        booking.Property,
                        booking.TruckReg,
                        booking.Status,
                        booking.Notes,
                        animals = new List<object>() // no animals linked locally
                    }
                };

                string json = JsonSerializer.Serialize(payload, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = false
                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_baseUrl}/api/booking", content);

                if (response.IsSuccessStatusCode)
                {
                    booking.Synced = true;
                    await db.UpdateBookingAsync(booking);
                    System.Diagnostics.Debug.WriteLine($"[Sync OK] Booking {booking.BookingNumber}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"[Sync Failed] Booking {booking.BookingNumber}, Status: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Sync Error] Booking {booking.BookingNumber}, Error: {ex.Message}");
            }
        }
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

