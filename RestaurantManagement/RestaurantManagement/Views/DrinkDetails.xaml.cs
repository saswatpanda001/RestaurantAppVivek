using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RestaurantManagement.Models;
using RestaurantManagement.Services;

namespace RestaurantManagement.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrinkDetails : ContentPage
    {
        private Drink _selectedDrink;

        public DrinkDetails(Drink drink)
        {
            InitializeComponent();
            _selectedDrink = drink;
            BindingContext = _selectedDrink;
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Home());
        }

        private async void AddToCart(object sender, EventArgs e)
        {
            if (int.TryParse(QuantityEntry.Text, out int quantity) && quantity > 0)
            {
                var cart = new Cart
                {
                    UserId = SessionManager.LoggedInUser.UserId, // ✅ get from session
                    DrinkId = _selectedDrink.DrinkId,
                    Quantity = quantity
                };

                var cartService = new CartService();
                bool success = await cartService.CreateCartAsync(cart);

                if (success)
                {
                    
                    await Navigation.PushAsync(new CartPage());

                }
                else
                {
                    await DisplayAlert("Error", "Failed to add item to cart. Please try again.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Please enter a valid quantity.", "OK");
            }
        }

    }
}
