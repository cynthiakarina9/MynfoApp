namespace Mynfo.Views
{
    using Mynfo.Domain;
    using Mynfo.ViewModels;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilesByTwitterPage : ContentPage
    {
        #region Constructor
        public ProfilesByTwitterPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void NewProfileTwitter_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.CreateProfileTwitter = new CreateProfileTwitterViewModel();
            App.Navigator.PushAsync(new CreateProfileTwitterPage());
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
            mainViewModel.EditProfileTwitter = new EditProfileTwitterViewModel(tappedItem.ProfileMSId);
            ListaTwitter.SelectedItem = null;
            App.Navigator.PushAsync(new EditProfileTwitterPage());
        }
        #endregion
    }
}