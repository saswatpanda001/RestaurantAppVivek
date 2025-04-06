using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;


namespace RestaurantManagement.ViewModels
{
    public class OrderHistoryViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Order> OrderHistory { get; set; }

        public OrderHistoryViewModel()
        {
            // Sample data for order history
            OrderHistory = new ObservableCollection<Order>
            {
                new Order { OrderId = 1001, OrderDate = DateTime.Now.AddDays(-3), TotalAmount = 45.00m, Status = "Delivered" },
                new Order { OrderId = 1002, OrderDate = DateTime.Now.AddDays(-7), TotalAmount = 32.00m, Status = "Cancelled" },
                new Order { OrderId = 1003, OrderDate = DateTime.Now.AddDays(-10), TotalAmount = 62.00m, Status = "Shipped" }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }
}
