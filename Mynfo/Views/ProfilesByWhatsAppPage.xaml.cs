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
    public partial class ProfilesByWhatsAppPage : ContentPage
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        public IList<ProfileWhatsapp> profileWhatsapp { get; private set; }
        #endregion

        #region Constructors
        public ProfilesByWhatsAppPage()
        {
            apiService = new ApiService();
            SetList();
            InitializeComponent();
        }
        #endregion

        #region Commands
        private async void SetList()
        {
            //this.IsRunning = true;
            //this.isEnabled = false;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                //this.IsRunning = false;
                //this.isEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return;
            }

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();

            profileWhatsapp = new List<ProfileWhatsapp>();
            profileWhatsapp = await this.apiService.GetListByUser<ProfileWhatsapp>(
                apiSecurity,
                "/api",
                "/ProfileWhatsapps",
                MainViewModel.GetInstance().User.UserId);

            var Lista = profileWhatsapp;
            BindingContext = this;
        }
        private void NewProfileWhatsApp_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.CreateProfileWhatsApp = new CreateProfileWhatsAppViewModel();
            Navigation.PushAsync(new CreateProfileWhatsAppPage());
        }
        //private async void EditProfileEmail(object sender, EventArgs e, int _ProfileEmailId)
        //{
        //    var mainViewModel = MainViewModel.GetInstance();
        //    mainViewModel.EditProfileEmail = new EditProfileEmailViewModel();
        //    Application.Current.MainPage = new NavigationPage(new EditProfileEmailPage(_ProfileEmailId));
        //}
        private async void EditProfileEmail_Clicked()
        {
            var mainViewModel = MainViewModel.GetInstance();
            //mainViewModel.EditProfileEmail = new EditProfileWhatsAppViewModel();
            Application.Current.MainPage = new NavigationPage(new EditProfileEmailPage(0));
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
            ProfileWhatsapp selectedItem = e.SelectedItem as ProfileWhatsapp;
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            ProfileWhatsapp tappedItem = e.Item as ProfileWhatsapp;
            MainViewModel.GetInstance().EditProfileWhatsApp = new EditProfileWhatsAppViewModel(tappedItem.ProfileWhatsappId);
            Navigation.PushAsync(new EditProfileWhatsAppPage());
        }
        #endregion
    }
}