namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Services;
    using Mynfo.Views;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class ProfilesBYPESMViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private ObservableCollection<ProfileEmail> profileEmail;
        private ObservableCollection<ProfilePhone> profilePhone;
        private ObservableCollection<ProfileWhatsapp> profileWhatsapp;
        private ObservableCollection<ProfileSM> profileSM;
        #endregion

        #region Properties

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public ObservableCollection<ProfileEmail> ProfileEmail
        {
            get { return profileEmail; }
            private set
            {
                SetValue(ref profileEmail, value);
            }
        }

        public ObservableCollection<ProfilePhone> ProfilePhone
        {
            get { return profilePhone; }
            private set
            {
                SetValue(ref profilePhone, value);
            }
        }

        public ObservableCollection<ProfileWhatsapp> ProfileWhatsapp
        {
            get { return profileWhatsapp; }
            private set
            {
                SetValue(ref profileWhatsapp, value);
            }
        }

        public ObservableCollection<ProfileSM> ProfileSM
        {
            get { return profileSM; }
            private set
            {
                SetValue(ref profileSM, value);
            }
        }

        public ProfilePhone selectedProfilePhone { get; set; }
        public ProfileEmail selectedProfileEmail { get; set; }
        public ProfileWhatsapp selectedProfileWhatsapp { get; set; }
        public ProfileSM selectedProfileSM { get; set; }
        #endregion

        #region Cosntructor
        public ProfilesBYPESMViewModel()
        {
            apiService = new ApiService();
            GetListPhone();
        }
        #endregion

        #region Commands
        public ICommand GotoProfilesCommand
        {
            get
            {
                return new RelayCommand(GotoProfiles);
            }
        }
        private void GotoProfiles()
        {
            MainViewModel.GetInstance().Profiles = new ProfilesViewModel();
            App.Navigator.PushAsync(new ProfilesPage());
        }
        #endregion


        #region Methods
        private async Task<ObservableCollection<ProfileEmail>> GetListEmail()
        {
            this.IsRunning = true;
            List<ProfileEmail> listEmail;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return null;
            }

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();

            ProfileEmail = new ObservableCollection<ProfileEmail>();
            listEmail = await this.apiService.GetListByUser<ProfileEmail>(
                apiSecurity,
                "/api",
                "/ProfileEmails",
                MainViewModel.GetInstance().User.UserId);

            foreach (ProfileEmail profEmail in listEmail)
                ProfileEmail.Add(profEmail);

            this.IsRunning = false;

            return ProfileEmail;
        }
        private async Task<ObservableCollection<ProfilePhone>> GetListPhone()
        {
            this.IsRunning = true;
            List<ProfilePhone> listPhone;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return null;
            }

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();

            ProfilePhone = new ObservableCollection<ProfilePhone>();
            listPhone = await this.apiService.GetListByUser<ProfilePhone>(
                apiSecurity,
                "/api",
                "/ProfilePhones",
                MainViewModel.GetInstance().User.UserId);

            foreach (ProfilePhone profPhone in listPhone)
                ProfilePhone.Add(profPhone);

            this.IsRunning = false;

            return ProfilePhone;
        }
        private async Task<ObservableCollection<ProfileWhatsapp>> GetListWhatsapp()
        {
            this.IsRunning = true;
            List<ProfileWhatsapp> listWhastapp;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return null;
            }

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();

            ProfileWhatsapp = new ObservableCollection<ProfileWhatsapp>();
            listWhastapp = await this.apiService.GetListByUser<ProfileWhatsapp>(
                apiSecurity,
                "/api",
                "/ProfileWhatsapps",
                MainViewModel.GetInstance().User.UserId);

            foreach (ProfileWhatsapp profWhatsapp in listWhastapp)
                ProfileWhatsapp.Add(profWhatsapp);

            this.IsRunning = false;

            return ProfileWhatsapp;
        }
        private async Task<ObservableCollection<ProfileSM>> GetListSM()
        {
            this.IsRunning = true;
            List<ProfileSM> listSM;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return null;
            }

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();

            ProfileSM = new ObservableCollection<ProfileSM>();
            listSM = await this.apiService.GetListByUser<ProfileSM>(
                apiSecurity,
                "/api",
                "/ProfileSMs",
                MainViewModel.GetInstance().User.UserId);

            foreach (ProfileSM profSM in listSM)
                ProfileSM.Add(profSM);

            this.IsRunning = false;

            return ProfileSM;
        }
        #endregion
    }
}