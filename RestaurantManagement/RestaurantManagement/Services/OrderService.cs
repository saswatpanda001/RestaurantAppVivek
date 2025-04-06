using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Models; // Ensure your Order model is properly imported
using Newtonsoft.Json;

namespace RestaurantManagement.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://10.0.2.2:23790/api/Orders"; // Replace with your actual API base URL

        public OrderService()
        {
            _httpClient = new HttpClient();
        }

        // ✅ Get All Orders
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Order>>(json);
        }

        // ✅ Get Order by ID
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Order>(json);
        }

        // ✅ Create New Order
        public async Task<bool> CreateOrderAsync(Order order)
        {
            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(BaseUrl, content);
            var responseContent = await response.Content.ReadAsStringAsync(); // 🔥 FIXED LINE

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Content: {responseContent}");

            return response.IsSuccessStatusCode;
        }


        // ✅ Update Existing Order
        public async Task<bool> UpdateOrderAsync(int id, Order order)
        {
            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{BaseUrl}/{id}", content);
            return response.IsSuccessStatusCode;
        }

        // ✅ Delete Order
        public async Task<bool> DeleteOrderAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
