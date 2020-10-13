namespace Mynfo.ViewModels
{
    using Models;
    using Mynfo.Helpers;
    using System.Collections.ObjectModel;

    public class MainViewModel : BaseViewModel
    {
        #region Attributes
        private UserLocal user;
        #endregion

        #region Properties
        public TokenResponse Token  
        { 
            get; 
            set; 
        }

        public ObservableCollection <MenuItemViewModel> Menus
        {
            get;
            set;
        }

        public UserLocal User
        {
            get { return this.user; }
            set { SetValue(ref this.user, value); }
        }
        #endregion
        #region ViewModels
        public LoginViewModel Login
        { 
            get; 
            set; 
        }

        public HomeViewModel Home
        {
            get;
            set;
        }

        public RegisterViewModel Register 
        { 
            get; 
            set; 
        }

        public MyProfileViewModel MyProfile
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

        public SettingsViewModel Settings
        {
            get;
            set;
        }

        public ProfilesViewModel Profiles
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

        #region Methos
        private void LoadMenu()
        {
            this.Menus = new ObservableCollection<MenuItemViewModel>();
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "account",
                PageName = "MyProfilePage",
                Title = Languages.MyAccount,
            });
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "perfiles",
                PageName = "ProfilesPage",
                Title = Languages.MyProfiles,
            });
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "configuraciones",
                PageName = "SettingsPage",
                Title = Languages.Settings,
            });
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
