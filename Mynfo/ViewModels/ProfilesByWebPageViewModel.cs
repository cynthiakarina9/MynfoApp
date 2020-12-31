namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Views;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class ProfilesByWebPageViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private ObservableCollection<ProfileSM> profilesM;
        #endregion

        #region Properties

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public ObservableCollection<ProfileSM> profileSM
        {
            get { return profilesM; }
            private set
            {
                SetValue(ref profilesM, value);
            }
        }

        public ProfileSM selectedProfile { get; set; }
        #endregion

        #region Constructor
        public ProfilesByWebPageViewModel()
        {
            apiService = new ApiService();
            GetList();
        }
        #endregion

        #region Commands

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

        #region Methods
        private async Task<ObservableCollection<ProfileSM>> GetList()
        {
            this.IsRunning = true;

            List<ProfileSM> profileSocialMedia;
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

            profileSM = new ObservableCollection<ProfileSM>();
            int IdNetwork = 10;
            profileSocialMedia = await this.apiService.GetProfileByNetWork(
                apiSecurity,
                "/api",
                "/ProfileSMs/GetProfileByNetWorkT",
                MainViewModel.GetInstance().User.UserId,
                IdNetwork);

            this.IsRunning = false;

            if (profileSocialMedia.Count == 0)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Information,
                    Languages.ProfileNull,
                    Languages.Accept);
                return null;
            }

            foreach (ProfileSM profSM in profileSocialMedia)
                profileSM.Add(profSM);

            return profileSM;
        }

        //Actualizar listas
        public void addProfile(ProfileSM _profileSM)
        {
            profileSM.Add(_profileSM);
        }

        public void removeProfile()
        {
            profileSM.Remove(selectedProfile);
        }

        #endregion
    }
}
