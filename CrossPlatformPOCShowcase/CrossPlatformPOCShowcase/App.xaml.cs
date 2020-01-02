using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CrossPlatformPOCShowcase.Services;
using CrossPlatformPOCShowcase.Views;
using CrossPlatformPOCShowcase.Models;
using System.Diagnostics;

namespace CrossPlatformPOCShowcase
{
    public partial class App : Application
    {
        public static PocRepository Repository;
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string AzureBackendUrl;
            
        public static DataStoreTypes datastore = DataStoreTypes.Azure;
        public static string SqliteDbPath;
        public App()
        {
            InitializeComponent();
            
            switch (datastore)
            {
                case DataStoreTypes.Mock:
                    DependencyService.Register<MockDataStore>();
                    break;
                case DataStoreTypes.Azure:
                    //AzureBackendUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:44379" : "https://localhost:44379";
                    AzureBackendUrl = "https://crossplatformpoc.azurewebsites.net";
                    DependencyService.Register<AzureDataStore>();
                    break;
                case DataStoreTypes.Sqlite:
                    Debug.WriteLine($"Database located at: {SqliteDbPath}");
                    Repository = new PocRepository(SqliteDbPath);
                    DependencyService.Register<SqliteDataStore>();
                    break;
                default:
                    break;
            }

                
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }

    
}
