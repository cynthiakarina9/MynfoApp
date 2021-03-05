namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Services;
    using Mynfo.Views;
    using Rg.Plugins.Popup.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class ProfilesBYPESMViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private int RedSocial;
        private ObservableCollection<ProfileEmail> profileEmail;
        private ObservableCollection<ProfilePhone> profilePhone;
        private ObservableCollection<ProfileWhatsapp> profileWhatsapp;
        private ObservableCollection<ProfileSM> profileSM;
        public bool emptyList;
        public Box box;
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

        public ObservableCollection<ProfileWhatsapp> ProfileWhatsapp
        {
            get { return profileWhatsapp; }
            private set
            {
                SetValue(ref profileWhatsapp, value);
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
        public Box Box
        {
            get { return box; }
            private set
            {
                SetValue(ref box, value);
            }
        }
        public ProfileEmail selectedProfileEmail { get; set; }
        public ProfilePhone selectedProfilePhone { get; set; }
        public ProfileWhatsapp selectedProfileWhatsapp { get; set; }
        public ProfileSM selectedProfileSM { get; set; }
        #endregion

        #region Constructor
        public ProfilesBYPESMViewModel(int _BoxId, string _ProfileType)
        {
            apiService = new ApiService();
            GetBox();
            EmptyList = false;
            switch (_ProfileType)
            {
                case "Email":
                    GetListEmail(_BoxId);
                    break;
                case "Facebook":
                    RedSocial = 1;
                    GetListSM(_BoxId);
                    break;
                case "Instagram":
                    RedSocial = 2;
                    GetListSM(_BoxId);
                    break;
                case "LinkedIn":
                    RedSocial = 5;
                    GetListSM(_BoxId);
                    break;
                case "Phone":
                    GetListPhone(_BoxId);
                    break;
                case "Snapchat":
                    RedSocial = 4;
                    GetListSM(_BoxId);
                    break;
                case "Spotify":
                    RedSocial = 8;
                    GetListSM(_BoxId);
                    break;
                case "TikTok":
                    RedSocial = 6;
                    GetListSM(_BoxId);
                    break;
                case "Twitch":
                    RedSocial = 9;
                    GetListSM(_BoxId);
                    break;
                case "Twitter":
                    RedSocial = 3;
                    GetListSM(_BoxId);
                    break;
                case "WebPage":
                    RedSocial = 10;
                    GetListSM(_BoxId);
                    break;
                case "Whatsapp":
                    GetListWhatsapp(_BoxId);
                    break;
                case "Youtube":
                    RedSocial = 7;
                    GetListSM(_BoxId);
                    break;
                default:
                    break;
            }
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

        public ICommand GotoAddCommand
        {
            get
            {
                return new RelayCommand(GotoAdd);
            }
        }
        private void GotoAdd()
        {
            PopupNavigation.Instance.PopAsync();
            MainViewModel.GetInstance().ListOfNetworks = new ListOfNetworksViewModel(Box.BoxId);
            PopupNavigation.Instance.PushAsync(new ListOfNetworksPage(Box.BoxId));
        }

        public ICommand GoBackCommand
        {
            get
            {
                return new RelayCommand(GoBack);
            }
        }
        private void GoBack()
        {
            PopupNavigation.Instance.PopAsync();
        }
        #endregion

        #region Methods

        #region Lista Email
        //Obtener Lista Email
        private async Task<ObservableCollection<ProfileEmail>> GetListEmail(int _BoxId)
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
                
                var response = await this.apiService.Get(
                    apiSecurity,
                    "/api",
                    "/Box_ProfileEmail/GetBox_ProfileEmail",
                    RelationEmail);

                ItemEmail.Exist = response.IsSuccess;
            }

            var ListOrderBy = listEmail.OrderBy(x => x.Name).ToList();
            foreach (ProfileEmail profEmail in ListOrderBy)
                ProfileEmail.Add(profEmail);

            if (ProfileEmail.Count == 0)
            {
                EmptyList = true;
            }
            this.IsRunning = false;
            return ProfileEmail;
        }
        //Actualizar lista Email
        public void addProfileEmail(ProfileEmail _profileEmail)
        {
            ProfileEmail.Add(_profileEmail);
            EmptyList = false;
        }

        public void removeProfileEmail()
        {
            ProfileEmail.Remove(selectedProfileEmail);
            if (ProfileEmail.Count == 0)
            {
                EmptyList = true;
            }
        }

        public void updateProfileEmail(ProfileEmail _profileEmail)
        {
            int newIndex = ProfileEmail.IndexOf(selectedProfileEmail);
            ProfileEmail.Remove(selectedProfileEmail);

            ProfileEmail.Insert(newIndex, _profileEmail);
        }
        #endregion

        #region Lista Phone
        //Obtener lista Phone
        private async Task<ObservableCollection<ProfilePhone>> GetListPhone(int _BoxId)
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
                apiSecurity = Application.Current.Resources["APISecurity"].ToString();
                var response = await this.apiService.Get(
                    apiSecurity,
                    "/api",
                    "/Box_ProfilePhone/GetBox_ProfilePhone",
                    RelationPhone);

                ItemPhone.Exist = response.IsSuccess;
            }

            var ListOrderBy = listPhone.OrderBy(x => x.Name).ToList();
            foreach (ProfilePhone profPhone in ListOrderBy)
                ProfilePhone.Add(profPhone);
            
            if (ProfilePhone.Count == 0)
            {
                EmptyList = true;
            }
            this.IsRunning = false;
            return ProfilePhone;
        }

        //Actualizar listas Phone
        public void addProfilePhone(ProfilePhone _profilePhone)
        {
            ProfilePhone.Add(_profilePhone);
            EmptyList = false;
        }

        public void removeProfilePhone()
        {
            ProfilePhone.Remove(selectedProfilePhone);
            if (ProfilePhone.Count == 0)
            {
                EmptyList = true;
            }
        }

        public void updateProfilePhone(ProfilePhone _profilePhone)
        {
            int newIndex = ProfilePhone.IndexOf(selectedProfilePhone);
            ProfilePhone.Remove(selectedProfilePhone);

            ProfilePhone.Insert(newIndex, _profilePhone);
        }
        #endregion

        #region Lista SM
        //Obtener lista SM
        private async Task<ObservableCollection<ProfileSM>> GetListSM(int _BoxId)
        {
            this.IsRunning = true;
            List<ProfileSM> listSM;

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

            ProfileSM = new ObservableCollection<ProfileSM>();
            listSM = await this.apiService.GetProfileByNetWork(
                apiSecurity,
                "/api",
                "/ProfileSMs/GetProfileByNetWorkT",
                MainViewModel.GetInstance().User.UserId,
                RedSocial);

            foreach (ProfileSM ItemSM in listSM)
            {
                Box_ProfileSM RelationSM;
                RelationSM = new Box_ProfileSM
                {
                    BoxId = _BoxId,
                    ProfileMSId = ItemSM.ProfileMSId
                };

                var response = await this.apiService.Get(
                    apiSecurity,
                    "/api",
                    "/Box_ProfileSM/GetBox_ProfileSM",
                    RelationSM);

                ItemSM.Exist = response.IsSuccess;
            }

            var ListOrderBy = listSM.OrderBy(x => x.ProfileName).ToList();
            foreach (ProfileSM profSM in ListOrderBy)
            {
                ProfileSM.Add(profSM);
            }

            if (ProfileSM.Count == 0)
            {
                EmptyList = true;
            }
            this.IsRunning = false;
            return ProfileSM;
        }

        //Actualizar listas SM
        public void addProfileSM(ProfileSM _profileSM)
        {
            ProfileSM.Add(_profileSM);
            EmptyList = false;
        }

        public void removeProfileSM()
        {
            ProfileSM.Remove(selectedProfileSM);
            if (ProfileSM.Count == 0)
            {
                EmptyList = true;
            }
        }

        public void updateProfileSM(ProfileSM _profileSM)
        {
            int newIndex = ProfileSM.IndexOf(selectedProfileSM);
            ProfileSM.Remove(selectedProfileSM);

            ProfileSM.Insert(newIndex, _profileSM);
        }
        #endregion

        #region Lista Whatsapp
        //Obtener lista Whatsapp
        private async Task<ObservableCollection<ProfileWhatsapp>> GetListWhatsapp(int _BoxId)
        {
            this.IsRunning = true;
            List<ProfileWhatsapp> listWhastapp;

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

            ProfileWhatsapp = new ObservableCollection<ProfileWhatsapp>();
            listWhastapp = await this.apiService.GetListByUser<ProfileWhatsapp>(
                apiSecurity,
                "/api",
                "/ProfileWhatsapps",
                MainViewModel.GetInstance().User.UserId);

            foreach (ProfileWhatsapp ItemWhatsapp in listWhastapp)
            {
                Box_ProfileWhatsapp RelationWhatsapp;
                RelationWhatsapp = new Box_ProfileWhatsapp
                {
                    BoxId = _BoxId,
                    ProfileWhatsappId = ItemWhatsapp.ProfileWhatsappId
                };
                apiSecurity = Application.Current.Resources["APISecurity"].ToString();
                var response = await this.apiService.Get(
                    apiSecurity,
                    "/api",
                    "/Box_ProfileWhatsapp/GetBox_ProfileWhatsapp",
                    RelationWhatsapp);

                ItemWhatsapp.Exist = response.IsSuccess;
            }

            var ListOrderBy = listWhastapp.OrderBy(x => x.Name).ToList();
            foreach (ProfileWhatsapp profWhatsapp in ListOrderBy)
                ProfileWhatsapp.Add(profWhatsapp);

            if (ProfileWhatsapp.Count == 0)
            {
                EmptyList = true;
            }
            this.IsRunning = false;
            return ProfileWhatsapp;
        }

        //Actualizar listas Whatsapp
        public void addProfileWhatsapp(ProfileWhatsapp _profileWhatsapp)
        {
            ProfileWhatsapp.Add(_profileWhatsapp);
            EmptyList = false;
        }

        public void removeProfileWhatsapp()
        {
            ProfileWhatsapp.Remove(selectedProfileWhatsapp);
            if (ProfileWhatsapp.Count == 0)
            {
                EmptyList = true;
            }
        }

        public void updateProfileWhatsapp(ProfileWhatsapp _profileWhatsapp)
        {
            int newIndex = ProfileWhatsapp.IndexOf(selectedProfileWhatsapp);
            ProfileWhatsapp.Remove(selectedProfileWhatsapp);

            ProfileWhatsapp.Insert(newIndex, _profileWhatsapp);
        }
        #endregion

        #region Box
        public async Task<Box> GetBox()
        {
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();

            Box = new Box();
            Box = await this.apiService.GetBox(
                apiSecurity,
                "/api",
                "/Boxes",
                MainViewModel.GetInstance().User.UserId);
            return Box;
        }
        #endregion

        #endregion
    }
}