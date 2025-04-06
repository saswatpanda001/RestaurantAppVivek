using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RestaurantManagement.Models;
using RestaurantManagement.Services;

namespace RestaurantManagement.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        private readonly OrderService _orderService = new OrderService();
        private readonly OrderItemService _orderItemService = new OrderItemService();
        private readonly DrinkService _drinkService = new DrinkService();

        public Command<Drink> DrinkTappedCommand { get; }

        public Home()
        {
            InitializeComponent();
            BindingContext = this;

            DrinkTappedCommand = new Command<Drink>(GoToDrinkDetail);

            LoadUserData();
            LoadOrderStatistics();
            LoadBestSellingDrinks();
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            SessionManager.LoggedInUser = null;
            await Navigation.PushAsync(new AboutPage());
        }


        private async void GoToDrinkDetail(Drink drink)
        {
            if (drink != null)
                await Navigation.PushAsync(new DrinkDetails(drink));
        }

        private void LoadUserData()
        {
            WelcomeLabel.Text = $"Welcome, {SessionManager.LoggedInUser.Username}!";
        }

        private async void LoadOrderStatistics()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            var orderItems = await _orderItemService.GetAllOrderItemsAsync();

            TotalOrdersLabel.Text = $"Total Orders: {orders.Count}";
            TotalPriceLabel.Text = $"Total Price: ₹{orders.Sum(o => o.TotalPrice):F2}";
            TotalOrderItemsLabel.Text = $"Total Order Items: {orderItems.Count}";
        }

        private async void LoadBestSellingDrinks()
        {
            var orderItems = await _orderItemService.GetAllOrderItemsAsync();
            var allDrinks = await _drinkService.GetAllDrinksAsync();

            var bestSelling = orderItems
                .GroupBy(oi => oi.DrinkId)
                .Select(g => new
                {
                    DrinkId = g.Key,
                    Quantity = g.Sum(oi => oi.Quantity)
                })
                .OrderByDescending(d => d.Quantity)
                .Take(4)
                .ToList();

            var bestSellingDrinks = allDrinks
                .Where(d => bestSelling.Any(b => b.DrinkId == d.DrinkId))
                .ToList();

            BestSellingDrinksCollectionView.ItemsSource = bestSellingDrinks;
        }

        private async void GoToMenu(object sender, EventArgs e) => await Navigation.PushAsync(new Menu());

        private async void GoToOrders(object sender, EventArgs e) => await Navigation.PushAsync(new OrderHistoryPage());

        private async void GoToCart(object sender, EventArgs e) => await Navigation.PushAsync(new CartPage());

        private async void GoToLogout(object sender, EventArgs e)
        {
            SessionManager.LoggedInUser = null;
            await Navigation.PushAsync(new AboutPage());
        }
    }
}
