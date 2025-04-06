using System;
using System.Collections.Generic;
using System.Text;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RestaurantManagement.ViewModels
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<MenuItem> _menuItems;

        public ObservableCollection<MenuItem> MenuItems
        {
            get => _menuItems;
            set
            {
                _menuItems = value;
                OnPropertyChanged();
            }
        }

        public MenuViewModel()
        {
            // Sample Data - You can fetch from an API/Database
            MenuItems = new ObservableCollection<MenuItem>
            {
                new MenuItem { Name = "Mango Smoothie", Price = "$5.99", Image = "mango_smoothie.jpg" },
                new MenuItem { Name = "Pasta Alfredo", Price = "$8.99", Image = "pasta.jpg" },
                new MenuItem { Name = "Chocolate Cake", Price = "$6.49", Image = "chocolate_cake.jpg" },
                new MenuItem { Name = "Iced Coffee", Price = "$4.99", Image = "iced_coffee.jpg" }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public class MenuItem
        {
            public string Name { get; set; }
            public string Price { get; set; }
            public string Image { get; set; }

        }
    }
}
