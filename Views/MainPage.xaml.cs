using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course_Planner_App_Felix__Berinde.Models;
using Course_Planner_App_Felix__Berinde.Services;
using Course_Planner_App_Felix__Berinde.Views;
using Plugin.LocalNotifications;
using Xamarin.Forms;

namespace Course_Planner_App_Felix__Berinde
{

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }

        async void AddTerm_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TermAdd());
        }

        //async void Settings_OnClicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new AppSettings());
        //}

        async void TermCollectionView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Term term = (Term)e.CurrentSelection.FirstOrDefault();
                await Navigation.PushAsync(new TermView(term));
            }
        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();

            TermCollectionView.ItemsSource = await DatabaseService.GetTerms();

            var coursesList = await DatabaseService.GetCourses();

            var assessmentsList = await DatabaseService.GetAssessments();

            int notificationCount = 0;

            foreach (Course courseRecord in coursesList)
            {
                if (courseRecord.StartNotification)
                {
                    if (courseRecord.StartDate == DateTime.Today)
                    {
                        notificationCount++;

                        CrossLocalNotifications.Current.Show("Course Notice", $"{courseRecord.Title} begins today!", notificationCount);
                    }
                }

                if (courseRecord.EndNotification)
                {
                    if (courseRecord.EndDate == DateTime.Today)
                    {
                        notificationCount++;

                        CrossLocalNotifications.Current.Show("Course Notice", $"{courseRecord.Title} ends today!", notificationCount);
                    }
                }
            }

            foreach (Assessment assessmentRecord in assessmentsList)
            {
                if (assessmentRecord.StartNotification)
                {
                    if (assessmentRecord.StartDate == DateTime.Today)
                    {
                        notificationCount++;

                        CrossLocalNotifications.Current.Show("Assessment Notice", $"{assessmentRecord.Title} begins today!", notificationCount);
                    }
                }

                if (assessmentRecord.EndNotification)
                {
                    if (assessmentRecord.EndDate == DateTime.Today)
                    {
                        notificationCount++;

                        CrossLocalNotifications.Current.Show("Assessment Notice", $"{assessmentRecord.Title} is due today!", notificationCount);
                    }
                }
            }
        }
    }
}
