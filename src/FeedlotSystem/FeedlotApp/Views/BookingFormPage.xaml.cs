// -------------------------------------------------------------------------------------------------
// BookingFormPage.xaml.cs -- The BookingFormPage code-behind.
// -------------------------------------------------------------------------------------------------

using FeedlotApp.Models;

namespace FeedlotApp.Views
{
    [QueryProperty(nameof(BookingId), "bookingId")]
    public partial class BookingFormPage : ContentPage
    {
        public int BookingId { get; set; }

        private Booking? _existingBooking;

        public BookingFormPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BookingId != 0)
            {
                _existingBooking = await App.FLDatabase.GetBookingByIdAsync(BookingId);
                if (_existingBooking != null)
                {
                    BookingReferenceEntry.Text = _existingBooking.BookingNumber;
                    BookingDatePicker.Date = _existingBooking.BookingDate;
                    VendorEntry.Text = _existingBooking.VendorName;
                    PropertyEntry.Text = _existingBooking.Property;
                    TruckRegEntry.Text = _existingBooking.TruckReg;
                    NotesEditor.Text = _existingBooking.Notes;
                }
            }
        }

        private async void OnSaveBookingClicked(object sender, EventArgs e)
        {
            if (_existingBooking != null)
            {
                // Update existing booking
                _existingBooking.BookingNumber = BookingReferenceEntry.Text;
                _existingBooking.BookingDate = BookingDatePicker.Date;
                _existingBooking.VendorName = VendorEntry.Text;
                _existingBooking.Property = PropertyEntry.Text;
                _existingBooking.TruckReg = TruckRegEntry.Text;
                _existingBooking.Notes = NotesEditor.Text;
                _existingBooking.Synced = false;

                await App.FLDatabase.UpdateBookingAsync(_existingBooking);
                await DisplayAlert("Updated", "Booking updated and marked for sync.", "OK");
            }
            else
            {
                // Save new booking
                var newBooking = new Booking
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

                await App.FLDatabase.SaveBookingAsync(newBooking);
                await DisplayAlert("Saved", "Booking saved locally.", "OK");
            }

            // Reset form fields
            BookingReferenceEntry.Text = string.Empty;
            VendorEntry.Text = string.Empty;
            PropertyEntry.Text = string.Empty;
            TruckRegEntry.Text = string.Empty;
            NotesEditor.Text = string.Empty;
            BookingDatePicker.Date = DateTime.Today;

            BookingId = 0;
            _existingBooking = null;
        }
    }
}
