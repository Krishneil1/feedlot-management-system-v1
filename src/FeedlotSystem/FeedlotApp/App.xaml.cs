using FeedlotApp.Data;
using FeedlotApp.Sync;
using FeedlotApp.Views;
using Microsoft.Maui.Networking;

namespace FeedlotApp;

public partial class App : Application
{
    public static FLDatabase FLDatabase { get; private set; }

    public App()
    {
        InitializeComponent();

        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "animal.db3");
        FLDatabase = new FLDatabase(dbPath);

        MainPage = new NavigationPage(new AnimalFormPage());

        // 🔄 Sync on connectivity change
        Connectivity.ConnectivityChanged += async (s, e) =>
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                try
                {
                    await new SyncService().SyncAnimalsAsync();
                }
                catch (Exception ex)
                {
                    // Optional: log or alert the user
                    Console.WriteLine($"[Sync Error] {ex.Message}");
                }
            }
        };
    }
}
