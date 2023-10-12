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
    public partial class AssessmentEdit : ContentPage
    {
        public AssessmentEdit()
        {
            InitializeComponent();
        }

        private readonly Assessment _selectedAssessment;

        public AssessmentEdit(Assessment assessment)
        {
            InitializeComponent();
            _selectedAssessment = assessment;
            AssessmentTitle.Text = assessment.Title;
            StartDatePicker.Date = assessment.StartDate;
            EndDatePicker.Date = assessment.EndDate;
            Type.SelectedItem = assessment.Type;
            StartNotification.IsToggled = assessment.StartNotification;
            EndNotification.IsToggled = assessment.EndNotification;
        }

        async void SaveCourse_OnClicked(object sender, EventArgs e)
        {
            var assessments = await DatabaseService.GetAssessments(_selectedAssessment.CourseId);
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

            else if ((_selectedAssessment.Type == "Objective" && Type.SelectedItem.ToString() == "Performance" && performanceCount > 0) ||
                     (_selectedAssessment.Type == "Performance" && Type.SelectedItem.ToString() == "Objective" && objectiveCount > 0))
            {
                await DisplayAlert("Too Many Assessments", "Only 1 assessment of each type is allowed. Please check how many performance and objective assessments are present.", "OK");
            }

            else
            {
                await DatabaseService.UpdateAssessment(_selectedAssessment.Id, AssessmentTitle.Text,
                    StartDatePicker.Date, EndDatePicker.Date, Type.SelectedItem.ToString(), StartNotification.IsToggled, EndNotification.IsToggled);

                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                await Navigation.PopAsync();
            }
        }
    }
}