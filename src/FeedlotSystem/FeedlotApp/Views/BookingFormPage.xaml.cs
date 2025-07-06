using FeedlotApp.Models;

namespace FeedlotApp.Views
{
    public partial class BookingFormPage : ContentPage
    {
        public BookingFormPage()
        {
            InitializeComponent();
        }

        private async void OnSaveBookingClicked(object sender, EventArgs e)
        {
            var booking = new Booking
            {
                Reference = BookingReferenceEntry.Text,
                Date = BookingDatePicker.Date
            };

            await App.FLDatabase.SaveBookingAsync(booking);
            await DisplayAlert("Saved", "Booking saved locally.", "OK");

            BookingReferenceEntry.Text = string.Empty;
            BookingDatePicker.Date = DateTime.Today;
        }
    }
}
