namespace Mynfo.Views
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Xamarin.Forms;
    using Models;
    using Xamarin.Forms.Xaml;
    using Mynfo.ViewModels;
    using Mynfo.Domain;
    using System.Data.SqlClient;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;

    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            var mainViewModel = MainViewModel.GetInstance();
            var Default = new Button();

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select * from dbo.Boxes where dbo.Boxes.UserId = 22"); 
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int valor1 = (int)reader["BoxId"];
                            string valor2 = (string)reader["Name"];
                            int valor3 = (int)reader["UserId"];
                        }
                    }
                }
            }

            //mainViewModel.Box = new BoxLocal();

            Default.Text = "Prueba 1";
            Default.BackgroundColor = Color.FromHex("#f9a589");
            Default.CornerRadius = 70;
            Default.FontAttributes = FontAttributes.Bold;
            Default.FontSize = 20;
            Default.HeightRequest = 140;
            Default.TextColor = Color.FromHex("#fff");
            Default.WidthRequest = 140;


            DefaultButton.Children.Add(Default);
        }

        private async void CreateBox_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.BoxRegister = new BoxRegisterViewModel();
            await Navigation.PushAsync(new BoxRegisterPage());
        }

    }
}