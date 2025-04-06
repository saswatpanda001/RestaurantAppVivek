using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Models; // Make sure your Drink model is properly imported
using Newtonsoft.Json;

namespace RestaurantManagement.Services
{
    public class DrinkService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://10.0.2.2:23790/api/Drinks"; // Replace with your actual API base URL

        public DrinkService()
        {
            _httpClient = new HttpClient();
        }
        // ✅ Get Drink by ID
        public async Task<Drink> GetDrinkByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Drink>(json);
        }

        // ✅ Get All Drinks
        public async Task<List<Drink>> GetAllDrinksAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Drink>>(json);
        }

        

        // ✅ Create New Drink
        public async Task<bool> CreateDrinkAsync(Drink drink)
        {
            var json = JsonConvert.SerializeObject(drink);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(BaseUrl, content);
            return response.IsSuccessStatusCode;
        }

        // ✅ Update Existing Drink
        public async Task<bool> UpdateDrinkAsync(int id, Drink drink)
        {
            var json = JsonConvert.SerializeObject(drink);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{BaseUrl}/{id}", content);
            return response.IsSuccessStatusCode;
        }

        // ✅ Delete Drink
        public async Task<bool> DeleteDrinkAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
