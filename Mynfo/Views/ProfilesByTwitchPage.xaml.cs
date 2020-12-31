namespace Mynfo.Views
{
    using Mynfo.Domain;
    using Mynfo.ViewModels;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilesByTwitchPage : ContentPage
    {
        #region Constructor
        public ProfilesByTwitchPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Commands
        private void NewProfileTwitch_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.CreateProfileTwitch = new CreateProfileTwitchViewModel();
            App.Navigator.PushAsync(new CreateProfileTwitchPage());
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
            mainViewModel.EditProfileTwitch = new EditProfileTwitchViewModel(tappedItem.ProfileMSId);
            App.Navigator.PushAsync(new EditProfileTwitchPage());
        }
        #endregion
    }
}