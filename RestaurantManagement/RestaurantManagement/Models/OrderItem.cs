using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int DrinkId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } // at time of ordering

        public Order Order { get; set; }
        public Drink Drink { get; set; }
    }
}
