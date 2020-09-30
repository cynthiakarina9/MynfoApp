namespace Mynfo
{
    using Helpers;
    using Models;
    using Services;
    using System;
    using ViewModels;
    using Views;
    using Xamarin.Forms;
    public partial class App : Application
    {
        #region Variables
        public static string root_db;
        #endregion

        #region Properties
        public static NavigationPage Navigator
        {
            get;
            internal set;
        }
        public static MasterPage Master 
        { 
            get; 
            internal set; 
        }
        #endregion

        #region Constructors
        public App(string root_DB)
        {
            InitializeComponent();

            //Set root SQLite
            root_db = root_DB;

            if (Settings.IsRemembered == "true")
            {
                
                var token = new TokenResponse();               
                using (var conn = new SQLite.SQLiteConnection(App.root_db))
                {
                    conn.CreateTable<TokenResponse>();
                    token = conn.Table<TokenResponse>().FirstOrDefault();
                }

                if (token != null && token.Expires > DateTime.Now)
                {
                    //Connection with SQLite
                    var user = new UserLocal();
                    using (var conn = new SQLite.SQLiteConnection(App.root_db))
                    {
                        conn.CreateTable<UserLocal>();
                        user = conn.Table<UserLocal>().FirstOrDefault();
                    }
                    var mainViewModel = MainViewModel.GetInstance();
                    mainViewModel.Token = token;
                    mainViewModel.User = user;//sqlite
                    mainViewModel.Home = new HomeViewModel();
                    Application.Current.MainPage = new MasterPage();
                }
                else
                {
                    this.MainPage = new NavigationPage(new LoginPage());
                }
                
            }
            else
            {
                this.MainPage = new NavigationPage(new LoginPage());
            }
        }
        #endregion

        #region Methods
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        #endregion
    }
}
