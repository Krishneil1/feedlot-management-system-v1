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
                BookingNumber = BookingReferenceEntry.Text,
                BookingDate = BookingDatePicker.Date,
                VendorName = VendorEntry.Text,
                Property = PropertyEntry.Text,
                TruckReg = TruckRegEntry.Text,
                Notes = NotesEditor.Text,
                Status = "Pending",
                Synced = false
            };

            await App.FLDatabase.SaveBookingAsync(booking);
            await DisplayAlert("Saved", "Booking saved locally.", "OK");

            BookingReferenceEntry.Text = string.Empty;
            VendorEntry.Text = string.Empty;
            PropertyEntry.Text = string.Empty;
            TruckRegEntry.Text = string.Empty;
            NotesEditor.Text = string.Empty;
            BookingDatePicker.Date = DateTime.Today;
        }
    }
}
