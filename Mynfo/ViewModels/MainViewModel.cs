﻿namespace Mynfo.ViewModels
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
        public ProfileEmail ProfileEmail
        {
            get { return this.profileEmail; }
            set { SetValue(ref this.profileEmail, value); }

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

        public CreateProfilePhoneViewModel CreateProfilePhone
        {
            get;
            set;
        }

        public DetailsBoxViewModel DetailsBox
        {
            get;
            set;
        }

        public EditProfileEmailViewModel EditProfileEmail
        {
            get;
            set;
        }

        public HomeViewModel Home
        {
            get;
            set;
        }
        public LoginViewModel Login
        {
            get;
            set;
        }

        public MyExternalProfileViewModel MyExternalProfile
        {
            get;
            set;
        }

        public MyProfileViewModel MyProfile
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

        public RegisterViewModel Register
        {
            get;
            set;
        }

        public SettingsViewModel Settings
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
