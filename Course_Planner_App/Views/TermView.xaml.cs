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
    public partial class TermView : ContentPage
    {
        public TermView()
        {
            InitializeComponent();
        }


        private readonly Term _selectedTerm;

        public TermView(Term term)
        {
            InitializeComponent();

            _selectedTerm = term;
            Title.Text = term.Title;
            Start.Text = term.StartDate.ToShortDateString();
            End.Text = term.EndDate.ToShortDateString();
        }

        async void AddCourse_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CourseAdd(_selectedTerm));
        }

        async void EditTerm_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TermEdit(_selectedTerm));
        }



        async void CourseCollectionView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Course course = (Course)e.CurrentSelection.FirstOrDefault();
                await Navigation.PushAsync(new CourseView(course));
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            CourseCollectionView.ItemsSource = await DatabaseService.GetCourses(_selectedTerm.Id);

        }

        async void Button_OnClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete Term?", "Delete this Term?", "Yes", "No");

            if (answer == true)
            {
                var id = int.Parse(_selectedTerm.Id.ToString());

                await DatabaseService.RemoveTerm(id);

                await DisplayAlert("Term Deleted", "Term Deleted", "OK");

                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Delete Canceled", "Nothing Deleted", "OK");
            }

        }
    }
}