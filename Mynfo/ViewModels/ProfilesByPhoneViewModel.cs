﻿namespace Mynfo.ViewModels
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

    public class ProfilesByPhoneViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private ObservableCollection<ProfilePhone> profilePhone;
        #endregion

        #region Properties

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public ObservableCollection<ProfilePhone> profilephone
        {
            get { return profilePhone; }
            private set
            {
                SetValue(ref profilePhone, value);
            }
        }

        public ProfilePhone selectedProfile { get; set; }
        #endregion

        #region Constructor
        public ProfilesByPhoneViewModel()
        {
            apiService = new ApiService();
            GetList();
        }
        private async Task<ObservableCollection<ProfilePhone>> GetList()
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

            //Profilephone = new List<ProfilePhone>();
            profilephone = new ObservableCollection<ProfilePhone>();
            listPhone = await this.apiService.GetListByUser<ProfilePhone>(
                apiSecurity,
                "/api",
                "/ProfilePhones",
                MainViewModel.GetInstance().User.UserId);

            foreach (ProfilePhone profPhone in listPhone)
                profilephone.Add(profPhone);

            this.IsRunning = false;

            return profilephone;
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

        //Actualizar listas
        public void addProfile(ProfilePhone _profilePhone)
        {
            profilephone.Add(_profilePhone);
        }

        public void removeProfile()
        {
            profilephone.Remove(selectedProfile);
        }

        public void updateProfile(ProfilePhone _profilePhone)
        {
            int newIndex = profilephone.IndexOf(selectedProfile);
            profilephone.Remove(selectedProfile);

            profilephone.Insert(newIndex, _profilePhone);

        }
        #endregion
    }
}
