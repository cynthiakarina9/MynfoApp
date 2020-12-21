namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Models;
    using Mynfo.Services;
    using Mynfo.Views;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class DetailsBoxViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private ObservableCollection<ProfileEmail> profileEmail;
        private ObservableCollection<ProfilePhone> profilePhone;
        private ObservableCollection<ProfileSM> profileSM;
        private ObservableCollection<ProfileWhatsapp> profileWhatsapp;
        private ObservableCollection<ProfileLocal> profilePerfiles;
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

        public ObservableCollection<ProfileSM> ProfileSM
        {
            get { return profileSM; }
            private set
            {
                SetValue(ref profileSM, value);
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

        public ObservableCollection<ProfileLocal> ProfilePerfiles
        {
            get { return profilePerfiles; }
            private set
            {
                SetValue(ref profilePerfiles, value);
            }
        }

        
        public ProfilePhone selectedProfile { get; set; }
        #endregion

        #region Constructor
        public DetailsBoxViewModel(int _BoxId)
        {
            
            apiService = new ApiService();
            ProfilePerfiles = new ObservableCollection<ProfileLocal>();
            GetListEmail(_BoxId);
            GetListPhone(_BoxId);
            GetListSM(_BoxId);
            GetListWhatsapp(_BoxId);
        }
        #endregion

        #region Methods
        public void ConverterToProfileLocal (int _BoxId)
        {
           
        }

        

        private async Task<ObservableCollection<ProfileEmail>> GetListEmail(int _BoxId)
        {
            this.IsRunning = true;
            List<ProfileEmail> listEmail;
            //var connection = await this.apiService.CheckConnection();
            //if (!connection.IsSuccess)
            //{
            //    this.IsRunning = false;
            //    await Application.Current.MainPage.DisplayAlert(
            //        Languages.Error,
            //        connection.Message,
            //        Languages.Accept);
            //    return null;
            //}
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            ProfileEmail = new ObservableCollection<ProfileEmail>();
            listEmail = await this.apiService.GetListByUser<ProfileEmail>(
                apiSecurity,
                "/api",
                "/ProfileEmails",
                MainViewModel.GetInstance().User.UserId);
            foreach (ProfileEmail ItemEmail in listEmail)
            {
                Box_ProfileEmail RelationEmail;
                RelationEmail = new Box_ProfileEmail
                {
                    BoxId = _BoxId,
                    ProfileEmailId = ItemEmail.ProfileEmailId
                };
                //apiSecurity = Application.Current.Resources["APISecurity"].ToString();
                var response = await this.apiService.Get(
                    apiSecurity,
                    "/api",
                    "/Box_ProfileEmail/GetBox_ProfileEmail",
                    RelationEmail);

                ItemEmail.Exist = response.IsSuccess;
                
                if (ItemEmail.Exist == true)
                {
                    var Email = Converter.ToProfileLocalE(ItemEmail);
                    ProfilePerfiles.Add(Email);
                }
            }
            //foreach (ProfileEmail profEmail in listEmail)
            //{
            //    if (profEmail.Exist == true)
            //    {
            //        ProfilePerfiles.Add(profEmail);
            //    }
            //}
            this.IsRunning = false;
            return ProfileEmail;
        }

        private async Task<ObservableCollection<ProfilePhone>> GetListPhone(int _BoxId)
        {
            this.IsRunning = true;
            List<ProfilePhone> listPhone;
            //var connection = await this.apiService.CheckConnection();
            //if (!connection.IsSuccess)
            //{
            //    this.IsRunning = false;
            //    await Application.Current.MainPage.DisplayAlert(
            //        Languages.Error,
            //        connection.Message,
            //        Languages.Accept);
            //    return null;
            //}

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            ProfilePhone = new ObservableCollection<ProfilePhone>();
            listPhone = await this.apiService.GetListByUser<ProfilePhone>(
                apiSecurity,
                "/api",
                "/ProfilePhones",
                MainViewModel.GetInstance().User.UserId);
            foreach (ProfilePhone ItemPhone in listPhone)
            {
                Box_ProfilePhone RelationPhone;
                RelationPhone = new Box_ProfilePhone
                {
                    BoxId = _BoxId,
                    ProfilePhoneId = ItemPhone.ProfilePhoneId
                };
                //apiSecurity = Application.Current.Resources["APISecurity"].ToString();
                var response = await this.apiService.Get(
                    apiSecurity,
                    "/api",
                    "/Box_ProfilePhone/GetBox_ProfilePhone",
                    RelationPhone);

                ItemPhone.Exist = response.IsSuccess;
                if (ItemPhone.Exist == true)
                {
                    var Phone = Converter.ToProfileLocalP(ItemPhone);
                    ProfilePerfiles.Add(Phone);
                }
            }
            //foreach (ProfilePhone profPhone in listPhone)
            //{
            //    if (profPhone.Exist == true)
            //    {
            //        ProfilePerfiles.Add(profPhone);
            //    }
            //}
            this.IsRunning = false;
            return ProfilePhone;
        }

        private async Task<ObservableCollection<ProfileSM>> GetListSM(int _BoxId)
        {
            this.IsRunning = true;
            List<ProfileSM> listSM;
            //var connection = await this.apiService.CheckConnection();
            //if (!connection.IsSuccess)
            //{
            //    this.IsRunning = false;
            //    await Application.Current.MainPage.DisplayAlert(
            //        Languages.Error,
            //        connection.Message,
            //        Languages.Accept);
            //    return null;
            //}

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();

            ProfileSM = new ObservableCollection<ProfileSM>();
            listSM = await this.apiService.GetListByUser<ProfileSM>(
                apiSecurity,
                "/api",
                "/ProfileSMs",
                MainViewModel.GetInstance().User.UserId);
            foreach (ProfileSM ItemSM in listSM)
            {
                Box_ProfileSM RelationSM;
                RelationSM = new Box_ProfileSM
                {
                    BoxId = _BoxId,
                    ProfileMSId = ItemSM.ProfileMSId
                };
                //apiSecurity = Application.Current.Resources["APISecurity"].ToString();
                var response = await this.apiService.Get(
                    apiSecurity,
                    "/api",
                    "/Box_ProfileSM/GetBox_ProfileSM",
                    RelationSM);
                ItemSM.Exist = response.IsSuccess;
                if (ItemSM.Exist == true)
                {
                    var SM = Converter.ToProfileLocalSM(ItemSM);
                    ProfilePerfiles.Add(SM);
                }
            }
            //foreach (ProfileSM profSM in listSM)
            //{
            //    if (profSM.Exist == true)
            //    {
            //        ProfileSM.Add(profSM);
            //    }
            //}
            this.IsRunning = false;
            return ProfileSM;
        }

        private async Task<ObservableCollection<ProfileWhatsapp>> GetListWhatsapp(int _BoxId)
        {
            this.IsRunning = true;
            List<ProfileWhatsapp> listWhatsapp;

            //var connection = await this.apiService.CheckConnection();

            //if (!connection.IsSuccess)
            //{
            //    this.IsRunning = false;
            //    await Application.Current.MainPage.DisplayAlert(
            //        Languages.Error,
            //        connection.Message,
            //        Languages.Accept);
            //    return null;
            //}

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            ProfileWhatsapp = new ObservableCollection<ProfileWhatsapp>();
            listWhatsapp = await this.apiService.GetListByUser<ProfileWhatsapp>(
                apiSecurity,
                "/api",
                "/ProfileWhatsapps",
                MainViewModel.GetInstance().User.UserId);
            foreach (ProfileWhatsapp ItemWhatsapp in listWhatsapp)
            {
                Box_ProfileWhatsapp RelationWhatsapp;
                RelationWhatsapp = new Box_ProfileWhatsapp
                {
                    BoxId = _BoxId,
                    ProfileWhatsappId = ItemWhatsapp.ProfileWhatsappId
                };
                //apiSecurity = Application.Current.Resources["APISecurity"].ToString();
                var response = await this.apiService.Get(
                    apiSecurity,
                    "/api",
                    "/Box_ProfileWhatsapp/GetBox_ProfileWhatsapp",
                    RelationWhatsapp);

                ItemWhatsapp.Exist = response.IsSuccess;
                if (ItemWhatsapp.Exist == true)
                {
                    var W = Converter.ToProfileLocalW(ItemWhatsapp);
                    ProfilePerfiles.Add(W);
                }
            }
            //foreach (ProfileWhatsapp profWhatsapp in listWhatsapp)
            //{
            //    if (profWhatsapp.Exist == true)
            //    {
            //        ProfilePerfiles.Add(profWhatsapp);
            //    }
            //}
            this.IsRunning = false;
            return ProfileWhatsapp;
        }


        //public async Task<ObservableCollection<object>> PostList(int Box)
        //{
        //    //var connection = await this.apiService.CheckConnection();

        //    //if (!connection.IsSuccess)
        //    //{
        //    //    this.IsRunning = false;
        //    //    await Application.Current.MainPage.DisplayAlert(
        //    //        Languages.Error,
        //    //        connection.Message,
        //    //        Languages.Accept);
        //    //    return null;
        //    //}
        //    ProfilePerfiles = new ObservableCollection<Object>();
        //    var Email = ProfileEmail;
        //    var Phone = ProfilePhone;
        //    var SM = ProfileSM;
        //    var Whatsapp = ProfileWhatsapp;
        //    foreach (ProfileEmail profEmail in profileEmail)
        //    {
        //        if (profEmail.Exist == true)
        //        {
        //            ProfilePerfiles.Add(profEmail);
        //        }
        //    }
        //    foreach (ProfilePhone profPhone in Phone)
        //    {
        //        if (profPhone.Exist == true)
        //        {
        //            ProfilePerfiles.Add(profPhone);
        //        }
        //    }
        //    foreach (ProfileSM profMS in SM)
        //    {
        //        if (profMS.Exist == true)
        //        {
        //            ProfilePerfiles.Add(profMS);
        //        }
        //    }
        //    foreach (ProfileWhatsapp profWhatsapp in Whatsapp)
        //    {
        //        if (profWhatsapp.Exist == true)
        //        {
        //            ProfilePerfiles.Add(profWhatsapp);
        //        }
        //    }

        //    return ProfilePerfiles;
        //}

        public void FillList ()
        {
            
        }
        #endregion
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
    }
}
