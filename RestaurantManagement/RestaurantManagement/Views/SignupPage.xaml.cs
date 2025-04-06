using System;
using System.Linq;
using Xamarin.Forms;
using RestaurantManagement.Models;
using RestaurantManagement.Services;

namespace RestaurantManagement.Views
{
    public partial class SignupPage : ContentPage
    {
        private readonly UserService _userService;

        public SignupPage()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        // Sign Up Button Clicked
        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;
            string confirmPassword = ConfirmPasswordEntry.Text;

            // Check if all fields are filled
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                await DisplayAlert("Error", "Please fill all fields", "OK");
                return;
            }

            // Check if password and confirm password match
            if (password != confirmPassword)
            {
                await DisplayAlert("Error", "Password and Confirm Password do not match", "OK");
                return;
            }

            // Check if email or username already exists
            var existingUserByEmail = await _userService.GetAllUsersAsync();
            var existingUser = existingUserByEmail?.FirstOrDefault(u => u.Email == email || u.Username == username);

            if (existingUser != null)
            {
                await DisplayAlert("Error", "Email or Username already exists", "OK");
                return;
            }

            // Create a new user
            User newUser = new User
            {
                Username = username,
                Email = email,
                PasswordHash = password, // In a real scenario, hash the password before storing it
                CreatedAt = DateTime.Now
            };

            var userCreated = await _userService.CreateUserAsync(newUser);

            if (userCreated)
            {
                await DisplayAlert("Success", "Account created successfully!", "OK");
                // Navigate to Login page after successful sign up
                await Navigation.PushAsync(new UserLogin());
            }
            else
            {
                await DisplayAlert("Error", "An error occurred while creating your account. Please try again.", "OK");
            }
        }

        // Go to Login Page
        private async void OnGoToLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserLogin());
        }
    }
}
