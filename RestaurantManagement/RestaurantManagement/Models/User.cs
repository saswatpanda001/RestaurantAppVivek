﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Cart> CartItems { get; set; }
        public List<Order> Orders { get; set; }
    }
}
