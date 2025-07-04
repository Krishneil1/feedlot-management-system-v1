// -------------------------------------------------------------------------------------------------
//
// AnimalFormPage.xaml.cs -- The AnimalFormPage.xaml.cs class.
//
// Copyright (c) 2025 Krishneel Kumar. All rights reserved.
//
// -------------------------------------------------------------------------------------------------

namespace FeedlotApp
{
    using FeedlotApp.Data;
    using FeedlotApp.Views;

    public partial class App : Application
    {
        public FLDatabase FLDatabase { get; private set; }

        public App()
        {
            InitializeComponent();

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "animal.db3");
            FLDatabase = new FLDatabase(dbPath);

            MainPage = new NavigationPage(new AnimalFormPage());
        }
    }
}
