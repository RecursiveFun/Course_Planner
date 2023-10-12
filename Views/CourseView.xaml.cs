using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Course_Planner_App_Felix__Berinde.Models;
using Course_Planner_App_Felix__Berinde.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Course_Planner_App_Felix__Berinde.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseView : ContentPage
    {
        public CourseView()
        {
            InitializeComponent();
        }

        private readonly Course _selectedCourse;

        public CourseView(Course course)
        {
            InitializeComponent();

            _selectedCourse = course;
            Title.Text = course.Title;
            Start.Text = course.StartDate.ToShortDateString();
            End.Text = course.EndDate.ToShortDateString();
            Status.Text = course.Status;
            InstructorName.Text = course.InstructorName;
            InstructorPhone.Text = course.InstructorPhone;
            InstructorEmail.Text = course.InstructorEmail;
            StartNotification.IsToggled = course.StartNotification;
            EndNotification.IsToggled = course.EndNotification;
            Notes.Text = course.Notes;
        }

        async void DeleteCourse_OnClickedButton_OnClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete Course?", "Delete this Course?", "Yes", "No");

            if (answer == true)
            {
                var id = int.Parse(_selectedCourse.Id.ToString());

                await DatabaseService.RemoveCourse(id);

                await DisplayAlert("Course Deleted", "Course Deleted", "OK");

                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Delete Canceled", "Nothing Deleted", "OK");
            }
        }

        async void EditCourse_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CourseEdit(_selectedCourse));
        }


        async void Share_OnClicked(object sender, EventArgs e)
        {
            var notes = _selectedCourse.Notes;

            await Share.RequestAsync(new ShareTextRequest()
            {
                Text = notes,
                Title = _selectedCourse.Title
            });
        }

        async void Assessments_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AssessmentView(_selectedCourse));
        }
    }
}