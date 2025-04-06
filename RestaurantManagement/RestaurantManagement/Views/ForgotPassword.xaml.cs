using System;
using System.Linq;
using Xamarin.Forms;
using RestaurantManagement.Models;
using RestaurantManagement.Services;

namespace RestaurantManagement.Views
{
    public partial class ForgotPassword : ContentPage
    {
        private readonly UserService _userService;

        public ForgotPassword()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        // Reset Password Button Clicked
        private async void OnResetPasswordClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string newPassword = NewPasswordEntry.Text;
            string confirmPassword = ConfirmPasswordEntry.Text;

            // Check if all fields are filled
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                await DisplayAlert("Error", "Please fill all fields", "OK");
                return;
            }

            // Check if password and confirm password match
            if (newPassword != confirmPassword)
            {
                await DisplayAlert("Error", "Password and Confirm Password do not match", "OK");
                return;
            }

            // Get the list of users and check if the email exists
            var users = await _userService.GetAllUsersAsync();
            var existingUser = users?.FirstOrDefault(u => u.Email == email);

            if (existingUser == null)
            {
                await DisplayAlert("Error", "Email not found", "OK");
                return;
            }

            // Update the password for the existing user
            existingUser.PasswordHash = newPassword;  // In a real application, hash the password before saving it

            var updated = await _userService.UpdateUserAsync(existingUser.UserId, existingUser);

            if (updated)
            {
                await DisplayAlert("Success", "Password reset successfully", "OK");
                // Navigate to the login page
                await Navigation.PushAsync(new UserLogin());
            }
            else
            {
                await DisplayAlert("Error", "An error occurred while resetting your password. Please try again.", "OK");
            }
        }

        // Back to Login Button Clicked
        private async void OnBackToLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserLogin());
        }
    }
}
