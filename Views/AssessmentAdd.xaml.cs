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
    public partial class AssessmentAdd : ContentPage
    {
        public AssessmentAdd()
        {
            InitializeComponent();
        }

        private readonly Course _selectedCourse;

        public AssessmentAdd(Course course)
        {
            InitializeComponent();
            _selectedCourse = course;
        }
        async void SaveCourse_OnClicked(object sender, EventArgs e)
        {
            var assessments = await DatabaseService.GetAssessments(_selectedCourse.Id);
            int objectiveCount = 0;
            int performanceCount = 0;

            foreach (var assessment in assessments)
            {
                if (assessment.Type == "Objective")
                {
                    objectiveCount++;
                }

                if (assessment.Type == "Performance")
                {
                    performanceCount++;
                }
            }

            if (string.IsNullOrWhiteSpace(AssessmentTitle.Text))
            {
                await DisplayAlert("Missing Name", "Please enter a name.", "OK");
            }
            else if (StartDatePicker.Date > EndDatePicker.Date)
            {
                await DisplayAlert("Invalid Dates", "Start Date must be before End Date.", "OK");
            }
            else if (Type.SelectedIndex == -1)
            {
                await DisplayAlert("Missing Type", "An assessment Type must be selected.", "OK");
            }

            else if ((Type.SelectedItem.ToString() == "Objective" && objectiveCount > 0) ||
                     (Type.SelectedItem.ToString() == "Performance" && performanceCount > 0))
            {
                await DisplayAlert("Too Many Assessments", "Only 1 assessment of each type is allowed. Please check how many performance and objective assessments are present.", "OK");
            }

            else
            {
                await DatabaseService.AddAssessment(_selectedCourse.Id, AssessmentTitle.Text, StartDatePicker.Date,
                    EndDatePicker.Date, Type.SelectedItem.ToString(), StartNotification.IsToggled, EndNotification.IsToggled);
                await DatabaseService.GetAssessments(_selectedCourse.Id);
                await Navigation.PopAsync();
            }
        }
    }
}