﻿namespace Mynfo.Views
{
    using Mynfo.ViewModels;
    using System;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilesByEmailPage : ContentPage
    {

        #region Constructors
        public ProfilesByEmailPage()
        {
            InitializeComponent();
            
            int UserId = MainViewModel.GetInstance().User.UserId;
            string queryGetEmailByUser = "select * from dbo.ProfileEmails where dbo.ProfileEmails.UserId = " + UserId;
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            System.Text.StringBuilder sb;

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(queryGetEmailByUser);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var emailProfile = new Label();
                            var emailAddress = new Label();
                            //var deleteProfile = new Button();
                            var editProfile = new ImageButton();
                            var Line = new BoxView();

                            emailProfile.Text = (string)reader["Name"];
                            emailProfile.FontSize = 15;
                            emailProfile.FontAttributes = FontAttributes.Bold;

                            emailAddress.Text = (string)reader["Email"];
                            emailAddress.FontSize = 25;
                            emailAddress.HorizontalTextAlignment = TextAlignment.Center;

                            /*deleteProfile.Text = "B";
                            deleteProfile.TextColor = Color.Black;
                            deleteProfile.FontSize = 10;
                            deleteProfile.BackgroundColor = Color.FromHex("#f9a589");
                            deleteProfile.CornerRadius = 15;
                            deleteProfile.HeightRequest = 30;
                            deleteProfile.WidthRequest = 30;
                            deleteProfile.HorizontalOptions = LayoutOptions.End;
                            deleteProfile.Clicked += new EventHandler((sender, e) => DeleteBoxEmail(sender, e, BoxId, EmailId));*/
                            int EmailId = (int)reader["ProfileEmailId"];
                            editProfile.Source = "edit2";
                            editProfile.BackgroundColor = Color.Transparent;
                            editProfile.CornerRadius = 15;
                            editProfile.HeightRequest = 30;
                            editProfile.WidthRequest = 30;
                            editProfile.HorizontalOptions = LayoutOptions.End;
                            editProfile.Clicked += new EventHandler((sender, e) => EditProfileEmail(sender, e, EmailId));

                            Line.HeightRequest = 1;
                            Line.Color = Color.FromHex("#FF5521");

                            EmailList.Children.Add(emailProfile);
                            EmailList.Children.Add(emailAddress);
                            //EmailList.Children.Add(deleteProfile);
                            EmailList.Children.Add(editProfile);
                            EmailList.Children.Add(Line);
                        }
                    }
                    connection.Close();
                }
            }
         }
        #endregion

        #region Commands
        
        private void NewProfileEmail_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.CreateProfileEmail = new CreateProfileEmailViewModel();
            Application.Current.MainPage = new NavigationPage(new CreateProfileEmailPage());
        }
        private async void EditProfileEmail(object sender, EventArgs e, int _ProfileEmailId)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.EditProfileEmail = new EditProfileEmailViewModel();
            Application.Current.MainPage = new NavigationPage(new EditProfileEmailPage(_ProfileEmailId));
        }
        private void Back_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Profiles = new ProfilesViewModel();
            Application.Current.MainPage = new NavigationPage(new ProfilesPage());
        }
        private void BackHome_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }
        #endregion
    }
}