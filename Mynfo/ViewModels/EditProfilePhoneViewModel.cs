namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Services;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class EditProfilePhoneViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        private ProfilePhone profilephone;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public ProfilePhone profilePhone
        {
            get { return profilephone; }
            private set
            {
                SetValue(ref profilephone, value);
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
        public EditProfilePhoneViewModel(int _ProfilePhoneId)
        {
            apiService = new ApiService();
            GetProfilePhone(_ProfilePhoneId);
        }
        #endregion

        #region Commands
        private async Task<ProfilePhone> GetProfilePhone(int _ProfilePhoneId)
        {
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            profilePhone = new ProfilePhone();
            profilePhone = await this.apiService.GetProfilePhone(
               apiSecurity,
               "/api",
               "/ProfilePhones/GetProfilePhone",
               _ProfilePhoneId);
            return profilePhone;
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
                "/ProfilePhones/PutProfilePhone",
                profilePhone);

            this.IsRunning = false;
            this.IsEnabled = true;

            await App.Navigator.PopAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        //private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
