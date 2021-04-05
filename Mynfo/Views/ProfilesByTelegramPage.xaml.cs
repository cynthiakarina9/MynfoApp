namespace Mynfo.Views
{
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Services;
    using Mynfo.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilesByTelegramPage : ContentPage
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        public IList<ProfileWhatsapp> profileTelegram { get; private set; }
        #endregion

        #region Constructors
        public ProfilesByTelegramPage()
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
        private void NewProfileTelegram_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.CreateProfileSM = new CreateProfileSMViewModel("Telegram");
            Navigation.PushAsync(new CreateProfileTelegramPage());
        }

        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ProfileSM selectedItem = e.SelectedItem as ProfileSM;
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            ProfileSM tappedItem = e.Item as ProfileSM;
            MainViewModel.GetInstance().EditProfileTelegram = new EditProfileTelegramViewModel(tappedItem.ProfileMSId);
            Navigation.PushAsync(new EditProfileTelegramPage());
        }
        #endregion
    }
}