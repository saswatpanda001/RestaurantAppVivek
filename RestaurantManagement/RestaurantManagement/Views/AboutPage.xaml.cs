using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RestaurantManagement.Views;


namespace RestaurantManagement.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void OnClickedLogin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserLogin()); 
        }

        private async void OnClickedExplore(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignupPage());
        }


    }
}