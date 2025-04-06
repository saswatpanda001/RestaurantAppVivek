using System;
using System.Collections.Generic;
using System.Text;
using RestaurantManagement.Models;

namespace RestaurantManagement.Services
{
    class SessionManager
    {
        public static User LoggedInUser { get; set; }
    }
}
