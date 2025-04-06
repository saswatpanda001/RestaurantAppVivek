using System;
using System.Collections.Generic;
using System.Text;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModels;
using System.Collections.ObjectModel;
using RestaurantManagement.Services;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace RestaurantManagement.ViewModels
{
    public class CartViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<CartItem> CartItems { get; set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand CheckoutCommand { get; private set; }

        private decimal totalPrice;
        public decimal TotalPrice
        {
            get { return totalPrice; }
            set
            {
                if (totalPrice != value)
                {
                    totalPrice = value;
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }

        public CartViewModel()
        {
            // Sample data
            CartItems = new ObservableCollection<CartItem>
            {
                new CartItem { Name = "Mango Juice", Price = 12.00m, Image = "mango_juice.png" },
                new CartItem { Name = "Cold Coffee", Price = 15.00m, Image = "cold_coffee.png" },
                new CartItem { Name = "Green Tea", Price = 9.00m, Image = "green_tea.png" }
            };

            // Calculate total price initially
            UpdateTotalPrice();

            // Commands
            RemoveCommand = new Command<CartItem>(RemoveItem);
            CheckoutCommand = new Command(ProceedToCheckout);
        }

        private void RemoveItem(CartItem item)
        {
            if (item != null && CartItems.Contains(item))
            {
                CartItems.Remove(item);
                OnPropertyChanged(nameof(CartItems)); // Notify UI that the list has changed
                UpdateTotalPrice();
            }
        }
        private void UpdateTotalPrice()
        {
            TotalPrice = CartItems.Sum(item => item.Price);
        }

        private void ProceedToCheckout()
        {
            // Logic to navigate to the checkout page
            Application.Current.MainPage.DisplayAlert("Checkout", "Proceeding to checkout!", "OK");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class CartItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}
