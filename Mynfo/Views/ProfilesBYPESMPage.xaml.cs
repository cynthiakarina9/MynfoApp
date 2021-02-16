namespace Mynfo.Views
{
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Services;
    using Mynfo.ViewModels;
    using Rg.Plugins.Popup.Services;
    using System;
    using System.Text;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilesBYPESMPage 
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
        public ProfilePhone selectedProfilePhone { get; set; }
        public ProfileEmail selectedItemEmail { get; set; }
        public ProfileSM selectedItemSM { get; set; }
        public ProfileWhatsapp selectedItemWhatsapp { get; set; }
        #endregion

        #region Constructor
        public ProfilesBYPESMPage(int _BoxId, string _ProfileType, bool _BoxDefault, string _boxName = "")
        {
            apiService = new ApiService();
            Box = new Box();
            Box.BoxId = _BoxId;
            InitializeComponent();

            #region Variables
            int BoxId = _BoxId;
            int UserId = MainViewModel.GetInstance().User.UserId;

            #endregion

            #region Commands

            //BackDetails.Clicked += new EventHandler((sender, e) => Back_Clicked(sender, e, _BoxId, _BoxDefault, _boxName));

            GoToProfiles.Clicked += new EventHandler((sender, e) => GoToProfiles_Clicked(sender, e, _BoxId, _ProfileType, _BoxDefault));
            #endregion

            switch (_ProfileType)
            {

                case "Email":
                    ProfileListEmail.IsVisible = true;
                    ProfileListPhone.IsVisible = false;
                    ProfileListFacebook.IsVisible = false;
                    ProfileListInstagram.IsVisible = false;
                    ProfileListLinkedin.IsVisible = false;
                    ProfileListSnapchat.IsVisible = false;
                    ProfileListSpotify.IsVisible = false;
                    ProfileListTiktok.IsVisible = false;
                    ProfileListTwitch.IsVisible = false;
                    ProfileListTwitter.IsVisible = false;
                    ProfileListWebPage.IsVisible = false;
                    ProfileListWhatsapp.IsVisible = false;
                    ProfileListYoutube.IsVisible = false;
                    break;
                case "Facebook":
                    ProfileListEmail.IsVisible = false;
                    ProfileListPhone.IsVisible = false;
                    ProfileListFacebook.IsVisible = true;
                    ProfileListInstagram.IsVisible = false;
                    ProfileListLinkedin.IsVisible = false;
                    ProfileListSnapchat.IsVisible = false;
                    ProfileListSpotify.IsVisible = false;
                    ProfileListTiktok.IsVisible = false;
                    ProfileListTwitch.IsVisible = false;
                    ProfileListTwitter.IsVisible = false;
                    ProfileListWebPage.IsVisible = false;
                    ProfileListWhatsapp.IsVisible = false;
                    ProfileListYoutube.IsVisible = false;
                    break;
                case "Instagram":
                    ProfileListEmail.IsVisible = false;
                    ProfileListPhone.IsVisible = false;
                    ProfileListFacebook.IsVisible = false;
                    ProfileListInstagram.IsVisible = true;
                    ProfileListLinkedin.IsVisible = false;
                    ProfileListSnapchat.IsVisible = false;
                    ProfileListSpotify.IsVisible = false;
                    ProfileListTiktok.IsVisible = false;
                    ProfileListTwitch.IsVisible = false;
                    ProfileListTwitter.IsVisible = false;
                    ProfileListWebPage.IsVisible = false;
                    ProfileListWhatsapp.IsVisible = false;
                    ProfileListYoutube.IsVisible = false;
                    break;
                case "LinkedIn":
                    ProfileListEmail.IsVisible = false;
                    ProfileListPhone.IsVisible = false;
                    ProfileListFacebook.IsVisible = false;
                    ProfileListInstagram.IsVisible = false;
                    ProfileListLinkedin.IsVisible = true;
                    ProfileListSnapchat.IsVisible = false;
                    ProfileListSpotify.IsVisible = false;
                    ProfileListTiktok.IsVisible = false;
                    ProfileListTwitch.IsVisible = false;
                    ProfileListTwitter.IsVisible = false;
                    ProfileListWebPage.IsVisible = false;
                    ProfileListWhatsapp.IsVisible = false;
                    ProfileListYoutube.IsVisible = false;
                    break;
                case "Phone":
                    ProfileListEmail.IsVisible = false;
                    ProfileListPhone.IsVisible = true;
                    ProfileListFacebook.IsVisible = false;
                    ProfileListInstagram.IsVisible = false;
                    ProfileListLinkedin.IsVisible = false;
                    ProfileListSnapchat.IsVisible = false;
                    ProfileListSpotify.IsVisible = false;
                    ProfileListTiktok.IsVisible = false;
                    ProfileListTwitch.IsVisible = false;
                    ProfileListTwitter.IsVisible = false;
                    ProfileListWebPage.IsVisible = false;
                    ProfileListWhatsapp.IsVisible = false;
                    ProfileListYoutube.IsVisible = false;
                    break;
                case "Snapchat":
                    ProfileListEmail.IsVisible = false;
                    ProfileListPhone.IsVisible = false;
                    ProfileListFacebook.IsVisible = false;
                    ProfileListInstagram.IsVisible = false;
                    ProfileListLinkedin.IsVisible = false;
                    ProfileListSnapchat.IsVisible = true;
                    ProfileListSpotify.IsVisible = false;
                    ProfileListTiktok.IsVisible = false;
                    ProfileListTwitch.IsVisible = false;
                    ProfileListTwitter.IsVisible = false;
                    ProfileListWebPage.IsVisible = false;
                    ProfileListWhatsapp.IsVisible = false;
                    ProfileListYoutube.IsVisible = false;
                    break;
                case "Spotify":
                    ProfileListEmail.IsVisible = false;
                    ProfileListPhone.IsVisible = false;
                    ProfileListFacebook.IsVisible = false;
                    ProfileListInstagram.IsVisible = false;
                    ProfileListLinkedin.IsVisible = false;
                    ProfileListSnapchat.IsVisible = false;
                    ProfileListSpotify.IsVisible = true;
                    ProfileListTiktok.IsVisible = false;
                    ProfileListTwitch.IsVisible = false;
                    ProfileListTwitter.IsVisible = false;
                    ProfileListWebPage.IsVisible = false;
                    ProfileListWhatsapp.IsVisible = false;
                    ProfileListYoutube.IsVisible = false;
                    break;
                case "TikTok":
                    ProfileListEmail.IsVisible = false;
                    ProfileListPhone.IsVisible = false;
                    ProfileListFacebook.IsVisible = false;
                    ProfileListInstagram.IsVisible = false;
                    ProfileListLinkedin.IsVisible = false;
                    ProfileListSnapchat.IsVisible = false;
                    ProfileListSpotify.IsVisible = false;
                    ProfileListTiktok.IsVisible = true;
                    ProfileListTwitch.IsVisible = false;
                    ProfileListTwitter.IsVisible = false;
                    ProfileListWebPage.IsVisible = false;
                    ProfileListWhatsapp.IsVisible = false;
                    ProfileListYoutube.IsVisible = false;
                    break;
                case "Twitch":
                    ProfileListEmail.IsVisible = false;
                    ProfileListPhone.IsVisible = false;
                    ProfileListFacebook.IsVisible = false;
                    ProfileListInstagram.IsVisible = false;
                    ProfileListLinkedin.IsVisible = false;
                    ProfileListSnapchat.IsVisible = false;
                    ProfileListSpotify.IsVisible = false;
                    ProfileListTiktok.IsVisible = false;
                    ProfileListTwitch.IsVisible = true;
                    ProfileListTwitter.IsVisible = false;
                    ProfileListWebPage.IsVisible = false;
                    ProfileListWhatsapp.IsVisible = false;
                    ProfileListYoutube.IsVisible = false;
                    break;
                case "Twitter":
                    ProfileListEmail.IsVisible = false;
                    ProfileListPhone.IsVisible = false;
                    ProfileListFacebook.IsVisible = false;
                    ProfileListInstagram.IsVisible = false;
                    ProfileListLinkedin.IsVisible = false;
                    ProfileListSnapchat.IsVisible = false;
                    ProfileListSpotify.IsVisible = false;
                    ProfileListTiktok.IsVisible = false;
                    ProfileListTwitch.IsVisible = false;
                    ProfileListTwitter.IsVisible = true;
                    ProfileListWebPage.IsVisible = false;
                    ProfileListWhatsapp.IsVisible = false;
                    ProfileListYoutube.IsVisible = false;
                    break;
                case "WebPage":
                    ProfileListEmail.IsVisible = false;
                    ProfileListPhone.IsVisible = false;
                    ProfileListFacebook.IsVisible = false;
                    ProfileListInstagram.IsVisible = false;
                    ProfileListLinkedin.IsVisible = false;
                    ProfileListSnapchat.IsVisible = false;
                    ProfileListSpotify.IsVisible = false;
                    ProfileListTiktok.IsVisible = false;
                    ProfileListTwitch.IsVisible = false;
                    ProfileListTwitter.IsVisible = false;
                    ProfileListWebPage.IsVisible = true;
                    ProfileListWhatsapp.IsVisible = false;
                    ProfileListYoutube.IsVisible = false;
                    break;
                case "Whatsapp":
                    ProfileListEmail.IsVisible = false;
                    ProfileListPhone.IsVisible = false;
                    ProfileListFacebook.IsVisible = false;
                    ProfileListInstagram.IsVisible = false;
                    ProfileListLinkedin.IsVisible = false;
                    ProfileListSnapchat.IsVisible = false;
                    ProfileListSpotify.IsVisible = false;
                    ProfileListTiktok.IsVisible = false;
                    ProfileListTwitch.IsVisible = false;
                    ProfileListTwitter.IsVisible = false;
                    ProfileListWebPage.IsVisible = false;
                    ProfileListWhatsapp.IsVisible = true;
                    ProfileListYoutube.IsVisible = false;
                    break;
                case "Youtube":
                    ProfileListEmail.IsVisible = false;
                    ProfileListPhone.IsVisible = false;
                    ProfileListFacebook.IsVisible = false;
                    ProfileListInstagram.IsVisible = false;
                    ProfileListLinkedin.IsVisible = false;
                    ProfileListSnapchat.IsVisible = false;
                    ProfileListSpotify.IsVisible = false;
                    ProfileListTiktok.IsVisible = false;
                    ProfileListTwitch.IsVisible = false;
                    ProfileListTwitter.IsVisible = false;
                    ProfileListWebPage.IsVisible = false;
                    ProfileListWhatsapp.IsVisible = false;
                    ProfileListYoutube.IsVisible = true;
                    break;
                default:
                    break;
            }

        }
        #endregion

        #region Methods

        #region Commands
        private void GoToProfiles_Clicked(object sender, EventArgs e, int _boxId, string _profileType, bool _BoxDefault)
        {
            var mainViewModel = MainViewModel.GetInstance();
            switch (_profileType)
            {
                case "Phone":
                    mainViewModel.CreateProfilePhone = new CreateProfilePhoneViewModel();
                    PopupNavigation.Instance.PushAsync(new CreateProfilePhonePopUpPage());
                    break;
                case "Email":
                    mainViewModel.CreateProfileEmail = new CreateProfileEmailViewModel();
                    PopupNavigation.Instance.PushAsync(new CreateProfileEmailPopUpPage());
                    break;
                case "Facebook":
                    mainViewModel.CreateProfileFacebook = new CreateProfileFacebookViewModel();
                    PopupNavigation.Instance.PushAsync(new CreateProfileFacebookPopUpPage());
                    break;
                case "Instagram":
                    mainViewModel.CreateProfileInstagram = new CreateProfileInstagramViewModel();
                    PopupNavigation.Instance.PushAsync(new CreateProfileInstagramPopUpPage());
                    break;
                case "LinkedIn":
                    mainViewModel.CreateProfileLinkedin = new CreateProfileLinkedinViewModel();
                    PopupNavigation.Instance.PushAsync(new CreateProfileLinkedinPopUpPage());
                    break;
                case "Spotify":
                    mainViewModel.CreateProfileSpotify = new CreateProfileSpotifyViewModel();
                    PopupNavigation.Instance.PushAsync(new CreateProfileSpotifyPopUpPage());
                    break;
                case "Snapchat":
                    mainViewModel.CreateProfileSnapchat = new CreateProfileSnapchatViewModel();
                    PopupNavigation.Instance.PushAsync(new CreateProfileSnapchatPopUpPage());
                    break;
                case "Twitch":
                    mainViewModel.CreateProfileTwitch = new CreateProfileTwitchViewModel();
                    PopupNavigation.Instance.PushAsync(new CreateProfileTwitchPopUpPage());
                    break;
                case "TikTok":
                    mainViewModel.CreateProfileTiktok = new CreateProfileTiktokViewModel();
                    PopupNavigation.Instance.PushAsync(new CreateProfileTiktokPopUpPage());
                    break;
                case "Twitter":
                    mainViewModel.CreateProfileTwitter = new CreateProfileTwitterViewModel();
                    PopupNavigation.Instance.PushAsync(new CreateProfileTwitterPopUpPage());
                    break;
                case "WebPage":
                    mainViewModel.CreateProfileWebPage = new CreateProfileWebViewModel();
                    PopupNavigation.Instance.PushAsync(new CreateProfileWebPagePopUpPage());
                    break;
                case "Whatsapp":
                    mainViewModel.CreateProfileWhatsApp = new CreateProfileWhatsAppViewModel();
                    PopupNavigation.Instance.PushAsync(new CreateProfileWhatsAppPopUpPage());
                    break;
                case "Youtube":
                    mainViewModel.CreateProfileYoutube = new CreateProfileYoutubeViewModel();
                    PopupNavigation.Instance.PushAsync(new CreateProfileYoutubePopUpPage());
                    break;
                default:
                    break;
            }
        }

        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            selectedItemEmail = e.SelectedItem as ProfileEmail;
            selectedProfilePhone = e.SelectedItem as ProfilePhone;
            selectedItemSM = e.SelectedItem as ProfileSM;
            selectedItemWhatsapp = e.SelectedItem as ProfileWhatsapp;
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            ProfileEmail tappedItemEmail = e.Item as ProfileEmail;
            ProfilePhone tappedItemPhone = e.Item as ProfilePhone;
            ProfileSM tappedItemSM = e.Item as ProfileSM;
            ProfileWhatsapp tappedItemWhatsapp = e.Item as ProfileWhatsapp;

            if (tappedItemEmail != null)
            {
                if (tappedItemEmail.Exist == false)
                {
                    PostProfileEmail(Box.BoxId, tappedItemEmail.ProfileEmailId);
                    tappedItemEmail.Exist = true;
                    MainViewModel.GetInstance().ProfilesBYPESM.updateProfileEmail(tappedItemEmail);
                    MainViewModel.GetInstance().ListOfNetworks.updateProfileEmail(tappedItemEmail);
                    MainViewModel.GetInstance().DetailsBox.addProfileEmail(tappedItemEmail);
                }
                else
                {
                    DeleteProfileEmail(Box.BoxId, tappedItemEmail.ProfileEmailId);
                    tappedItemEmail.Exist = false;
                    MainViewModel.GetInstance().ProfilesBYPESM.updateProfileEmail(tappedItemEmail);
                    MainViewModel.GetInstance().ListOfNetworks.updateProfileEmail(tappedItemEmail);
                    MainViewModel.GetInstance().DetailsBox.removeProfileEmail(tappedItemEmail);
                }
            }

            else if (tappedItemPhone != null)
            {
                if (tappedItemPhone.Exist == false)
                {
                    PostProfilePhone(Box.BoxId, tappedItemPhone.ProfilePhoneId);
                    tappedItemPhone.Exist = true;
                    MainViewModel.GetInstance().ProfilesBYPESM.updateProfilePhone(tappedItemPhone);
                    MainViewModel.GetInstance().ListOfNetworks.updateProfilePhone(tappedItemPhone);
                    MainViewModel.GetInstance().DetailsBox.addProfilePhone(tappedItemPhone);
                }
                else
                {
                    DeleteProfilePhone(Box.BoxId, tappedItemPhone.ProfilePhoneId);
                    tappedItemPhone.Exist = false;
                    MainViewModel.GetInstance().ProfilesBYPESM.updateProfilePhone(tappedItemPhone);
                    MainViewModel.GetInstance().ListOfNetworks.updateProfilePhone(tappedItemPhone);
                    MainViewModel.GetInstance().DetailsBox.removeProfilePhone(tappedItemPhone);
                }
            }

            else if (tappedItemSM != null)
            {
                if (tappedItemSM.Exist == false)
                {
                    PostProfileSM(Box.BoxId, tappedItemSM.ProfileMSId);
                    tappedItemSM.Exist = true;
                    MainViewModel.GetInstance().ProfilesBYPESM.updateProfileSM(tappedItemSM);
                    MainViewModel.GetInstance().ListOfNetworks.updateProfileSM(tappedItemSM);
                    MainViewModel.GetInstance().DetailsBox.addProfileSM(tappedItemSM);
                }
                else
                {
                    DeleteProfileSM(Box.BoxId, tappedItemSM.ProfileMSId);
                    tappedItemSM.Exist = false;
                    MainViewModel.GetInstance().ProfilesBYPESM.updateProfileSM(tappedItemSM);
                    MainViewModel.GetInstance().ListOfNetworks.updateProfileSM(tappedItemSM);
                    MainViewModel.GetInstance().DetailsBox.removeProfileSM(tappedItemSM);
                }
            }

            else if (tappedItemWhatsapp != null)
            {
                if (tappedItemWhatsapp.Exist == false)
                {
                    PostProfileWhatsapp(Box.BoxId, tappedItemWhatsapp.ProfileWhatsappId);
                    tappedItemWhatsapp.Exist = true;
                    MainViewModel.GetInstance().ProfilesBYPESM.updateProfileWhatsapp(tappedItemWhatsapp);
                    MainViewModel.GetInstance().ListOfNetworks.updateProfileWhatsapp(tappedItemWhatsapp);
                    MainViewModel.GetInstance().DetailsBox.addProfileW(tappedItemWhatsapp);
                }
                else
                {
                    DeleteProfileWhatsapp(Box.BoxId, tappedItemWhatsapp.ProfileWhatsappId);
                    tappedItemWhatsapp.Exist = false;
                    MainViewModel.GetInstance().ProfilesBYPESM.updateProfileWhatsapp(tappedItemWhatsapp);
                    MainViewModel.GetInstance().ListOfNetworks.updateProfileWhatsapp(tappedItemWhatsapp);
                    MainViewModel.GetInstance().DetailsBox.removeProfileW(tappedItemWhatsapp);
                }
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