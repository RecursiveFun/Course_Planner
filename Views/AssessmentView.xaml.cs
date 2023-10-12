using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course_Planner_App_Felix__Berinde.Models;
using Course_Planner_App_Felix__Berinde.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Course_Planner_App_Felix__Berinde.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentView : ContentPage
    {
        public AssessmentView()
        {
            InitializeComponent();
        }

        private readonly Course _selectedCourse;

        public AssessmentView(Course course)
        {
            InitializeComponent();
            _selectedCourse = course;
            Title.Text = course.Title;

        }

        async void AssessmentCollectionView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Assessment assessment = (Assessment)e.CurrentSelection.FirstOrDefault();
                await Navigation.PushAsync(new AssessmentDetail(assessment));
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            AssessmentCollectionView.ItemsSource = await DatabaseService.GetAssessments(_selectedCourse.Id);

        }

        async void AddAssessment_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AssessmentAdd(_selectedCourse));
        }
    }
}