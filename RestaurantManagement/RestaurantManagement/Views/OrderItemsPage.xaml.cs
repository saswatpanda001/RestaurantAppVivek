using RestaurantManagement.Models;
using RestaurantManagement.Services;
using RestaurantManagement.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace RestaurantManagement.Views
{
    public partial class OrderItemsPage : ContentPage
    {
        private readonly OrderItemService _orderItemService = new OrderItemService();
        private readonly DrinkService _drinkService = new DrinkService();
        private int _orderId;

        public OrderItemsPage(int orderId)
        {
            InitializeComponent();
            _orderId = orderId;
            LoadOrderItems();
          
        }

     
        private async void LoadOrderItems()
        {
            var allItems = await _orderItemService.GetAllOrderItemsAsync();
            var filteredItems = allItems?.Where(item => item.OrderId == _orderId).ToList();

            var orderItemViewModels = new List<OrderItemViewModel>();

            foreach (var item in filteredItems)
            {
                var drink = await _drinkService.GetDrinkByIdAsync(item.DrinkId);
                orderItemViewModels.Add(new OrderItemViewModel
                {
                    DrinkId = item.DrinkId,
                    DrinkName = drink?.Name ?? "Unknown",
                    Quantity = item.Quantity,
                    Price = item.Price
                });
            }

            OrderItemsListView.ItemsSource = orderItemViewModels;
        }
    }
}
