using Mynfo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mynfo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }
        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MenuItemViewModel selectedItem = e.SelectedItem as MenuItemViewModel;
        }
        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            MenuItemViewModel tappedItem = e.Item as MenuItemViewModel;
        }        
    }
}