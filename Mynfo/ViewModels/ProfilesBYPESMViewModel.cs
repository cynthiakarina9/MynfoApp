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

    public class ProfilesBYPESMViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        //private bool isCheck;
        private ObservableCollection<ProfileEmail> profileEmail;
        private ObservableCollection<ProfilePhone> profilePhone;
        private ObservableCollection<ProfileWhatsapp> profileWhatsapp;
        private ObservableCollection<ProfileSM> profileSM;
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


        public ProfileEmail selectedProfileEmail { get; set; }
        public ProfilePhone selectedProfilePhone { get; set; }
        public ProfileWhatsapp selectedProfileWhatsapp { get; set; }
        public ProfileSM selectedProfileSM { get; set; }
        #endregion

        #region Cosntructor
        public ProfilesBYPESMViewModel(int _BoxId, string _ProfileType, bool _BoxDefault, string _boxName = "")
        {
            apiService = new ApiService();
            switch(_ProfileType)
            {
                case "Email":
                    GetListEmail(_BoxId);
                    break;
                case "Phone":
                    GetListPhone(_BoxId);
                    break;
                case "Facebook":
                    GetListSM(_BoxId);
                    break;
                case "Whatsapp":
                    GetListWhatsapp(_BoxId);
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

            if (listEmail.Count == 0)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Warning,
                    "Aun no ha creado ningún perfil de este tipo",
                    Languages.Accept);
                return null;
            }

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

            foreach (ProfileEmail profEmail in listEmail)
                ProfileEmail.Add(profEmail);

            this.IsRunning = false;
            return ProfileEmail;
        }
        //Actualizar lista Email
        public void addProfileEmail(ProfileEmail _profileEmail)
        {
            ProfileEmail.Add(_profileEmail);
        }

        public void removeProfileEmail()
        {
            ProfileEmail.Remove(selectedProfileEmail);
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

            if(listPhone.Count == 0)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Warning,
                    "Aun no ha creado ningún perfil de este tipo",
                    Languages.Accept);
                return null;
            }

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

            foreach (ProfilePhone profPhone in listPhone)
                ProfilePhone.Add(profPhone);

            this.IsRunning = false;
            return ProfilePhone;
        }

        //Actualizar listas Phone
        public void addProfilePhone(ProfilePhone _profilePhone)
        {
            ProfilePhone.Add(_profilePhone);
        }

        public void removeProfilePhone()
        {
            ProfilePhone.Remove(selectedProfilePhone);
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
            listSM = await this.apiService.GetListByUser<ProfileSM>(
                apiSecurity,
                "/api",
                "/ProfileSMs",
                MainViewModel.GetInstance().User.UserId);

            if (listSM.Count == 0)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Warning,
                    "Aun no ha creado ningún perfil de este tipo",
                    Languages.Accept);
                return null;
            }

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

            foreach (ProfileSM profSM in listSM)
                ProfileSM.Add(profSM);

            this.IsRunning = false;
            return ProfileSM;
        }

        //Actualizar listas SM
        public void addProfileSM(ProfileSM _profileSM)
        {
            ProfileSM.Add(_profileSM);
        }

        public void removeProfileSM()
        {
            ProfileSM.Remove(selectedProfileSM);
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

            if (listWhastapp.Count == 0)
            {
                this.IsRunning = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Warning,
                    "Aun no ha creado ningún perfil de este tipo",
                    Languages.Accept);
                return null;
            }

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

            foreach (ProfileWhatsapp profWhatsapp in listWhastapp)
                ProfileWhatsapp.Add(profWhatsapp);

            this.IsRunning = false;
            return ProfileWhatsapp;
        }

        //Actualizar listas Whatsapp
        public void addProfileWhatsapp(ProfileWhatsapp _profileWhatsapp)
        {
            ProfileWhatsapp.Add(_profileWhatsapp);
        }

        public void removeProfileWhatsapp()
        {
            ProfileWhatsapp.Remove(selectedProfileWhatsapp);
        }

        public void updateProfileWhatsapp(ProfileWhatsapp _profileWhatsapp)
        {
            int newIndex = ProfileWhatsapp.IndexOf(selectedProfileWhatsapp);
            ProfileWhatsapp.Remove(selectedProfileWhatsapp);

            ProfileWhatsapp.Insert(newIndex, _profileWhatsapp);
        }
        #endregion

        #endregion
    }
}