namespace Mynfo.Views
{
    using Mynfo.Domain;
    using System;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilesByLinkedinPage : ContentPage
    {
        #region Constructor
        public ProfilesByLinkedinPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Commands
        private void NewProfileLinkedin_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.CreateProfileLinkedin = new CreateProfileLinkedinViewModel();
            App.Navigator.PushAsync(new CreateProfileLinkedinPage());
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
            if (tappedItem == null)
                return;
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.EditProfileLinkedin = new EditProfileLinkedinViewModel(tappedItem.ProfileMSId);
            ListaLinkedin.SelectedItem = null;
            App.Navigator.PushAsync(new EditProfileLinkedinPage());
        }
        #endregion
    }
}