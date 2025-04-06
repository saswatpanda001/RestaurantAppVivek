using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RestaurantManagement.Models;
using RestaurantManagement.Services;
using RestaurantManagement.Views;

namespace RestaurantManagement.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        private DrinkService _drinkService;
        private List<Drink> _allDrinks;

        public Menu()
        {
            InitializeComponent();
            _drinkService = new DrinkService();
            LoadDrinksAndCategories();
            NavigationPage.SetHasBackButton(this, false);
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Home());
        }

        private async void LoadDrinksAndCategories()
        {
            _allDrinks = await _drinkService.GetAllDrinksAsync();
            if (_allDrinks == null)
            {
                await DisplayAlert("Error", "Failed to load drinks.", "OK");
                return;
            }

            // Load distinct categories into picker
            var categories = _allDrinks
                .Where(d => !string.IsNullOrEmpty(d.Category))
                .Select(d => d.Category)
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            categories.Insert(0, "All"); // Add "All" option
            CategoryPicker.ItemsSource = categories;

            // Show all drinks initially
            DrinksCollectionView.ItemsSource = _allDrinks;
        }

        private void OnCategorySelected(object sender, EventArgs e)
        {
            var selectedCategory = CategoryPicker.SelectedItem as string;

            if (string.IsNullOrWhiteSpace(selectedCategory) || selectedCategory == "All")
            {
                DrinksCollectionView.ItemsSource = _allDrinks;
            }
            else
            {
                var filtered = _allDrinks
                    .Where(d => d.Category != null && d.Category.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                DrinksCollectionView.ItemsSource = filtered;
            }
        }


        private async void GoToDrinkDetails(object sender, EventArgs e)
        {
            var button = sender as Button;
            var selectedDrink = button?.CommandParameter as Drink;

            if (selectedDrink != null)
            {
                await Navigation.PushAsync(new DrinkDetails(selectedDrink));
            }
        }



    }
}
