using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.ViewModels
{
    public partial class OrderItemViewModel
    {
        public int DrinkId { get; set; }
        public string DrinkName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
