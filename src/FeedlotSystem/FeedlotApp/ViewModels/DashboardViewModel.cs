// -------------------------------------------------------------------------------------------------
//
// DashboardViewModel.cs -- The DashboardViewModel.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApp.ViewModels;

using System.Threading.Tasks;

public class DashboardViewModel : BaseViewModel
{
    private int _animalCount;
    public int AnimalCount
    {
        get => _animalCount;
        set { _animalCount = value; OnPropertyChanged(); }
    }

    private int _bookingCount;
    public int BookingCount
    {
        get => _bookingCount;
        set { _bookingCount = value; OnPropertyChanged(); }
    }

    protected override async Task OnRefresh()
    {
        var db = App.FLDatabase;
        AnimalCount = (await db.GetAllAnimalsAsync()).Count;
        BookingCount = (await db.GetAllBookingsAsync()).Count;
    }

    public DashboardViewModel()
    {
        _ = OnRefresh(); // Auto-refresh on load
    }
}
