using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Models; // Make sure your OrderItem model is properly imported
using Newtonsoft.Json;

namespace RestaurantManagement.Services
{
    public class OrderItemService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://10.0.2.2:23790/api/OrderItems"; // Replace with your actual API base URL

        public OrderItemService()
        {
            _httpClient = new HttpClient();
        }

        // ✅ Get All OrderItems
        public async Task<List<OrderItem>> GetAllOrderItemsAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<OrderItem>>(json);
        }


        // ✅ Create New OrderItem
        public async Task<bool> CreateOrderItemAsync(OrderItem orderItem)
        {
            var json = JsonConvert.SerializeObject(orderItem);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(BaseUrl, content);
            return response.IsSuccessStatusCode;
        }

       

        // ✅ Get OrderItem by ID
        public async Task<OrderItem> GetOrderItemByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OrderItem>(json);
        }

       

        // ✅ Update Existing OrderItem
        public async Task<bool> UpdateOrderItemAsync(int id, OrderItem orderItem)
        {
            var json = JsonConvert.SerializeObject(orderItem);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{BaseUrl}/{id}", content);
            return response.IsSuccessStatusCode;
        }

        // ✅ Delete OrderItem
        public async Task<bool> DeleteOrderItemAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
