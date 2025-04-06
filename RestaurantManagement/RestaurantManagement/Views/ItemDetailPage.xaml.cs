using RestaurantManagement.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace RestaurantManagement.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}