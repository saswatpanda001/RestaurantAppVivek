using RestaurantManagement.Models;
using RestaurantManagement.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using RestaurantManagement.Views;

namespace RestaurantManagement.Views
{
    public partial class CartPage : ContentPage
    {
        private readonly CartService _cartService = new CartService();
        private readonly DrinkService _drinkService = new DrinkService();

        public ObservableCollection<Cart> CartItems { get; set; } = new ObservableCollection<Cart>();
        public decimal TotalPrice { get; set; }

        public CartPage()
        {
            InitializeComponent();
            BindingContext = this;
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Home());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            TotalPrice = 0;
            CartItems.Clear();

            var allCarts = await _cartService.GetAllCartsAsync();
            var userId = SessionManager.LoggedInUser.UserId;
            var userCarts = allCarts?.Where(c => c.UserId == userId).ToList();

            if (userCarts != null)
            {
                foreach (var cart in userCarts)
                {
                    // Get drink details
                    var drink = await _drinkService.GetDrinkByIdAsync(cart.DrinkId);
                    if (drink != null)
                    {
                        cart.Drink = drink;
                        TotalPrice += drink.Price * cart.Quantity;
                    }

                    CartItems.Add(cart);
                }
            }

            OnPropertyChanged(nameof(TotalPrice));
        }

        private async void OnRemoveItem(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var cartItem = (Cart)button.CommandParameter;

            bool isDeleted = await _cartService.DeleteCartAsync(cartItem.CartId);
            if (isDeleted)
            {
                CartItems.Remove(cartItem);
                await DisplayAlert("Removed", "Item removed from cart", "OK");
                await Navigation.PushAsync(new CartPage());
                Navigation.RemovePage(this);


            }
        }

        private async void OnUpdateQuantity(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var cartItem = (Cart)button.CommandParameter;

            if (cartItem.Quantity > 0)
            {
                bool isUpdated = await _cartService.UpdateCartAsync(cartItem.CartId, cartItem);
                if (isUpdated)
                {
                    await DisplayAlert("Updated", "Quantity updated", "OK");
                    await Navigation.PushAsync(new CartPage());
                    Navigation.RemovePage(this);

                }
                else
                {
                    await DisplayAlert("Error", "Failed to update quantity", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Quantity must be more than 0", "OK");
            }
        }

        private async void OnClickedOrder(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Checkout(TotalPrice));
           
        }
    }
}
