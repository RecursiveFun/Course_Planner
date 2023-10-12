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
    public partial class AssessmentDetail : ContentPage
    {
        public AssessmentDetail()
        {
            InitializeComponent();
        }

        private readonly Assessment _selectedAssessment;

        public AssessmentDetail(Assessment assessment)
        {

            InitializeComponent();
            _selectedAssessment = assessment;
            Title.Text = assessment.Title;
            Type.Text = assessment.Type;
            Start.Text = assessment.StartDate.ToShortDateString();
            End.Text = assessment.EndDate.ToShortDateString();
            StartNotification.IsToggled = assessment.StartNotification;
            EndNotification.IsToggled = assessment.EndNotification;
        }


        async void EditAssessment_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AssessmentEdit(_selectedAssessment));
        }

        async void DeleteAssessment_OnClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete Assessment?", "Delete this Assessment?", "Yes", "No");

            if (answer == true)
            {
                var id = int.Parse(_selectedAssessment.Id.ToString());

                await DatabaseService.RemoveAssessment(id);

                await DisplayAlert("Assessment Deleted", "Assessment Deleted", "OK");

                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Delete Canceled", "Nothing Deleted", "OK");
            }
        }

        async void Home_OnClicked(object sender, EventArgs e)
        {
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Navigation.PopAsync();
        }
    }
}