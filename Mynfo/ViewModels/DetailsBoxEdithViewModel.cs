﻿namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Models;
    using Mynfo.Services;
    using Mynfo.Views;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class DetailsBoxEdithViewModel : BaseViewModel
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


        public ProfileSM selectedProfileSM { get; set; }
        #endregion

        #region Constructor
        public DetailsBoxEdithViewModel(int _BoxId)
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

        #region Email
        private async Task<ObservableCollection<ProfileEmail>> GetListEmail(int _BoxId)
        {
            this.IsRunning = true;
            List<ProfileEmail> listEmail;
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
                    var Email = Converter.ToProfileLocalE1(ItemEmail);
                    ProfilePerfiles.Add(Email);
                }
            }
            this.IsRunning = false;
            return ProfileEmail;
        }

        public void addProfileEmail(ProfileEmail _profileEmail)
        {
            var E = Converter.ToProfileLocalE1(_profileEmail);
            ProfilePerfiles.Add(E);
        }

        public void removeProfileEmail(ProfileEmail _profileEmail)
        {
            ProfileLocal E = Converter.ToProfileLocalE1(_profileEmail);
            ProfileLocal Aux = new ProfileLocal();
            foreach (ProfileLocal PLocal in ProfilePerfiles)
            {
                if (E.ProfileName == PLocal.ProfileName && E.value == PLocal.value)
                {
                    Aux = PLocal;
                }
            }
            ProfilePerfiles.Remove(Aux);
            var A = ProfilePerfiles.Count;
        }
        #endregion

        #region Phone
        private async Task<ObservableCollection<ProfilePhone>> GetListPhone(int _BoxId)
        {
            this.IsRunning = true;
            List<ProfilePhone> listPhone;
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
                
                var response = await this.apiService.Get(
                    apiSecurity,
                    "/api",
                    "/Box_ProfilePhone/GetBox_ProfilePhone",
                    RelationPhone);

                ItemPhone.Exist = response.IsSuccess;
                if (ItemPhone.Exist == true)
                {
                    var Phone = Converter.ToProfileLocalP1(ItemPhone);
                    ProfilePerfiles.Add(Phone);
                }
            }

            this.IsRunning = false;
            return ProfilePhone;
        }

        public void addProfilePhone(ProfilePhone _profilePhone)
        {
            var P = Converter.ToProfileLocalP1(_profilePhone);
            ProfilePerfiles.Add(P);
        }

        public void removeProfilePhone(ProfilePhone _profilePhone)
        {
            ProfileLocal P = Converter.ToProfileLocalP1(_profilePhone);
            ProfileLocal Aux = new ProfileLocal();
            foreach (ProfileLocal PLocal in ProfilePerfiles)
            {
                if (P.ProfileName == PLocal.ProfileName && P.value == PLocal.value)
                {
                    Aux = PLocal;
                }
            }
            ProfilePerfiles.Remove(Aux);
            var A = ProfilePerfiles.Count;
        }
        #endregion

        #region SM
        private async Task<ObservableCollection<ProfileSM>> GetListSM(int _BoxId)
        {
            this.IsRunning = true;
            List<ProfileSM> listSM;

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
                    var SM = Converter.ToProfileLocalSM1(ItemSM);
                    ProfilePerfiles.Add(SM);
                }
            }
            this.IsRunning = false;
            return ProfileSM;
        }

        public void addProfileSM(ProfileSM _profileSM)
        {
            var SM = Converter.ToProfileLocalSM1(_profileSM);
            ProfilePerfiles.Add(SM);
        }

        public void removeProfileSM(ProfileSM _profileSM)
        {
            ProfileLocal SM = Converter.ToProfileLocalSM1(_profileSM);
            ProfileLocal Aux = new ProfileLocal();
            foreach (ProfileLocal PLocal in ProfilePerfiles)
            {
                if (SM.ProfileName == PLocal.ProfileName && SM.value == PLocal.value)
                {
                    Aux = PLocal;
                }
            }
            ProfilePerfiles.Remove(Aux);
            var A = ProfilePerfiles.Count;
        }
        #endregion

        #region Whatsapp
        private async Task<ObservableCollection<ProfileWhatsapp>> GetListWhatsapp(int _BoxId)
        {
            this.IsRunning = true;
            List<ProfileWhatsapp> listWhatsapp;

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
                    var W = Converter.ToProfileLocalW1(ItemWhatsapp);
                    ProfilePerfiles.Add(W);
                }
            }
            this.IsRunning = false;
            return ProfileWhatsapp;
        }

        public void addProfileW(ProfileWhatsapp _profileW)
        {
            var W = Converter.ToProfileLocalW1(_profileW);
            ProfilePerfiles.Add(W);
        }

        public void removeProfileW(ProfileWhatsapp _profileW)
        {
            ProfileLocal W = Converter.ToProfileLocalW1(_profileW);
            ProfileLocal Aux = new ProfileLocal();
            foreach (ProfileLocal PLocal in ProfilePerfiles)
            {
                if (W.ProfileName == PLocal.ProfileName && W.value == PLocal.value)
                {
                    Aux = PLocal;
                }
            }
            ProfilePerfiles.Remove(Aux);
            var A = ProfilePerfiles.Count;
        }
        #endregion

        #region Box
        public async void EdithBox(Box Box)
        {
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var profile = await this.apiService.PutBox(
                apiSecurity,
                "/api",
                "/Boxes/",
                profilePhone);
        }
        #endregion

        #endregion

        #region Commads
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