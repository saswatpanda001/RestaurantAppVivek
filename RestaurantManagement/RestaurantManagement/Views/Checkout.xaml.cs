using Newtonsoft.Json;
using RestaurantManagement.Models;
using RestaurantManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
namespace RestaurantManagement.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Checkout : ContentPage
    {
        private readonly decimal _totalPrice;
        private readonly int _loggedInUserId;
        private readonly CartService _cartService = new CartService();
        private readonly OrderService _orderService = new OrderService();
        private readonly OrderItemService _orderItemService = new OrderItemService();
        private readonly DrinkService _drinkService = new DrinkService();

        public Checkout(decimal totalPrice)
        {
            InitializeComponent();
            _totalPrice = totalPrice;

            // 👇 Show total price on the UI
            TotalPriceLabel.Text = $"Total Price: ₹{_totalPrice:F2}";

            // 👇 Assuming logged-in user ID is stored in App.Current.Properties
            if (SessionManager.LoggedInUser != null)
            {
                _loggedInUserId = SessionManager.LoggedInUser.UserId;
            }
            else
            {
                DisplayAlert("Error", "User not logged in.", "OK");
            }
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Home());
        }


        private async void PlaceOrderClicked(object sender, EventArgs e)
        {
            var fullName = FullNameEntry.Text;
            var address = AddressEntry.Text;
            var phone = PhoneEntry.Text;
            var paymentMethod = PaymentPicker.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(paymentMethod))
            {
                await DisplayAlert("Error", "Please fill in all the fields.", "OK");
                return;
            }


            var order = new Order
            {
                UserId = _loggedInUserId,
                FullName = fullName,
                Address = address,
                PhoneNumber = phone,
                PaymentMethod = paymentMethod,
                TotalPrice = _totalPrice,
                OrderDate = DateTime.Now,
                
            };

            Console.WriteLine($"UserId: {_loggedInUserId} (Type: { _loggedInUserId.GetType() })");
            Console.WriteLine($"FullName: {fullName} (Type: { fullName?.GetType() })");
            Console.WriteLine($"Address: {address} (Type: { address?.GetType() })");
            Console.WriteLine($"PhoneNumber: {phone} (Type: { phone?.GetType() })");
            Console.WriteLine($"PaymentMethod: {paymentMethod} (Type: { paymentMethod?.GetType() })");
            Console.WriteLine($"TotalPrice: {_totalPrice} (Type: { _totalPrice.GetType() })");
            Console.WriteLine($"OrderDate: {DateTime.Now} (Type: { DateTime.Now.GetType() })");




            var orderCreated = await _orderService.CreateOrderAsync(order);
            Console.WriteLine(orderCreated);


           

            if (orderCreated == null)
            {
                await DisplayAlert("Error", "Failed to create order.", "OK");
                return;
            }

            var allOrders = await _orderService.GetAllOrdersAsync();
            var currentOrderCreated = allOrders.OrderByDescending(o => o.OrderId).FirstOrDefault();
            Console.WriteLine(currentOrderCreated.UserId);
            Console.WriteLine(currentOrderCreated.OrderId);





           
            var allCarts = await _cartService.GetAllCartsAsync();
            var userCarts = allCarts?.Where(c => c.UserId == _loggedInUserId).ToList();

            if (userCarts == null || !userCarts.Any())
            {
                await DisplayAlert("Error", "No cart items found.", "OK");
                return;
            }

            // 3️⃣ Create OrderItems
            foreach (var cart in userCarts)
            {
                var drink = await _drinkService.GetDrinkByIdAsync(cart.DrinkId);
                if (drink == null) continue;

                var orderItem = new OrderItem
                {
                    OrderId = currentOrderCreated.OrderId,
                    DrinkId = cart.DrinkId,
                    Quantity = cart.Quantity,
                    Price = drink.Price * cart.Quantity
                };

                await _orderItemService.CreateOrderItemAsync(orderItem);
            }

            // 4️⃣ Delete cart items
            foreach (var cart in userCarts)
            {
                await _cartService.DeleteCartAsync(cart.CartId);
            }

            await DisplayAlert("Success", "Order placed successfully!", "OK");
            await Navigation.PushAsync(new OrderHistoryPage());
        }
    }
}
