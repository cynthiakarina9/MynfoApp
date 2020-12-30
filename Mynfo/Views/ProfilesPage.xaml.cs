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

        private void TwitterProfile_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByTwitter = new ProfilesByTwitterViewModel();
            Navigation.PushAsync(new ProfilesByTwitterPage());
        }

        private void WhatsAppProfile_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByWhatsApp = new ProfilesByWhatsAppViewModel();
            Navigation.PushAsync(new ProfilesByWhatsAppPage());
        }

        
    }
}