namespace Mynfo.Views
{
    using Mynfo.ViewModels;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileTypeSelection : ContentPage
    {
        public ProfileTypeSelection(int _BoxId, bool _boxDefault, string _boxName)
        {
            InitializeComponent();
            //BackDetails.Clicked += new EventHandler((sender, e) => Back_Clicked(sender, e, _BoxId));

            //Botones de redes sociales
            ProfilesEmail.Clicked += new EventHandler((sender, e) => ProfilesList_Clicked(sender, e, _BoxId, "Email", _boxDefault, _boxName));
            ProfilesPhone.Clicked += new EventHandler((sender, e) => ProfilesList_Clicked(sender, e, _BoxId, "Phone", _boxDefault, _boxName));
            ProfilesFacebook.Clicked += new EventHandler((sender, e) => ProfilesList_Clicked(sender, e, _BoxId, "Facebook", _boxDefault, _boxName));
            ProfilesWhatsapp.Clicked += new EventHandler((sender, e) => ProfilesList_Clicked(sender, e, _BoxId, "Whatsapp", _boxDefault, _boxName));
            ProfilesInstagram.Clicked += new EventHandler((sender, e) => ProfilesList_Clicked(sender, e, _BoxId, "Instagram", _boxDefault, _boxName));
            ProfilesTwitter.Clicked += new EventHandler((sender, e) => ProfilesList_Clicked(sender, e, _BoxId, "Twitter", _boxDefault, _boxName));
            ProfilesLinkedin.Clicked += new EventHandler((sender, e) => ProfilesList_Clicked(sender, e, _BoxId, "LinkedIn", _boxDefault, _boxName));
            ProfilesTiktok.Clicked += new EventHandler((sender, e) => ProfilesList_Clicked(sender, e, _BoxId, "TikTok", _boxDefault, _boxName));
            ProfilesSnapchat.Clicked += new EventHandler((sender, e) => ProfilesList_Clicked(sender, e, _BoxId, "Snapchat", _boxDefault, _boxName));
            ProfilesSpotify.Clicked += new EventHandler((sender, e) => ProfilesList_Clicked(sender, e, _BoxId, "Spotify", _boxDefault, _boxName));
            ProfilesYoutube.Clicked += new EventHandler((sender, e) => ProfilesList_Clicked(sender, e, _BoxId, "Youtube", _boxDefault, _boxName));
            ProfilesTwitch.Clicked += new EventHandler((sender, e) => ProfilesList_Clicked(sender, e, _BoxId, "Twitch", _boxDefault, _boxName));
            ProfilesWebPage.Clicked += new EventHandler((sender, e) => ProfilesList_Clicked(sender, e, _BoxId, "WebPage", _boxDefault, _boxName));
        }

        private void ProfilesList_Clicked(object sender, EventArgs e, int _BoxId, string _profileType, bool _BoxDefault, string _boxName)
        {
            var mainViewModel = MainViewModel.GetInstance();
            //mainViewModel.ProfilesBYPESM = new ProfilesBYPESMViewModel(_BoxId, _profileType, _BoxDefault, _boxName);
            mainViewModel.ProfilesBYPESM = new ProfilesBYPESMViewModel(_BoxId, _profileType, _BoxDefault, _boxName);
            //Application.Current.MainPage = new NavigationPage(new ProfilesBYPESMPage(_BoxId, _profileType, _BoxDefault, _boxName));
            App.Navigator.PushAsync(new ProfilesBYPESMPage(_BoxId, _profileType, _BoxDefault, _boxName));
        }
        private void BackHome_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }
    }
}