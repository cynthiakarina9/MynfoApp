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

    public class ProfilesByWhatsAppViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isVisible;
        private ObservableCollection<ProfileWhatsapp> profileWhatsapp;
        public bool emptyList;
        #endregion

        #region Properties
        public bool EmptyList
        {
            get { return this.emptyList; }
            set { SetValue(ref this.emptyList, value); }
        }

        public ObservableCollection<ProfileWhatsapp> profileWhatsApp
        {
            get { return profileWhatsapp; }
            private set
            {
                SetValue(ref profileWhatsapp, value);

            }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public bool IsVisible
        {
            get { return this.isVisible; }
            set { SetValue(ref this.isVisible, value); }
        }
        public string NoNetworks { get; set; }

        public ProfileWhatsapp selectedProfile { get; set; }
        #endregion

        #region Constructors
        public ProfilesByWhatsAppViewModel()
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
        public async Task<ObservableCollection<ProfileWhatsapp>> GetList()
        {
            this.IsRunning = true;

            List<ProfileWhatsapp> listWhats;

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

            profileWhatsApp = new ObservableCollection<ProfileWhatsapp>();
            listWhats = await this.apiService.GetListByUser<ProfileWhatsapp>(
                apiSecurity,
                "/api",
                "/ProfileWhatsapps",
                MainViewModel.GetInstance().User.UserId);

            this.IsRunning = false;

            if (listWhats.Count == 0)
            {
                EmptyList = true;
            }

            foreach (ProfileWhatsapp profWhatsapp in listWhats)
                profileWhatsApp.Add(profWhatsapp);

            return profileWhatsApp;

        }

        #region Listas
        public void addProfile(ProfileWhatsapp _profileWhatsapp)
        {
            profileWhatsApp.Add(_profileWhatsapp);
            EmptyList = false;
        }

        public void removeProfile()
        {
            profileWhatsApp.Remove(selectedProfile);
            if (profileWhatsApp.Count == 0)
            {
                EmptyList = true;
            }
        }

        public void updateProfile(ProfileWhatsapp _profileWhatsapp)
        {
            int newIndex = profileWhatsApp.IndexOf(selectedProfile);
            profileWhatsApp.Remove(selectedProfile);

            profileWhatsApp.Insert(newIndex, _profileWhatsapp);
            selectedProfile = null;
        }
        #endregion

        #endregion
    }
}
