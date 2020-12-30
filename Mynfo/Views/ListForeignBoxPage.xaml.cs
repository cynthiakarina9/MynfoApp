namespace Mynfo.Views
{
    using Mynfo.Models;
    using Mynfo.ViewModels;
    using System;
    using System.Collections.Generic;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListForeignBoxPage : ContentPage
    {
        #region Contructor

        
        public ListForeignBoxPage()
        {           
            InitializeComponent();           

            BindingContext = this;
        }
        #endregion

        #region Commands
        private void BackHome_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }
        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ForeingBox selectedItem = e.SelectedItem as ForeingBox;
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            ForeingBox tappedItem = e.Item as ForeingBox;
            Navigation.PushAsync(new ForeingBoxPage(tappedItem));

        }
        #endregion
    }
}