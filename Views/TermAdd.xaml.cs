using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course_Planner_App_Felix__Berinde.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Course_Planner_App_Felix__Berinde.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermAdd : ContentPage
    {
        public TermAdd()
        {
            InitializeComponent();
        }

        async void SaveTerm_OnClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TermTitle.Text))
            {
                await DisplayAlert("Missing Name", "Please enter a name.", "OK");
                return;
            }

            if (StartDatePicker.Date > EndDatePicker.Date)
            {
                await DisplayAlert("Invalid Dates", "Start Date must be before End Date.", "OK");
                return;
            }

            await DatabaseService.AddTerm(TermTitle.Text, StartDatePicker.Date, EndDatePicker.Date);
            await Navigation.PopAsync();

        }
    }
}