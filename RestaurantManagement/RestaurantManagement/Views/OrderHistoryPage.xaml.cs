using RestaurantManagement.Models;
using RestaurantManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace RestaurantManagement.Views
{
    public partial class OrderHistoryPage : ContentPage
    {
        private readonly OrderService _orderService = new OrderService();
        public List<Order> OrderHistory { get; set; }

        public OrderHistoryPage()
        {
            InitializeComponent();
            LoadOrders();
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void LoadOrders()
        {
            var userId = SessionManager.LoggedInUser.UserId;
            var allOrders = await _orderService.GetAllOrdersAsync();
            OrderHistory = allOrders?.Where(o => o.UserId == userId).OrderByDescending(o => o.OrderDate).ToList();
            OrdersListView.ItemsSource = OrderHistory;

        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Home());
        }

        private async void OrdersListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedOrder = e.Item as Order;
            if (selectedOrder != null)
            {
                await Navigation.PushAsync(new OrderItemsPage(selectedOrder.OrderId));
            }
        }
    }
}
