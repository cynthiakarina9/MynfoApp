namespace Mynfo.Views
{
    using Mynfo.ViewModels;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
            var A = MainViewModel.GetInstance().User;
        }
        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MenuItemViewModel selectedItem = e.SelectedItem as MenuItemViewModel;
        }
        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            MenuItemViewModel tappedItem = e.Item as MenuItemViewModel;
        }
        void OnToggled(object sender, ToggledEventArgs e)
        {                      
            try
            {
                bool value = e.Value;
                int share = 1;
                if (value == true) { share = 1; }
                if (value == false) { share = 0; }
                int user_id = MainViewModel.GetInstance().User.UserId;
                string cadenaConexion = @"data source=serverappmynfo1.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";
                string queryLastBoxCreated = @"UPDATE Users SET Share = "+ share + "where UserId ="+ user_id;                
                
                SqlConnection con = new SqlConnection(cadenaConexion);
                SqlCommand sqlcom = new SqlCommand(queryLastBoxCreated, con);
                con.Open();
                    sqlcom.ExecuteNonQuery();
                con.Close();
                }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}