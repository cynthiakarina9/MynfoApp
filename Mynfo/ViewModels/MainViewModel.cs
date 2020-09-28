namespace Mynfo.ViewModels
{
    using Models;
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using System;
    using System.Collections.ObjectModel;
    using ViewModels;
    using Xamarin.Forms;

    public class MainViewModel
    {
        #region Properties
        public string Token  
        { 
            get; 
            set; 
        }

        public string TokenType
        {
            get;
            set;
        }

        public ObservableCollection <MenuItemViewModel> Menus
        {
            get;
            set;
        }

        public User User{ get; set; }
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
                PageName = Languages.MyProfile,
                Title = "MyProfile",
            });
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "exit",
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
