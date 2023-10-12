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
    public partial class CourseAdd : ContentPage
    {
        public CourseAdd()
        {
            InitializeComponent();
        }

        private readonly Term _selectedTerm;

        public CourseAdd(Term term)
        {
            InitializeComponent();
            _selectedTerm = term;
        }

        async void SaveCourse_OnClicked(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(CourseTitle.Text))
            {
                await DisplayAlert("Missing Name", "Please enter a name.", "OK");
            }

            else if (StartDatePicker.Date > EndDatePicker.Date)
            {
                await DisplayAlert("Invalid Dates", "Start Date must be before End Date.", "OK");
                return;
            }

            else if (Status.SelectedIndex == -1)
            {
                await DisplayAlert("Status Blank", "A Status must be selected.", "OK");
            }

            else if (string.IsNullOrWhiteSpace(InstructorName.Text))
            {
                await DisplayAlert("Missing Instructor Name", "Please enter an Instructor's name.", "OK");
            }

            else if (string.IsNullOrWhiteSpace(InstructorPhone.Text))
            {
                await DisplayAlert("Missing Instructor Phone Number", "Please enter an Instructor's phone number.", "OK");
            }

            else if (string.IsNullOrWhiteSpace(InstructorEmail.Text))
            {
                await DisplayAlert("Missing Instructor Email", "Please enter an Instructor's Email.", "OK");
            }
            else
            {
                await DatabaseService.AddCourse(_selectedTerm.Id, CourseTitle.Text, StartDatePicker.Date,
                    EndDatePicker.Date, Status.SelectedItem.ToString(), InstructorName.Text, InstructorPhone.Text,
                    InstructorEmail.Text, Notes.Text, StartNotification.IsToggled, EndNotification.IsToggled);
                await DatabaseService.GetCourses(_selectedTerm.Id);
                await Navigation.PopAsync();
            }
        }
    }
}