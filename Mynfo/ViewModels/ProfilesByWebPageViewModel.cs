namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Views;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
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
        public bool emptyList;
        #endregion

        #region Properties
        public bool EmptyList
        {
            get { return this.emptyList; }
            set { SetValue(ref this.emptyList, value); }
        }
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
            EmptyList = false;
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
        private void BackHome()
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
                EmptyList = true;
            }

            var ListOrderBy = profileSocialMedia.OrderBy(x => x.ProfileName).ToList();
            foreach (ProfileSM profSM in ListOrderBy)
                profileSM.Add(profSM);

            

            return profileSM;
        }

        #region Listas
        public void addProfile(ProfileSM _profileSM)
        {
            profileSM.Add(_profileSM);
            EmptyList = true;
        }

        public void removeProfile()
        {
            profileSM.Remove(selectedProfile);
            if (profileSM.Count == 0)
            {
                EmptyList = true;
            }
        }

        public void updateProfile(ProfileSM _profileSM)
        {
            int newIndex = profileSM.IndexOf(selectedProfile);
            profileSM.Remove(selectedProfile);

            profileSM.Insert(newIndex, _profileSM);
            selectedProfile = null;
        }
        #endregion

        #endregion
    }
}
