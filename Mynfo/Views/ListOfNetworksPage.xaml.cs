namespace Mynfo.Views
{
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Models;
    using Mynfo.Services;
    using Mynfo.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListOfNetworksPage 
    {
        #region Attributes
        public bool Actived;
        public bool isCheck;
        #endregion

        #region Services
        ApiService apiService;
        #endregion

        #region Properties
        public Box Box { get; set; }
        public Box Box2 { get; set; }
        public ProfileLocal selectedItemProfile { get; set; }
        #endregion

        #region Constructor
        public ListOfNetworksPage(int _BoxId)
        {
            InitializeComponent();
            apiService = new ApiService();
            Box = new Box();
            Box.BoxId = _BoxId;
            NavigationPage.SetHasNavigationBar(this, false);
            OSAppTheme currentTheme = Application.Current.RequestedTheme;
            BackgroundFull.CloseWhenBackgroundIsClicked = true;
        }
        #endregion

        #region Methods

        #region Commands
        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            selectedItemProfile = e.SelectedItem as ProfileLocal;
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            ProfileLocal tappedItemProfile = e.Item as ProfileLocal;

            switch(tappedItemProfile.Logo)
            {
                case "mail2":
                    ProfileEmail E = Converter.ToProfileEmail(tappedItemProfile);
                    if (E.Exist == false)
                    {
                        PostProfileEmail(Box.BoxId, E.ProfileEmailId);
                        E.Exist = true;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileEmail(E);
                        MainViewModel.GetInstance().DetailsBox.addProfileEmail(E);
                    }
                    else
                    {
                        DeleteProfileEmail(Box.BoxId, E.ProfileEmailId);
                        E.Exist = false;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileEmail(E);
                        MainViewModel.GetInstance().DetailsBox.removeProfileEmail(E);
                    }
                    break;
                case "tel2":
                    ProfilePhone P = Converter.ToProfilePhone(tappedItemProfile);
                    if (P.Exist == false)
                        {
                            PostProfilePhone(Box.BoxId, P.ProfilePhoneId);
                            P.Exist = true;
                            MainViewModel.GetInstance().ListOfNetworks.updateProfilePhone(P);
                            MainViewModel.GetInstance().DetailsBox.addProfilePhone(P);
                        }
                        else
                        {
                            DeleteProfilePhone(Box.BoxId, P.ProfilePhoneId);
                            P.Exist = false;
                            MainViewModel.GetInstance().ListOfNetworks.updateProfilePhone(P);
                            MainViewModel.GetInstance().DetailsBox.removeProfilePhone(P);
                        }
                    break;
                case "facebook2":
                    ProfileSM SMfb = Converter.ToProfileSM(tappedItemProfile);
                    if (SMfb.Exist == false)
                    {
                        PostProfileSM(Box.BoxId, SMfb.ProfileMSId);
                        SMfb.Exist = true;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileSM(SMfb);
                        MainViewModel.GetInstance().DetailsBox.addProfileSM(SMfb);
                    }
                    else
                    {
                        DeleteProfileSM(Box.BoxId, SMfb.ProfileMSId);
                        SMfb.Exist = false;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileSM(SMfb);
                        MainViewModel.GetInstance().DetailsBox.removeProfileSM(SMfb);
                    }
                break;
                case "twitterlogo2":
                    ProfileSM SMtwtt = Converter.ToProfileSM(tappedItemProfile);
                    if (SMtwtt.Exist == false)
                    {
                        PostProfileSM(Box.BoxId, SMtwtt.ProfileMSId);
                        SMtwtt.Exist = true;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileSM(SMtwtt);
                        MainViewModel.GetInstance().DetailsBox.addProfileSM(SMtwtt);
                    }
                    else
                    {
                        DeleteProfileSM(Box.BoxId, SMtwtt.ProfileMSId);
                        SMtwtt.Exist = false;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileSM(SMtwtt);
                        MainViewModel.GetInstance().DetailsBox.removeProfileSM(SMtwtt);
                    }
                    break;
                case "instagramlogo2":
                    ProfileSM SMIns = Converter.ToProfileSM(tappedItemProfile);
                    if (SMIns.Exist == false)
                    {
                        PostProfileSM(Box.BoxId, SMIns.ProfileMSId);
                        SMIns.Exist = true;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileSM(SMIns);
                        MainViewModel.GetInstance().DetailsBox.addProfileSM(SMIns);
                    }
                    else
                    {
                        DeleteProfileSM(Box.BoxId, SMIns.ProfileMSId);
                        SMIns.Exist = false;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileSM(SMIns);
                        MainViewModel.GetInstance().DetailsBox.removeProfileSM(SMIns);
                    }
                    break;
                case "snapchat2":
                    ProfileSM SMSnap = Converter.ToProfileSM(tappedItemProfile);
                    if (SMSnap.Exist == false)
                    {
                        PostProfileSM(Box.BoxId, SMSnap.ProfileMSId);
                        SMSnap.Exist = true;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileSM(SMSnap);
                        MainViewModel.GetInstance().DetailsBox.addProfileSM(SMSnap);
                    }
                    else
                    {
                        DeleteProfileSM(Box.BoxId, SMSnap.ProfileMSId);
                        SMSnap.Exist = false;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileSM(SMSnap);
                        MainViewModel.GetInstance().DetailsBox.removeProfileSM(SMSnap);
                    }
                    break;
                case "linkedin2":
                    ProfileSM SMSLink = Converter.ToProfileSM(tappedItemProfile);
                    if (SMSLink.Exist == false)
                    {
                        PostProfileSM(Box.BoxId, SMSLink.ProfileMSId);
                        SMSLink.Exist = true;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileSM(SMSLink);
                        MainViewModel.GetInstance().DetailsBox.addProfileSM(SMSLink);
                    }
                    else
                    {
                        DeleteProfileSM(Box.BoxId, SMSLink.ProfileMSId);
                        SMSLink.Exist = false;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileSM(SMSLink);
                        MainViewModel.GetInstance().DetailsBox.removeProfileSM(SMSLink);
                    }
                    break;
                case "tiktok2":
                    ProfileSM SMSTik = Converter.ToProfileSM(tappedItemProfile);
                    if (SMSTik.Exist == false)
                    {
                        PostProfileSM(Box.BoxId, SMSTik.ProfileMSId);
                        SMSTik.Exist = true;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileSM(SMSTik);
                        MainViewModel.GetInstance().DetailsBox.addProfileSM(SMSTik);
                    }
                    else
                    {
                        DeleteProfileSM(Box.BoxId, SMSTik.ProfileMSId);
                        SMSTik.Exist = false;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileSM(SMSTik);
                        MainViewModel.GetInstance().DetailsBox.removeProfileSM(SMSTik);
                    }
                    break;
                case "youtube2":
                    ProfileSM SMYou = Converter.ToProfileSM(tappedItemProfile);
                    if (SMYou.Exist == false)
                    {
                        PostProfileSM(Box.BoxId, SMYou.ProfileMSId);
                        SMYou.Exist = true;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileSM(SMYou);
                        MainViewModel.GetInstance().DetailsBox.addProfileSM(SMYou);
                    }
                    else
                    {
                        DeleteProfileSM(Box.BoxId, SMYou.ProfileMSId);
                        SMYou.Exist = false;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileSM(SMYou);
                        MainViewModel.GetInstance().DetailsBox.removeProfileSM(SMYou);
                    }
                    break;
                case "spotify2":
                    ProfileSM SMSP = Converter.ToProfileSM(tappedItemProfile);
                    if (SMSP.Exist == false)
                    {
                        PostProfileSM(Box.BoxId, SMSP.ProfileMSId);
                        SMSP.Exist = true;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileSM(SMSP);
                        MainViewModel.GetInstance().DetailsBox.addProfileSM(SMSP);
                    }
                    else
                    {
                        DeleteProfileSM(Box.BoxId, SMSP.ProfileMSId);
                        SMSP.Exist = false;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileSM(SMSP);
                        MainViewModel.GetInstance().DetailsBox.removeProfileSM(SMSP);
                    }
                    break;
                case "twitch2":
                    ProfileSM SMTwc = Converter.ToProfileSM(tappedItemProfile);
                    if (SMTwc.Exist == false)
                    {
                        PostProfileSM(Box.BoxId, SMTwc.ProfileMSId);
                        SMTwc.Exist = true;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileSM(SMTwc);
                        MainViewModel.GetInstance().DetailsBox.addProfileSM(SMTwc);
                    }
                    else
                    {
                        DeleteProfileSM(Box.BoxId, SMTwc.ProfileMSId);
                        SMTwc.Exist = false;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileSM(SMTwc);
                        MainViewModel.GetInstance().DetailsBox.removeProfileSM(SMTwc);
                    }
                    break;
                case "gmail2":
                    ProfileSM SMWeb = Converter.ToProfileSM(tappedItemProfile);
                    if (SMWeb.Exist == false)
                    {
                        PostProfileSM(Box.BoxId, SMWeb.ProfileMSId);
                        SMWeb.Exist = true;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileSM(SMWeb);
                        MainViewModel.GetInstance().DetailsBox.addProfileSM(SMWeb);
                    }
                    else
                    {
                        DeleteProfileSM(Box.BoxId, SMWeb.ProfileMSId);
                        SMWeb.Exist = false;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileSM(SMWeb);
                        MainViewModel.GetInstance().DetailsBox.removeProfileSM(SMWeb);
                    }
                    break;
                case "whatsapp2":
                    ProfileWhatsapp W  = Converter.ToProfileWhatsapp(tappedItemProfile);
                    if (W.Exist == false)
                    {
                        PostProfileWhatsapp(Box.BoxId, W.ProfileWhatsappId);
                        W.Exist = true;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileWhatsapp(W);
                        MainViewModel.GetInstance().DetailsBox.addProfileW(W);
                    }
                    else
                    {
                        DeleteProfileWhatsapp(Box.BoxId, W.ProfileWhatsappId);
                        W.Exist = false;
                        MainViewModel.GetInstance().ListOfNetworks.updateProfileWhatsapp(W);
                        MainViewModel.GetInstance().DetailsBox.removeProfileW(W);
                    }
                    break;
                default:
                    break;
            }
        }

        
        #endregion

        #region Email
        public async void DeleteProfileEmail(int _box, int _profileEmailId)
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                await App.Navigator.PopAsync();
            }

            Box_ProfileEmail box_ProfileEmail = new Box_ProfileEmail
            {
                BoxId = _box,
                ProfileEmailId = _profileEmailId
            };
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var idBox_Email = await this.apiService.GetIdRelation(
                apiSecurity,
                "/api",
                "/Box_ProfileEmail/GetBox_ProfileEmail",
                box_ProfileEmail);

            var profileEmail = await this.apiService.Delete(
                apiSecurity,
                "/api",
                "/Box_ProfileEmail",
                idBox_Email.Box_ProfileEmailId);
        }

        public async void PostProfileEmail(int _box, int _profileEmailId)
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                await App.Navigator.PopAsync();
            }

            Box_ProfileEmail box_ProfileEmail = new Box_ProfileEmail
            {
                BoxId = _box,
                ProfileEmailId = _profileEmailId
            };
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var profileEmail = await this.apiService.Post2(
                apiSecurity,
                "/api",
                "/Box_ProfileEmail",
                box_ProfileEmail);
        }
        #endregion

        #region Phone
        public async void PostProfilePhone(int _box, int _profilePhoneId)
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                await App.Navigator.PopAsync();
            }

            Box_ProfilePhone box_ProfilePhone = new Box_ProfilePhone
            {
                BoxId = _box,
                ProfilePhoneId = _profilePhoneId
            };
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var profilePhone = await this.apiService.Post2(
                apiSecurity,
                "/api",
                "/Box_ProfilePhone",
                box_ProfilePhone);
        }
        public async void DeleteProfilePhone(int _box, int _profilePhoneId)
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                await App.Navigator.PopAsync();
            }

            Box_ProfilePhone box_ProfilePhone = new Box_ProfilePhone
            {
                BoxId = _box,
                ProfilePhoneId = _profilePhoneId
            };
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var idBox_Phone = await this.apiService.GetIdRelation(
                apiSecurity,
                "/api",
                "/Box_ProfilePhone/GetBox_ProfilePhone",
                box_ProfilePhone);

            var profilePhone = await this.apiService.Delete(
                apiSecurity,
                "/api",
                "/Box_ProfilePhone",
                idBox_Phone.Box_ProfilePhoneId);
        }
        #endregion

        #region SM
        public async void PostProfileSM(int _box, int _profileSMId)
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                await App.Navigator.PopAsync();
            }

            Box_ProfileSM box_ProfileSM = new Box_ProfileSM
            {
                BoxId = _box,
                ProfileMSId = _profileSMId
            };
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var profileSM = await this.apiService.Post2(
                apiSecurity,
                "/api",
                "/Box_ProfileSM",
                box_ProfileSM);

        }
        public async void DeleteProfileSM(int _box, int _profileSMId)
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                await App.Navigator.PopAsync();
            }

            Box_ProfileSM box_ProfileSM = new Box_ProfileSM
            {
                BoxId = _box,
                ProfileMSId = _profileSMId
            };
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var idBox_SM = await this.apiService.GetIdRelation(
                apiSecurity,
                "/api",
                "/Box_ProfileSM/GetBox_ProfileSM",
                box_ProfileSM);

            var profileSM = await this.apiService.Delete(
                apiSecurity,
                "/api",
                "/Box_ProfileSM",
                idBox_SM.Box_ProfileSMId);
        }
        #endregion

        #region Whatsapp
        public async void PostProfileWhatsapp(int _box, int _profileWhatsappId)
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                await App.Navigator.PopAsync();
            }

            Box_ProfileWhatsapp box_ProfileWhatsapp = new Box_ProfileWhatsapp
            {
                BoxId = _box,
                ProfileWhatsappId = _profileWhatsappId
            };
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var profileSM = await this.apiService.Post2(
                apiSecurity,
                "/api",
                "/Box_ProfileWhatsapp",
                box_ProfileWhatsapp);
        }
        public async void DeleteProfileWhatsapp(int _box, int _profileWhatsappId)
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                await App.Navigator.PopAsync();
            }

            Box_ProfileWhatsapp box_ProfileWhatsapp = new Box_ProfileWhatsapp
            {
                BoxId = _box,
                ProfileWhatsappId = _profileWhatsappId
            };
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var idBox_Whatsapp = await this.apiService.GetIdRelation(
                apiSecurity,
                "/api",
                "/Box_ProfileWhatsapp/GetBox_ProfileWhatsapp",
                box_ProfileWhatsapp);

            var profileWhatsapp = await this.apiService.Delete(
                apiSecurity,
                "/api",
                "/Box_ProfileWhatsapp",
                idBox_Whatsapp.Box_ProfileWhatsappId);
        }
        #endregion

        #endregion
    }
}