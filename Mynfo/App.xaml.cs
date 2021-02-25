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
    using Xamarin.Essentials;
    using Xamarin.Forms;
    using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

    public partial class App : Xamarin.Forms.Application
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
            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            
            InitializeComponent();

            //Set root SQLite
            root_db = root_DB;

            Preferences.Set("key1", Guid.NewGuid().ToString());
            Preferences.Set("IsEnabled", true);

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
                        int a = conn.Table<UserLocal>().Count();
                    }
                    var mainViewModel = MainViewModel.GetInstance();
                    mainViewModel.Token = token;
                    mainViewModel.User = user;//sqlite
                    mainViewModel.Home = new HomeViewModel();
                    mainViewModel.Register = new RegisterViewModel();
                    mainViewModel.MyProfile = new MyProfileViewModel();
                    mainViewModel.Profiles = new ProfilesViewModel();
                    mainViewModel.TAG = new TAGViewModel();
                    mainViewModel.ChangePassword = new ChangePasswordViewModel();
                    mainViewModel.ListForeignBox = new ListForeignBoxViewModel();
                    //if(Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
                    //{
                        Xamarin.Forms.Application.Current.MainPage = new MasterPage();
                    /*}
                    else if(Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS)
                    {
                        Xamarin.Forms.Application.Current.MainPage = new MasterPageIOS();
                    }*/
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
                return new Action(() => Xamarin.Forms.Application.Current.MainPage =
                                  new NavigationPage(new LoginPage()));
            }
        }

        public static async Task NavigateToProfile(FacebookResponse profile)
        {
            if (profile == null)
            {
                Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }

            var apiService = new ApiService();

            var apiSecurity = Xamarin.Forms.Application.Current.Resources["APISecurity"].ToString();
            var token = await apiService.LoginFacebook(
                apiSecurity,
                "/api",
                "/Users/LoginFacebook",
                profile);

            if (token == null)
            {
                Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }

            var user = await apiService.GetUserByEmail(
                apiSecurity,
                "/api",
                "/Users/GetUserByEmail",
                token.TokenType,
                token.AccessToken,
                token.UserName);

            UserLocal userLocal;
            userLocal = Converter.ToUserLocal(user);

            if (user != null)
            {
                using (var conn = new SQLite.SQLiteConnection(App.root_db))
                {
                    conn.CreateTable<UserLocal>();
                    conn.Insert(userLocal);
                }
                using (var conn = new SQLite.SQLiteConnection(App.root_db))
                {
                    conn.CreateTable<TokenResponse>();
                    conn.Insert(token);
                }
            }
            
            
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            mainViewModel.User = userLocal;
            mainViewModel.Home = new HomeViewModel();
            mainViewModel.Register = new RegisterViewModel();
            mainViewModel.MyProfile = new MyProfileViewModel();
            mainViewModel.Profiles = new ProfilesViewModel();
            mainViewModel.TAG = new TAGViewModel();
            mainViewModel.ListForeignBox = new ListForeignBoxViewModel();
            Xamarin.Forms.Application.Current.MainPage = new MasterPage();
            Settings.IsRemembered = "true";
        }

        /*public static async Task DisplayAlertAsync(string msg) =>
            await Xamarin.Forms.Device.InvokeOnMainThreadAsync(async () => await Current.MainPage.DisplayAlert("message from service", msg, "ok"));*/


        public static async Task DisplayAlertAsync(string msg) =>
          await Xamarin.Forms.Device.InvokeOnMainThreadAsync(async () => await Current.MainPage.DisplayAlert("", msg, "ok"));




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
