using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;



namespace RestaurantManagement.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Drink> _popularDrinks;
        public ObservableCollection<Drink> PopularDrinks
        {
            get { return _popularDrinks; }
            set
            {
                _popularDrinks = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PopularDrinks)));
            }
        }

        public HomeViewModel()
        {
            PopularDrinks = new ObservableCollection<Drink>
            {
                new Drink { Name = "Mango Smoothie", Price = "$4.99", Image = "mango_smoothie.png" },
                new Drink { Name = "Iced Coffee", Price = "$3.99", Image = "iced_coffee.png" },
                new Drink { Name = "Green Tea", Price = "$2.49", Image = "green_tea.png" }
            };
        }
    }

    public class Drink
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }
    }
}
