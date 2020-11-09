﻿namespace Mynfo.Views
{
    using Mynfo.ViewModels;
    using System;
    using System.Data.SqlClient;
    using System.Text;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateProfileFacebookPage : ContentPage
    {
        public CreateProfileFacebookPage(bool _BoxDefault = false, int _boxId = 0)
        {
            InitializeComponent();

            if (_boxId == 0)
            {
                SaveWBox.IsVisible = false;
                BackButtonBox.IsVisible = false;
            }
            else
            {
                Save.IsVisible = false;
                BackButton.IsVisible = false;
            }

            SaveWBox.Clicked += new EventHandler((sender, e) => backToAssignProfiles(sender, e, _boxId, ProfileName.Text, ProfileLink.Text, _BoxDefault));
            BackButtonBox.Clicked += new EventHandler((sender, e) => BackBox_Clicked(sender, e, _boxId, _BoxDefault));
        }
        private void backToAssignProfiles(object sender, EventArgs e, int _BoxId, string _profileName, string _profileLink, bool _BoxDefault)
        {
            string query = "insert into dbo.ProfileSMs(ProfileName, link, UserId, RedSocialId)" +
                            "Values('" + _profileName + "', '" + _profileLink + "', " + MainViewModel.GetInstance().User.UserId + ",1)";

            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            StringBuilder sb;

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(query);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            Application.Current.MainPage = new NavigationPage(new ProfilesBYPESMPage(_BoxId, "Facebook", _BoxDefault));
        }
        private void Back_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByFacebook = new ProfilesByFacebookViewModel();
            Application.Current.MainPage = new NavigationPage(new ProfilesByFacebookPage());
        }
        private void BackBox_Clicked(object sender, EventArgs e, int _BoxId, bool _boxDefault)
        {
            Application.Current.MainPage = new NavigationPage(new ProfilesBYPESMPage(_BoxId, "Facebook", _boxDefault));
        }
        private void BackHome_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }
    }
}