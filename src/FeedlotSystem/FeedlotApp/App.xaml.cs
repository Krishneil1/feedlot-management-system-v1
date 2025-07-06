// -------------------------------------------------------------------------------------------------
//
// App.cs -- The App.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

using System.Text.Json;
using FeedlotApp.Data;
using FeedlotApp.Models;
using FeedlotApp.Services.Sync;

namespace FeedlotApp;

public partial class App : Application
{
    public static FLDatabase FLDatabase { get; private set; }
    public static AppSettings Settings { get; private set; }

    public App()
    {
        InitializeComponent();

        string dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "animal.db3"
        );
        FLDatabase = new FLDatabase(dbPath);

        LoadAppSettings(); // ⬅️ Load config from JSON

        MainPage = new AppShell();


        // 🔄 Sync on connectivity change
        Connectivity.ConnectivityChanged += async (s, e) =>
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                try
                {
                    var syncService = new SyncService();
                    await syncService.SyncAnimalsAsync();
                    await syncService.SyncBookingsAsync();
                }
                catch (Exception ex)
                {
                    // Optional: log or alert the user
                    Console.WriteLine($"[Sync Error] {ex.Message}");
                }
            }
        };
    }
    private void LoadAppSettings()
    {
        try
        {
            var file = FileSystem.OpenAppPackageFileAsync("appsettings.json").Result;
            using var reader = new StreamReader(file);
            string json = reader.ReadToEnd();
            Settings = JsonSerializer.Deserialize<AppSettings>(json)
                       ?? new AppSettings();
        }
        catch
        {
            Settings = new AppSettings(); // fallback default
        }
    }
}
