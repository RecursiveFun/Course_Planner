using System;
using Course_Planner_App_Felix__Berinde.Services;
using Course_Planner_App_Felix__Berinde.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Course_Planner_App_Felix__Berinde
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (Settings.FirstRun)
            {

                DatabaseService.LoadSampleData();
                Settings.FirstRun = false;
            }

            var page = new SplashPage();

            var navPage = new NavigationPage(page);
            MainPage = navPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
