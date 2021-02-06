namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Services;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Rg.Plugins.Popup.Extensions;
    using Views;
    using Xamarin.Forms;
    using Rg.Plugins.Popup.Services;
    using Mynfo.Models;
    using System.Collections.Generic;

    public class HomeViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        private ProfileLocal profile;
        private ObservableCollection<Box> box;
        private ObservableCollection<Box> boxNoDefault;
        private bool isRunning;
        private bool isNull;
        private bool moreOne;
        private ObservableCollection<ProfileEmail> profileEmail;
        private ObservableCollection<ProfilePhone> profilePhone;
        private ObservableCollection<ProfileSM> profileSM;
        private ObservableCollection<ProfileWhatsapp> profileWhatsapp;
        private ObservableCollection<ProfileLocal> profilePerfiles;
        #endregion

        #region Properties
        public ObservableCollection<Box> Box
        {
            get { return this.box; }
            set { SetValue(ref this.box, value); }
        }
        public ObservableCollection<Box> BoxNoDefault
        {
            get { return this.boxNoDefault; }
            set { SetValue(ref this.boxNoDefault, value); }
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
        public bool IsNull
        {
            get
            {
                return this.isNull;
            }
            set
            {
                SetValue(ref this.isNull, value);
            }
        }
        public ProfileLocal Profile
        {
            get
            {
                return this.profile;
            }
            set
            {
                SetValue(ref this.profile, value);
            }
        }
        public bool MoreOne
        {
            get
            {
                return this.moreOne;
            }
            set
            {
                SetValue(ref this.moreOne, value);
            }
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
        #endregion

        #region Contructor
        public HomeViewModel()
        {
            apiService = new ApiService();
            this.IsRunning = false;
            
            ProfilePerfiles = new ObservableCollection<ProfileLocal>();
            GetBoxDefault();
            GetBoxNoDefault();
            //GetProfiles();
        }
        #endregion

        #region Methods
        public async Task<ObservableCollection<Box>> GetBoxDefault()
        {
            this.IsNull = false;
            Box = new ObservableCollection<Box>();
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

            var BoxList = await this.apiService.GetBoxDefault<Box>(
                apiSecurity,
                "/api",
                "/Boxes/GetBoxDefault",
                MainViewModel.GetInstance().User.UserId);

            if(BoxList != null)
            {
                Box.Add(BoxList); 
            }
            
            if(Box.Count != 0)
            {
                IsNull = true;
            }
            this.IsRunning = false;
            return Box;
        }

        public async Task<ObservableCollection<Box>> GetBoxNoDefault()
        {
            this.MoreOne = false;
            BoxNoDefault = new ObservableCollection<Box>();
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

            var BoxListNoDefault = await this.apiService.GetBoxNoDefault<Box>(
                apiSecurity,
                "/api",
                "/Boxes/GetBoxNoDefault",
                MainViewModel.GetInstance().User.UserId);
            foreach(Box boxes in BoxListNoDefault)
            {
                BoxNoDefault.Add(boxes);
            }

            if (BoxListNoDefault.Count != 0)
            {
                MoreOne = true;
            }
            this.IsRunning = false;
            return Box;
        }

        public async void GetProfiles()
        {
            
            if (Box.Count != 0 && BoxNoDefault.Count == 0)
            {
                int BoxId = 0;
                foreach (Box b in Box)
                {
                    BoxId = b.BoxId;
                }
                GetListEmail(BoxId);
                GetListPhone(BoxId);
                GetListSM(BoxId);
                GetListWhatsapp(BoxId);
            }
            if (BoxNoDefault.Count != 0)
            {
                int BoxId = 0;
                int BoxId2 = 0;
                foreach (Box b in Box)
                {
                    BoxId2 = b.BoxId;
                    GetListEmail(BoxId2);
                    GetListPhone(BoxId2);
                    GetListSM(BoxId2);
                    GetListWhatsapp(BoxId2);
                }
                foreach (Box b in Box)
                {
                    BoxId = b.BoxId;
                    GetListEmail(BoxId);
                    GetListPhone(BoxId);
                    GetListSM(BoxId);
                    GetListWhatsapp(BoxId);
                }
            }
        }

        #region Listas
        public void AddList(Box _Boxes)
        {
            BoxNoDefault.Add(_Boxes);
        }

        public void RemoveList(Box _Boxes)
        {
            BoxNoDefault.Remove(_Boxes);
        }

        public void UpdateList(Box _Boxes)
        {
            Box Aux = new Box();
            foreach (Box B in BoxNoDefault)
            {
                if (_Boxes.BoxId == B.BoxId)
                {
                    Aux = B;
                }
            }
            int newIndex = BoxNoDefault.IndexOf(Aux);
            BoxNoDefault.Remove(Aux);

            BoxNoDefault.Insert(newIndex, _Boxes);
        }
        #endregion

        public async void GoToDetailsNoDefault()
        {
            int BoxId = 0;
            Box _Box = new Box();
            foreach (Box boxCount in BoxNoDefault)
            {
                BoxId = boxCount.BoxId;
                _Box = boxCount;
            }
            MainViewModel.GetInstance().DetailsBox = new DetailsBoxViewModel(_Box);
            //await App.Navigator.PushAsync(new DetailsBoxPage(_Box));
            await PopupNavigation.Instance.PushAsync(new DetailBoxPopUpPage(_Box));
        }

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
                    var Email = Converter.ToProfileLocalE(ItemEmail);
                    ProfilePerfiles.Add(Email);
                }
            }
            this.IsRunning = false;
            return ProfileEmail;
        }
        public void addProfileEmail(ProfileEmail _profileEmail)
        {
            var E = Converter.ToProfileLocalE(_profileEmail);
            ProfilePerfiles.Add(E);
        }

        public void removeProfileEmail(ProfileEmail _profileEmail)
        {
            ProfileLocal E = Converter.ToProfileLocalE(_profileEmail);
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
                    var Phone = Converter.ToProfileLocalP(ItemPhone);
                    ProfilePerfiles.Add(Phone);
                }
            }
            this.IsRunning = false;
            return ProfilePhone;
        }

        public void addProfilePhone(ProfilePhone _profilePhone)
        {
            var P = Converter.ToProfileLocalP(_profilePhone);
            ProfilePerfiles.Add(P);
        }

        public void removeProfilePhone(ProfilePhone _profilePhone)
        {
            ProfileLocal P = Converter.ToProfileLocalP(_profilePhone);
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
                    var SM = Converter.ToProfileLocalSM(ItemSM);
                    ProfilePerfiles.Add(SM);
                }
            }
            this.IsRunning = false;
            return ProfileSM;
        }

        public void addProfileSM(ProfileSM _profileSM)
        {
            var SM = Converter.ToProfileLocalSM(_profileSM);
            ProfilePerfiles.Add(SM);
        }

        public void removeProfileSM(ProfileSM _profileSM)
        {
            ProfileLocal SM = Converter.ToProfileLocalSM(_profileSM);
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
                    var W = Converter.ToProfileLocalW(ItemWhatsapp);
                    ProfilePerfiles.Add(W);
                }
            }
            this.IsRunning = false;
            return ProfileWhatsapp;
        }

        public void addProfileW(ProfileWhatsapp _profileW)
        {
            var W = Converter.ToProfileLocalW(_profileW);
            ProfilePerfiles.Add(W);
        }

        public void removeProfileW(ProfileWhatsapp _profileW)
        {
            ProfileLocal W = Converter.ToProfileLocalW(_profileW);
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

        #endregion

        #region Commands
        public ICommand GoToDetailsCommand
        {
            get
            {
                return new RelayCommand(GoToDetails);
            }
        }
        public async void GoToDetails ()
        {
            int BoxId = 0;
            int BoxId2 = 0;
            Box _Box = new Box();
            foreach(Box boxCount in Box)
            {
                BoxId = boxCount.BoxId;
                _Box = boxCount;
            }
            foreach(Box BoxCountNoDefault in BoxNoDefault)
            {
                BoxId2 = BoxCountNoDefault.BoxId;
            }
            MainViewModel.GetInstance().DetailsBox = new DetailsBoxViewModel(_Box);
            //await App.Navigator.PushAsync(new DetailsBoxPage(_Box));
            await PopupNavigation.Instance.PushAsync(new DetailBoxPopUpPage(_Box));
        }
        #endregion
    }
}
