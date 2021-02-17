namespace Mynfo.Views
{
    using Mynfo.Domain;
    using Mynfo.Services;
    using System;
    using System.Collections.Generic;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilesByPhonePage : ContentPage
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        public IList<ProfilePhone> profilePhone { get; private set; }
        #endregion

        #region Constructor
        public ProfilesByPhonePage()
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
        private void NewProfilePhone_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.CreateProfilePhone = new CreateProfilePhoneViewModel();
            App.Navigator.PushAsync(new CreateProfilePhonePage());
        }
        private void BackHome_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }
        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ProfilePhone selectedItem = e.SelectedItem as ProfilePhone;
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            ProfilePhone tappedItem = e.Item as ProfilePhone;
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.EditProfilePhone = new EditProfilePhoneViewModel(tappedItem.ProfilePhoneId);
            App.Navigator.PushAsync(new EditProfilePhonePage(tappedItem.ProfilePhoneId));
        }
        #endregion
    }
}