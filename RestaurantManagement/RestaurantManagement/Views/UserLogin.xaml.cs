using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using RestaurantManagement.Models;
using RestaurantManagement.Services;
using RestaurantManagement.Views;

namespace RestaurantManagement.Views
{
    public partial class UserLogin : ContentPage
    {
        private readonly UserService _userService;

        public UserLogin()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        // Login Button Clicked
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Please enter both email and password", "OK");
                return;
            }

            var users = await _userService.GetAllUsersAsync();

            // Check if any user exists with matching email and password
            var user = users?.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && u.PasswordHash.Equals(password));

            if (user != null)
            {
                SessionManager.LoggedInUser = user;
                await Navigation.PushAsync(new Home());
                // Successful login, proceed to the next page (e.g., Dashboard)
                SessionManager.LoggedInUser = user;
                await DisplayAlert("Success", "Login successful!", "OK");
                // Navigate to a new page, for example, a dashboard:
                // await Navigation.PushAsync(new DashboardPage());
            }
            else
            {
                // Show error if user not found or password does not match
                await DisplayAlert("Error", "Invalid email or password", "OK");
            }
        }

        // Forgot Password Clicked
        private async void OnClickedForgotPassword(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgotPassword());
        }

        private async void OnGoToSignUpClicked(object sender, EventArgs e)
        {
            // Navigate to SignUp page
            await Navigation.PushAsync(new SignupPage());
        }
    }
}



