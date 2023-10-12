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
    public partial class TermEdit : ContentPage
    {
        public TermEdit()
        {
            InitializeComponent();
        }

        private readonly int _selectedTermId;

        public TermEdit(Term term)
        {
            InitializeComponent();
            _selectedTermId = term.Id;
            TermId.Text = term.Id.ToString();
            TermName.Text = term.Title;
            StartDatePicker.Date = term.StartDate;
            EndDatePicker.Date = term.EndDate;
        }

        async void SaveTerm_OnClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TermName.Text))
            {
                await DisplayAlert("Missing Title", "Please enter a Term Name.", "OK");
                return;
            }

            if (StartDatePicker.Date > EndDatePicker.Date)
            {
                await DisplayAlert("Invalid Dates", "Start Date must be before End Date.", "OK");
                return;
            }

            await DatabaseService.UpdateTerm(Int32.Parse(TermId.Text), TermName.Text, DateTime.Parse(StartDatePicker.Date.ToString()),
                DateTime.Parse(EndDatePicker.Date.ToString()));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            await Navigation.PopAsync();

        }

    }
}