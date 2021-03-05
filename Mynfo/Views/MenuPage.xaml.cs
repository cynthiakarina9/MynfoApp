namespace Mynfo.Views
{
    using Mynfo.ViewModels;
    using System;
    using System.Data.SqlClient;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            get_share();
            InitializeComponent();
            if (MainViewModel.GetInstance().User.Share == true)
            {
                TagLabel.Text = "myTAG ON";
            }
            else
            {
                TagLabel.Text = "myTAG OFF";
            }
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
                if (value == true) { share = 1; TagLabel.Text = "myTAG ON"; }
                if (value == false) { share = 0; TagLabel.Text = "myTAG OFF"; }
                int user_id = MainViewModel.GetInstance().User.UserId;
                //string cadenaConexion = @"data source=serverappmynfo1.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";
                string cadenaConexion = @"data source=serverappmynfo.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";
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
        public void get_share()
        {
            //string cadenaConexion = @"data source=serverappmynfo1.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";
            string cadenaConexion = @"data source=serverappmynfo.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";
            string queryLastBoxCreated = "select* from users where UserId =" + MainViewModel.GetInstance().User.UserId;
            System.Text.StringBuilder sb;
            bool Share;
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(queryLastBoxCreated);

                string sql = sb.ToString();

                using (SqlCommand command3 = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command3.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Share = (bool)reader["Share"];
                            MainViewModel.GetInstance().User.Share = Share;
                        }
                    }
                    connection.Close();
                }
            }
        }
    }
}