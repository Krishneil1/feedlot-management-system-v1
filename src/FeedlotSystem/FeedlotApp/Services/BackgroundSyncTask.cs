// -------------------------------------------------------------------------------------------------
//
// BackgroundSyncTask.cs -- The BackgroundSyncTask.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApp.Services;
using System;
using System.Threading.Tasks;
using FeedlotApp.Services.Sync;

public class BackgroundSyncTask
{
    private readonly SyncService _syncService;
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(2); // Adjust as needed
    private CancellationTokenSource? _cts;

    public BackgroundSyncTask(SyncService syncService)
    {
        _syncService = syncService;
    }

    public void Start()
    {
        _cts = new CancellationTokenSource();
        Task.Run(() => RunSyncLoopAsync(_cts.Token));
    }

    public void Stop()
    {
        _cts?.Cancel();
    }

    private async Task RunSyncLoopAsync(CancellationToken token)
    {
        var timer = new PeriodicTimer(_interval);
        while (await timer.WaitForNextTickAsync(token))
        {
            try
            {
                await _syncService.SyncAnimalsAsync();
                await _syncService.SyncBookingsAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[BackgroundSync Error] {ex.Message}");
            }
        }
    }
}

