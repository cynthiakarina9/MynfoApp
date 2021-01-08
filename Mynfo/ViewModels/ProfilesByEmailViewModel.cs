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
    public class ProfilesByEmailViewModel : BaseViewModel    
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private ObservableCollection<ProfileEmail> profilemail;
        public bool emptyList;
        #endregion

        #region Properties
        public bool EmptyList
        {
            get { return this.emptyList; }
            set { SetValue(ref this.emptyList, value); }
        }
        public ObservableCollection<ProfileEmail> profileEmail 
        {
            get { return profilemail; } 
            private set 
            {
                SetValue(ref profilemail, value);
            }
        }


        public bool IsRunning
        {
            get 
            { 
                return this.isRunning; 
            }
            set 
            { 
                SetValue(ref this.isRunning, value); 
            }
        }

        public ProfileEmail selectedProfile { get; set; }
        #endregion

        #region Constructor
        public ProfilesByEmailViewModel()
        {
            apiService = new ApiService();
            EmptyList = false;
            GetList();
        }
        #endregion

        #region Commands
        public async Task<ObservableCollection<ProfileEmail>> GetList()
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
            
            //profileEmail = new List<ProfileEmail>();
            profileEmail = new ObservableCollection<ProfileEmail>();
            listEmail = await this.apiService.GetListByUser<ProfileEmail>(
                apiSecurity,
                "/api",
                "/ProfileEmails",
                MainViewModel.GetInstance().User.UserId);

            this.IsRunning = false;

            
            if (listEmail.Count == 0)
            {
                EmptyList = true;
            }

            foreach (ProfileEmail profEmail in listEmail)
                profileEmail.Add(profEmail);

            
            return profileEmail;
            
        }
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
        public ICommand NewProfileEmailCommand
        {
            get
            {
                return new RelayCommand(NewProfileEmail);
            }
        }
        private void NewProfileEmail()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.CreateProfileEmail = new CreateProfileEmailViewModel();
            App.Navigator.PushAsync(new CreateProfileEmailPage());
        }

        //Actualizar listas
        public void addProfile(ProfileEmail _profileEmail)
        {
            profileEmail.Add(_profileEmail);
        }

        public void removeProfile()
        {
            profileEmail.Remove(selectedProfile);
        }

        public void updateProfile(ProfileEmail _profileEmail)
        {
            int newIndex = profileEmail.IndexOf(selectedProfile);
            profileEmail.Remove(selectedProfile);

            profileEmail.Insert(newIndex, _profileEmail);

        }
        #endregion
    }
}
