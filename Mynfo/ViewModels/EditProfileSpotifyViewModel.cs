namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Views;
    using Services;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class EditProfileSpotifyViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        private ProfileSM profileSm;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public ProfileSM profileSM
        {
            get { return profileSm; }
            private set
            {
                SetValue(ref profileSm, value);
            }
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        #endregion

        #region Constructor
        public EditProfileSpotifyViewModel(int _ProfileMSId)
        {
            apiService = new ApiService();
            GetProfile(_ProfileMSId);
        }
        #endregion

        #region Commands
        private async Task<ProfileSM> GetProfile(int _ProfileMSId)
        {
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            profileSM = new ProfileSM();
            profileSM = await this.apiService.GetProfileSM(
               apiSecurity,
               "/api",
               "/ProfileSMs/GetProfileSM",
               _ProfileMSId);
            return profileSM;
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }
        private async void Save()
        {
            this.IsRunning = true;
            this.IsEnabled = false;
            var checkConnetion = await this.apiService.CheckConnection();
            if (!checkConnetion.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    checkConnetion.Message,
                    Languages.Accept);
                return;
            }

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var response = await this.apiService.PutProfile(
                apiSecurity,
                "/api",
                "/ProfileSMs/PutProfileSM",
                profileSM);

            this.IsRunning = false;
            this.IsEnabled = true;

            await App.Navigator.PopAsync();
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(Delete);
            }
        }
        private async void Delete()
        {
            this.IsRunning = true;
            this.IsEnabled = false;

            var checkConnetion = await this.apiService.CheckConnection();
            if (!checkConnetion.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    checkConnetion.Message,
                    Languages.Accept);
                return;
            }
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var response = await this.apiService.Delete1(
                apiSecurity,
                "/api",
                "/Box_ProfileSM/DeleteBox_ProfileSMRelations",
                profileSM.ProfileMSId);

            var response2 = await this.apiService.Delete(
                apiSecurity,
                "/api",
                "/ProfileSMs",
                profileSM.ProfileMSId);

            this.IsRunning = false;
            this.IsEnabled = true;

            MainViewModel.GetInstance().ProfilesBySpotify.removeProfile();

            await App.Navigator.PopAsync();
        }
        public ICommand BackHomeCommand
        {
            get
            {
                return new RelayCommand(BackHome);
            }
        }

        private async void BackHome()
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }
        #endregion
    }
}
