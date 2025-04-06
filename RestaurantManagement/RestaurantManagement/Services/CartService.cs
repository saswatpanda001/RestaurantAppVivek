using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Models; // Ensure your Cart model is properly imported
using Newtonsoft.Json;

    namespace RestaurantManagement.Services
    {
        public class CartService
        {
            private readonly HttpClient _httpClient;
            private const string BaseUrl = "http://10.0.2.2:23790/api/Carts"; // Replace with your actual API base URL

            public CartService()
            {
                _httpClient = new HttpClient();
            }

            // ✅ Get All Carts
            public async Task<List<Cart>> GetAllCartsAsync()
            {
                var response = await _httpClient.GetAsync(BaseUrl);
                if (!response.IsSuccessStatusCode) return null;

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Cart>>(json);
            }
        // ✅ Delete Cart Item
        public async Task<bool> DeleteCartAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> CreateCartAsync(Cart cart)
        {
            var json = JsonConvert.SerializeObject(cart);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(BaseUrl, content);
            return response.IsSuccessStatusCode;
        }


        

        // ✅ Get Cart by ID
        public async Task<Cart> GetCartByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Cart>(json);
        }

        // ✅ Get Carts by User ID
        public async Task<List<Cart>> GetCartsByUserIdAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/User/{userId}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Cart>>(json);
        }

        // ✅ Create New Cart Item
        

        // ✅ Update Cart Item
        public async Task<bool> UpdateCartAsync(int id, Cart cart)
        {
            var json = JsonConvert.SerializeObject(cart);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{BaseUrl}/{id}", content);
            return response.IsSuccessStatusCode;
        }

        
    }
}
