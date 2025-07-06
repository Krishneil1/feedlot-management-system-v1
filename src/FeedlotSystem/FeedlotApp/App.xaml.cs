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
using FeedlotApp.Services;
using FeedlotApp.Services.Sync;

namespace FeedlotApp;

public partial class App : Application
{
    public static FLDatabase? FLDatabase { get; private set; }
    public static AppSettings? Settings { get; private set; }
    private static BackgroundSyncTask? _backgroundSync;
    private static bool _isSyncing = false;

    public App()
    {
        InitializeComponent();

        string dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "livestock.db3"
        );
        FLDatabase = new FLDatabase(dbPath);


        LoadAppSettings();
        var syncService = new SyncService();
        _backgroundSync = new BackgroundSyncTask(syncService);
        _backgroundSync.Start();

        MainPage = new AppShell();

        // 🔄 Always sync at startup if online
        Task.Run(() => TrySyncAsync());

        // 🔄 Also sync when connection changes to internet
        Connectivity.ConnectivityChanged += async (s, e) =>
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                await TrySyncAsync();
            }
        };
    }

    // ✅ Sync when app resumes (back to foreground)
    protected override void OnResume()
    {
        base.OnResume();

        Task.Run(async () =>
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                await SyncAllAsync();
            }
        });
    }
    private async Task SyncAllAsync()
    {
        try
        {
            var syncService = new SyncService();
            await syncService.SyncAnimalsAsync();
            await syncService.SyncBookingsAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Sync Error] {ex.Message}");
        }
    }
    private static async Task TrySyncAsync()
    {
        if (_isSyncing)
            return;

        if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
        {
            try
            {
                _isSyncing = true;
                var syncService = new SyncService();
                await syncService.SyncAnimalsAsync();
                await syncService.SyncBookingsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Sync Error] {ex.Message}");
            }
            finally
            {
                _isSyncing = false;
            }
        }
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
    protected override void CleanUp()
    {
        _backgroundSync?.Stop();
        base.CleanUp();
    }
}
