// -------------------------------------------------------------------------------------------------
// AnimalFormPage.xaml.cs -- The AnimalFormPage code-behind.
// -------------------------------------------------------------------------------------------------

namespace FeedlotApp.Views;

using System;
using Microsoft.Maui.Controls;
using FeedlotApp.Models;

public partial class AnimalFormPage : ContentPage
{
    public AnimalFormPage()
    {
        InitializeComponent();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var animal = new Animal
        {
            TagId = TagIdEntry.Text,
            Breed = BreedEntry.Text,
            DateOfBirth = DobPicker.Date,
            Synced = false
        };

        await App.Current.MainPage.DisplayAlert("Saving", "Saving animal...", "OK");

        await App.FLDatabase.SaveAnimalAsync(animal);

        await DisplayAlert("Saved", "Animal saved locally", "OK");

        TagIdEntry.Text = string.Empty;
        BreedEntry.Text = string.Empty;
        DobPicker.Date = DateTime.Today;
    }
}
