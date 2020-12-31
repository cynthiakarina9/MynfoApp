namespace Mynfo.Views
{
    using System;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilesPage : ContentPage
    {
        public ProfilesPage()
        {
            InitializeComponent();
        }

        private void PhoneProfile_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByPhone = new ProfilesByPhoneViewModel();
            Navigation.PushAsync(new ProfilesByPhonePage());
        }
        private void EmailProfile_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().ProfilesByEmail = new ProfilesByEmailViewModel();
            Navigation.PushAsync(new ProfilesByEmailPage());
        }
        private void FacebookProfile_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByFacebook = new ProfilesByFacebookViewModel();
            Navigation.PushAsync(new ProfilesByFacebookPage());
        }

        private void LinkedinProfile_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByLinkedin = new ProfilesByLinkedinViewModel();
            Navigation.PushAsync(new ProfilesByLinkedinPage());
        }

        private void InstagramProfile_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByInstagram = new ProfilesByInstagramViewModel();
            Navigation.PushAsync(new ProfilesByInstagramPage());
        }

        private void SnapchatProfile_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesBySnapchat = new ProfilesBySnapchatViewModel();
            Navigation.PushAsync(new ProfilesBySnapchatPage());
        }

        private void SpotifyProfile_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesBySpotify = new ProfilesBySpotifyViewModel();
            Navigation.PushAsync(new ProfilesBySpotifyPage());
        }

        private void TiktokProfile_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByTiktok = new ProfilesByTiktokViewModel();
            Navigation.PushAsync(new ProfilesByTiktokPage());
        }

        private void TwitchProfile_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByTwitch = new ProfilesByTwitchViewModel();
            Navigation.PushAsync(new ProfilesByTwitchPage());
        }

        private void TwitterProfile_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByTwitter = new ProfilesByTwitterViewModel();
            Navigation.PushAsync(new ProfilesByTwitterPage());
        }

        private void WebPageProfile_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByWebPage = new ProfilesByWebPageViewModel();
            Navigation.PushAsync(new ProfilesByWebPagePage());
        }

        private void WhatsAppProfile_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            //mainViewModel.ProfilesByWhatsApp = new ProfilesByWhatsAppViewModel();
            //Navigation.PushAsync(new ProfilesByWhatsAppPage());
        }

        private void YoutubeProfile_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByYoutube = new ProfilesByYoutubeViewModel();
            Navigation.PushAsync(new ProfilesByYoutubePage());
        }
    }
}