﻿namespace Mynfo.Views
{
    using Mynfo.Domain;
    using Mynfo.ViewModels;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilesByTiktokPage : ContentPage
    {
        #region Constructor
        public ProfilesByTiktokPage()
        {
            InitializeComponent();
            OSAppTheme currentTheme = App.Current.RequestedTheme;
            if (currentTheme == OSAppTheme.Dark)
            {
                Logosuperior.Source = "logo_superior2.png";
            }
            else
            {
                Logosuperior.Source = "logo_superior3.png";
            }
        }
        #endregion

        #region Commands
        private void NewProfileTiktok_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.CreateProfileSM = new CreateProfileSMViewModel("TikTok"); ;
            App.Navigator.PushAsync(new CreateProfileTiktokPage());
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
        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ProfileSM selectedItem = e.SelectedItem as ProfileSM;
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {

            ProfileSM tappedItem = e.Item as ProfileSM;
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.EditProfileTiktok = new EditProfileTiktokViewModel(tappedItem.ProfileMSId);
            App.Navigator.PushAsync(new EditProfileTiktokPage());
        }
        #endregion
    }
}