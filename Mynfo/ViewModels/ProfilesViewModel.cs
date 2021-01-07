namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Views;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;

    public class ProfilesViewModel
    {
        #region Constrctor
        public ProfilesViewModel()
        {

        }
        #endregion

        #region Commads

        #region Email
        public ICommand EmailProfileCommand
        {
            get
            {
                return new RelayCommand(EmailProfile);
            }
        }
        private async void EmailProfile()
        {
            MainViewModel.GetInstance().ProfilesByEmail = new ProfilesByEmailViewModel();
            await App.Navigator.PushAsync(new ProfilesByEmailPage());
        }
        #endregion

        #region Phone
        public ICommand PhoneProfileCommand
        {
            get
            {
                return new RelayCommand(PhoneProfile);
            }
        }
        private async void PhoneProfile()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByPhone = new ProfilesByPhoneViewModel();
            await App.Navigator.PushAsync(new ProfilesByPhonePage());
        }
        #endregion

        #region RedSocial
        public ICommand FacebookProfileCommand
        {
            get
            {
                return new RelayCommand(FacebookProfile);
            }
        }
        private async void FacebookProfile()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByFacebook = new ProfilesByFacebookViewModel();
            await App.Navigator.PushAsync(new ProfilesByFacebookPage());
        }

        public ICommand LinkedinProfileCommand
        {
            get
            {
                return new RelayCommand(LinkedinProfile);
            }
        }
        private async void LinkedinProfile()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByLinkedin = new ProfilesByLinkedinViewModel();
            await App.Navigator.PushAsync(new ProfilesByLinkedinPage());
        }

        public ICommand InstagramProfileCommand
        {
            get
            {
                return new RelayCommand(InstagramProfile);
            }
        }
        private async void InstagramProfile()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByInstagram = new ProfilesByInstagramViewModel();
            await App.Navigator.PushAsync(new ProfilesByInstagramPage());
        }

        public ICommand SnapchatProfileCommand
        {
            get
            {
                return new RelayCommand(SnapchatProfile);
            }
        }
        private async void SnapchatProfile()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesBySnapchat = new ProfilesBySnapchatViewModel();
            await App.Navigator.PushAsync(new ProfilesBySnapchatPage());
        }

        public ICommand SpotifyProfileCommand
        {
            get
            {
                return new RelayCommand(SpotifyProfile);
            }
        }
        private async void SpotifyProfile()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesBySpotify = new ProfilesBySpotifyViewModel();
            await App.Navigator.PushAsync(new ProfilesBySpotifyPage());
        }

        public ICommand TiktokProfileCommand
        {
            get
            {
                return new RelayCommand(TiktokProfile);
            }
        }
        private async void TiktokProfile()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByTiktok = new ProfilesByTiktokViewModel();
            await App.Navigator.PushAsync(new ProfilesByTiktokPage());
        }

        public ICommand TwitchProfileCommand
        {
            get
            {
                return new RelayCommand(TwitchProfile);
            }
        }
        private async void TwitchProfile()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByTwitch = new ProfilesByTwitchViewModel();
            await App.Navigator.PushAsync(new ProfilesByTwitchPage());
        }

        public ICommand TwitterProfileCommand
        {
            get
            {
                return new RelayCommand(TwitterProfile);
            }
        }
        private async void TwitterProfile()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByTwitter = new ProfilesByTwitterViewModel();
            await App.Navigator.PushAsync(new ProfilesByTwitterPage());
        }

        public ICommand WebPageProfileCommand
        {
            get
            {
                return new RelayCommand(WebPageProfile);
            }
        }
        private async void WebPageProfile()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByWebPage = new ProfilesByWebPageViewModel();
            await App.Navigator.PushAsync(new ProfilesByWebPagePage());
        }
        public ICommand YoutubeProfileCommand
        {
            get
            {
                return new RelayCommand(YoutubeProfile);
            }
        }
        private async void YoutubeProfile()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByYoutube = new ProfilesByYoutubeViewModel();
            await App.Navigator.PushAsync(new ProfilesByYoutubePage());
        }
        #endregion

        #region Whatsapp
        public ICommand WhatsAppProfileCommand
        {
            get
            {
                return new RelayCommand(WhatsAppProfile);
            }
        }
        private async void WhatsAppProfile()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByWhatsApp = new ProfilesByWhatsAppViewModel();
            await App.Navigator.PushAsync(new ProfilesByWhatsAppPage());
        }
        #endregion

        #endregion
    }
}
