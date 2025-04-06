using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Models
{
    public class Drink
    {
        public int DrinkId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        public List<Cart> Carts { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
