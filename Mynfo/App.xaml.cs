namespace Mynfo
{
    using Helpers;
    using Models;
    using Services;
    using System;
    using System.IO;
    using System.Threading.Tasks;
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
        public static string FolderPath { get; private set; }
        #endregion

        #region Constructors
        public App(string root_DB)
        {
            FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            MainPage = new NavigationPage(new HomePage());
            InitializeComponent();

            //Set root SQLite
            root_db = root_DB;

            if (Settings.IsRemembered == "True")
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

        public static Action HideLoginView
        {
            get
            {
                return new Action(() => Application.Current.MainPage =
                                  new NavigationPage(new LoginPage()));
            }
        }

        public static async Task NavigateToProfile(FacebookResponse profile)
        {
            if (profile == null)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }

            var apiService = new ApiService();

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var token = await apiService.LoginFacebook(
                apiSecurity,
                "/api",
                "/Users/LoginFacebook",
                profile);

            if (token == null)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }

            var user = await apiService.GetUserByEmail(
                apiSecurity,
                "/api",
                "/Users/GetUserByEmail",
                token.TokenType,
                token.AccessToken,
                token.UserName);

            UserLocal userLocal = null;
            if (user != null)
            {
                userLocal = Converter.ToUserLocal(user);
                using (var conn = new SQLite.SQLiteConnection(App.root_db))
                {
                    conn.DeleteAll<UserLocal>();
                }
                using (var conn = new SQLite.SQLiteConnection(App.root_db))
                {
                    conn.DeleteAll<TokenResponse>();
                }
            }

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            mainViewModel.User = userLocal;
            mainViewModel.Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
            Settings.IsRemembered = "true";

            mainViewModel.Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }

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
