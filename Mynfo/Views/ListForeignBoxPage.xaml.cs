namespace Mynfo.Views
{
    using Mynfo.Models;
    using Mynfo.ViewModels;
    using SQLite;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListForeignBoxPage : ContentPage
    {
        #region Contructor

        public IList<ForeingBox> foreingBox { private set; get; }
        public ListForeignBoxPage()
        {           
            InitializeComponent();
            List<ForeingBox> foreignBoxList = new List<ForeingBox>();
            foreingBox = new List<ForeingBox>();

            using (var conn = new SQLite.SQLiteConnection(App.root_db))
            {
                int a = conn.Table<ForeingProfile>().Count();

                foreignBoxList = conn.Table<ForeingBox>().ToList();
            }

            foreach (ForeingBox foreingBoxValue in foreignBoxList)
            {
                foreingBox.Add(foreingBoxValue);
            }

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