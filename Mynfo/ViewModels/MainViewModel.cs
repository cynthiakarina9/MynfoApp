namespace Mynfo.ViewModels
{
    using Models;
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using System.Collections.ObjectModel;

    public class MainViewModel : BaseViewModel
    {
        #region Attributes
        private UserLocal user;
        private BoxLocal box;
        private ProfileEmail profileEmail;
        #endregion

        #region Properties
        public TokenResponse Token
        {
            get;
            set;
        }

        public Data_ntc data_nfc
        {
            get;
            set;
        }

        public ObservableCollection<MenuItemViewModel> Menus
        {
            get;
            set;
        }

        public UserLocal User
        {
            get { return this.user; }
            set { SetValue(ref this.user, value); }
        }

        public BoxLocal Box
        {
            get { return this.box; }
            set { SetValue(ref this.box, value); }
        }
        #endregion

        #region ViewModels
        public AddProfilesViewModel AddProfiles
        {
            get;
            set;
        }
        public BoxRegisterViewModel BoxRegister
        {
            get;
            set;
        }

        public ChangePasswordViewModel ChangePassword
        {
            get;
            set;
        }
        public CreateProfileEmailViewModel CreateProfileEmail
        {
            get;
            set;
        }

        public CreateProfileFacebookViewModel CreateProfileFacebook
        {
            get;
            set;
        }

        public CreateProfileInstagramViewModel CreateProfileInstagram
        {
            get;
            set;
        }

        public CreateProfileLinkedinViewModel CreateProfileLinkedin
        {
            get;
            set;
        }

        public CreateProfilePhoneViewModel CreateProfilePhone
        {
            get;
            set;
        }

        public CreateProfileSnapchatViewModel CreateProfileSnapchat
        {
            get;
            set;
        }

        public CreateProfileSpotifyViewModel CreateProfileSpotify
        {
            get;
            set;
        }

        public CreateProfileTiktokViewModel CreateProfileTiktok
        {
            get;
            set;
        }

        public CreateProfileTwitchViewModel CreateProfileTwitch
        {
            get;
            set;
        }

        public CreateProfileTwitterViewModel CreateProfileTwitter
        {
            get;
            set;
        }

        public CreateProfileWebViewModel CreateProfileWebPage
        {
            get;
            set;
        }

        public CreateProfileWhatsAppViewModel CreateProfileWhatsApp
        {
            get;
            set;
        }

        public CreateProfileYoutubeViewModel CreateProfileYoutube
        {
            get;
            set;
        }

        public DetailsBoxViewModel DetailsBox
        {
            get;
            set;
        }

        public DetailsBoxEdithViewModel DetailsBoxEdith
        {
            get; 
            set;
        }
        public EditProfileFacebookViewModel EditProfileFacebook
        {
            get;
            set;
        }

        public EditProfileInstagramViewModel EditProfileInstagram
        {
            get;
            set;
        }

        public EditProfileLinkedinViewModel EditProfileLinkedin
        {
            get;
            set;
        }

        public EditProfilePhoneViewModel EditProfilePhone
        {
            get;
            set;
        }

        public EditProfileSnapchatViewModel EditProfileSnapchat
        {
            get;
            set;
        }

        public EditProfileSpotifyViewModel EditProfileSpotify
        {
            get;
            set;
        }

        public EditProfileTiktokViewModel EditProfileTiktok
        {
            get;
            set;
        }

        public EditProfileTwitchViewModel EditProfileTwitch
        {
            get;
            set;
        }

        public EditProfileTwitterViewModel EditProfileTwitter
        {
            get;
            set;
        }

        public EditProfileWebPageViewModel EditProfileWebPage
        {
            get;
            set;
        }

        public EditProfileWhatsAppViewModel EditProfileWhatsApp
        {
            get;
            set;
        }

        public EditProfileYoutubeViewModel EditProfileYoutube
        {
            get;
            set;
        }

        public EditProfileEmailViewModel EditProfileEmail
        {
            get;
            set;
        }

        public ForeingBoxViewModel ForeingBox
        { 
            get; 
            set; 
        }

        public HomeViewModel Home
        {
            get;
            set;
        }

        public LectorQRViewModel LectorQR
        {
            get;
            set;
        }
        public ListForeignBoxViewModel ListForeignBox
        {
            get;
            set;
        }
        public ListOfNetworksViewModel ListOfNetworks
        {
            get;
            set;
        }
        public LoginViewModel Login
        {
            get;
            set;
        }

        public MyProfileViewModel MyProfile
        { 
            get; 
            set; 
        }

        public MyQRViewModel MyQR
        {
            get;
            set;
        }
        public PasswordRecoveryViewModel PasswordRecovery 
        { 
            get; 
            set; 
        }
        public ProfilesViewModel Profiles
        {
            get;
            set;
        }
        public ProfilesByEmailViewModel ProfilesByEmail
        {
            get;
            set;
        }

        public ProfilesByInstagramViewModel ProfilesByInstagram
        {
            get;
            set;
        }

        public ProfilesByPhoneViewModel ProfilesByPhone
        {
            get;
            set;
        }
        public ProfilesBYPESMViewModel ProfilesBYPESM
        {
            get;
            set;
        }
        public ProfilesByFacebookViewModel ProfilesByFacebook
        {
            get;
            set;
        }
        public ProfilesByLinkedinViewModel ProfilesByLinkedin
        {
            get;
            set;
        }

        public ProfilesBySnapchatViewModel ProfilesBySnapchat
        {
            get;
            set;
        }

        public ProfilesBySpotifyViewModel ProfilesBySpotify
        {
            get;
            set;
        }

        public ProfilesByTiktokViewModel ProfilesByTiktok
        {
            get;
            set;
        }

        public ProfilesByTwitchViewModel ProfilesByTwitch
        {
            get;
            set;
        }

        public ProfilesByTwitterViewModel ProfilesByTwitter
        {
            get;
            set;
        }

        public ProfilesByWebPageViewModel ProfilesByWebPage
        {
            get;
            set;
        }

        public ProfilesByWhatsAppViewModel ProfilesByWhatsApp
        {
            get;
            set;
        }

        public ProfilesByYoutubeViewModel ProfilesByYoutube
        {
            get;
            set;
        }

        public ProfileTypeSelectionViewModel ProfileTypeSelection
        {
            get;
            set;
        }

        public RegisterViewModel Register
        {
            get;
            set;
        }

        public Register2ViewModel Register2
        {
            get;
            set;
        }

        public SettingsViewModel Settings
        {
            get;
            set;
        }
        public TestCViewModel TestC
        {
            get;
            set;
        }

        public TestingViewModel Testing
        {
            get;
            set;
        }

        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
            this.LoadMenu();
        }
        #endregion

        #region Methods
        private void LoadMenu()
        {
            this.Menus = new ObservableCollection<MenuItemViewModel>();
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "account",
                PageName = "MyProfilePage",
                Title = Languages.MyAccount,
            });
            /*this.Menus.Add(new MenuItemViewModel
            {
                Icon = "icon_tag",
                PageName = "SettingsPage",
                Title = Languages.Tag,
            });
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "icon_qr",
                PageName = "QR",
                Title = Languages.QR,
            });*/
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "logout",
                PageName = "LoginPage",
                Title = Languages.LogOut,
            });
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion
    }
}
