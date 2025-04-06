using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int DrinkId { get; set; }
        public int Quantity { get; set; }

        public User User { get; set; }
        public Drink Drink { get; set; }
    }
}
