using MVVM.Views;
using Xamarin.Forms;

namespace MVVM
{
    public partial class App : Application
    {
        public const string SenderID = "586026715943";
        public const string ListenConnectionString = "Endpoint=sb://sekizdesekiznh.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=eTZzh4kEV0+mP3ty52havW8fG0+9ocT8Cqrp4gvM6Ws=";
        public const string NotificationHubName = "sekizdesekiznh";
        public const string PackageName = "SekizdeSekiz.SekizdeSekiz";
        public static string[] anons = null;
        public static string DbName { get; set; } = "8DE8ANONS.db3";
        public App()
        {
            MainPage = new NavigationPage(new ListViewPage1());
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
