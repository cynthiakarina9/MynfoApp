namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Models;
    using Mynfo.Services;
    using Mynfo.Views;
    using Rg.Plugins.Popup.Services;
    using System;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class CreateProfileWhatsAppViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Atributtes
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
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
        public string Name
        {
            get;
            set;
        }
        public string Number
        {
            get;
            set;
        }
        public string Number2
        {
            get;
            set;
        }
        public string Lada
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public CreateProfileWhatsAppViewModel()
        {
            this.apiService = new ApiService();

            this.IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand SaveProfileWhatsAppCommand
        {
            get
            {
                return new RelayCommand(SaveProfileWhatsApp);
            }
        }
        private async void SaveProfileWhatsApp()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NameValidation,
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.Number))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NumberValidation,
                    Languages.Accept);
                return;
            }
            if (!(this.Number).ToCharArray().All(Char.IsDigit))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NumberValidation,
                    Languages.Accept);
                return;
            }
            if (this.Number.Length != 10)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PhoneValidation2,
                    Languages.Accept);
                return;
            }

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

            var mainViewModel = MainViewModel.GetInstance();

            
            var profileWhatsApp = new ProfileWhatsapp
            {
                Name = this.Name,
                Number = this.Number,
                UserId = mainViewModel.User.UserId,
                Exist = false
            };

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var profileWhatsapp = await this.apiService.Post(
                apiSecurity,
                "/api",
                "/ProfileWhatsapps",
                profileWhatsApp);

            if (profileWhatsapp == default)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ErrorAddProfile,
                    Languages.Accept);
                return;
            }
            var ProfileLocal = new Profile
            {
                UserId = mainViewModel.User.UserId,
                ProfileName = profileWhatsApp.Name,
                value = profileWhatsApp.Number,
                ProfileType = "Whatsapp",
                Logo = "whatsapp2",
                ProfileId = profileWhatsApp.ProfileWhatsappId,
            };
            using (var conn = new SQLite.SQLiteConnection(App.root_db))
            {
                conn.CreateTable<Profile>();
                conn.Insert(ProfileLocal);
            }
            this.IsRunning = false;
            this.IsEnabled = true;

            //Agregar a la lista
            if (mainViewModel.ProfilesBYPESM != null)
            {
                mainViewModel.ProfilesBYPESM.addProfileWhatsapp(profileWhatsapp);
                mainViewModel.ListOfNetworks.addProfileWhatsapp(profileWhatsapp);
            }
            else
            {
                mainViewModel.Profiles.addProfileWhatsapp(profileWhatsapp);
            }

            this.Name = string.Empty;
            this.Number = string.Empty;

            if (mainViewModel.ProfilesBYPESM != null)
            {
                await PopupNavigation.Instance.PopAsync();
            }
            else
            {
                await App.Navigator.PopAsync();
            }
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
        #endregion
    }
}
