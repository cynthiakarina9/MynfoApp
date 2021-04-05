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
            MainViewModel.GetInstance().Profiles.GetListEmail();
            ProfileListEmail.IsVisible = true;
            ProfileListPhone.IsVisible = false;
            ProfileListFacebook.IsVisible = false;
            ProfileListInstagram.IsVisible = false;
            ProfileListLinkedin.IsVisible = false;
            ProfileListSnapchat.IsVisible = false;
            ProfileListSpotify.IsVisible = false;
            ProfileListTelegram.IsVisible = false;
            ProfileListTiktok.IsVisible = false;
            ProfileListTwitch.IsVisible = false;
            ProfileListTwitter.IsVisible = false;
            ProfileListWebPage.IsVisible = false;
            ProfileListWhatsapp.IsVisible = false;
            ProfileListYoutube.IsVisible = false;
        }

        private void EmailProfile_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Profiles.GetListEmail();
            ProfileListEmail.IsVisible = true;
            ProfileListPhone.IsVisible = false;
            ProfileListFacebook.IsVisible = false;
            ProfileListInstagram.IsVisible = false;
            ProfileListLinkedin.IsVisible = false;
            ProfileListSnapchat.IsVisible = false;
            ProfileListSpotify.IsVisible = false;
            ProfileListTelegram.IsVisible = false;
            ProfileListTiktok.IsVisible = false;
            ProfileListTwitch.IsVisible = false;
            ProfileListTwitter.IsVisible = false;
            ProfileListWebPage.IsVisible = false;
            ProfileListWhatsapp.IsVisible = false;
            ProfileListYoutube.IsVisible = false;
        }
        private void PhoneProfile_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Profiles.GetListPhone();
            ProfileListEmail.IsVisible = false;
            ProfileListPhone.IsVisible = true;
            ProfileListFacebook.IsVisible = false;
            ProfileListInstagram.IsVisible = false;
            ProfileListLinkedin.IsVisible = false;
            ProfileListSnapchat.IsVisible = false;
            ProfileListSpotify.IsVisible = false;
            ProfileListTelegram.IsVisible = false;
            ProfileListTiktok.IsVisible = false;
            ProfileListTwitch.IsVisible = false;
            ProfileListTwitter.IsVisible = false;
            ProfileListWebPage.IsVisible = false;
            ProfileListWhatsapp.IsVisible = false;
            ProfileListYoutube.IsVisible = false;
        }
        private void FacebookProfile_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Profiles.GetListSM(1);
            ProfileListEmail.IsVisible = false;
            ProfileListPhone.IsVisible = false;
            ProfileListFacebook.IsVisible = true;
            ProfileListInstagram.IsVisible = false;
            ProfileListLinkedin.IsVisible = false;
            ProfileListSnapchat.IsVisible = false;
            ProfileListSpotify.IsVisible = false;
            ProfileListTelegram.IsVisible = false;
            ProfileListTiktok.IsVisible = false;
            ProfileListTwitch.IsVisible = false;
            ProfileListTwitter.IsVisible = false;
            ProfileListWebPage.IsVisible = false;
            ProfileListWhatsapp.IsVisible = false;
            ProfileListYoutube.IsVisible = false;
        }

        private void LinkedinProfile_Clicked(object sender, EventArgs e)
        {
            ProfileListEmail.IsVisible = false;
            ProfileListPhone.IsVisible = false;
            ProfileListFacebook.IsVisible = false;
            ProfileListInstagram.IsVisible = false;
            ProfileListLinkedin.IsVisible = true;
            ProfileListSnapchat.IsVisible = false;
            ProfileListSpotify.IsVisible = false;
            ProfileListTelegram.IsVisible = false;
            ProfileListTiktok.IsVisible = false;
            ProfileListTwitch.IsVisible = false;
            ProfileListTwitter.IsVisible = false;
            ProfileListWebPage.IsVisible = false;
            ProfileListWhatsapp.IsVisible = false;
            ProfileListYoutube.IsVisible = false;
        }

        private void InstagramProfile_Clicked(object sender, EventArgs e)
        {
            ProfileListEmail.IsVisible = false;
            ProfileListPhone.IsVisible = false;
            ProfileListFacebook.IsVisible = false;
            ProfileListInstagram.IsVisible = false;
            ProfileListLinkedin.IsVisible = false;
            ProfileListSnapchat.IsVisible = false;
            ProfileListSpotify.IsVisible = false;
            ProfileListTelegram.IsVisible = true;
            ProfileListTiktok.IsVisible = false;
            ProfileListTwitch.IsVisible = false;
            ProfileListTwitter.IsVisible = false;
            ProfileListWebPage.IsVisible = false;
            ProfileListWhatsapp.IsVisible = false;
            ProfileListYoutube.IsVisible = false;
        }

        private void SnapchatProfile_Clicked(object sender, EventArgs e)
        {
            ProfileListEmail.IsVisible = false;
            ProfileListPhone.IsVisible = false;
            ProfileListFacebook.IsVisible = false;
            ProfileListInstagram.IsVisible = false;
            ProfileListLinkedin.IsVisible = false;
            ProfileListSnapchat.IsVisible = true;
            ProfileListSpotify.IsVisible = false;
            ProfileListTelegram.IsVisible = false;
            ProfileListTiktok.IsVisible = false;
            ProfileListTwitch.IsVisible = false;
            ProfileListTwitter.IsVisible = false;
            ProfileListWebPage.IsVisible = false;
            ProfileListWhatsapp.IsVisible = false;
            ProfileListYoutube.IsVisible = false;
        }

        private void SpotifyProfile_Clicked(object sender, EventArgs e)
        {
            ProfileListEmail.IsVisible = false;
            ProfileListPhone.IsVisible = false;
            ProfileListFacebook.IsVisible = false;
            ProfileListInstagram.IsVisible = false;
            ProfileListLinkedin.IsVisible = false;
            ProfileListSnapchat.IsVisible = false;
            ProfileListSpotify.IsVisible = true;
            ProfileListTelegram.IsVisible = false;
            ProfileListTiktok.IsVisible = false;
            ProfileListTwitch.IsVisible = false;
            ProfileListTwitter.IsVisible = false;
            ProfileListWebPage.IsVisible = false;
            ProfileListWhatsapp.IsVisible = false;
            ProfileListYoutube.IsVisible = false;
        }

        private void TiktokProfile_Clicked(object sender, EventArgs e)
        {
            ProfileListEmail.IsVisible = false;
            ProfileListPhone.IsVisible = false;
            ProfileListFacebook.IsVisible = false;
            ProfileListInstagram.IsVisible = false;
            ProfileListLinkedin.IsVisible = false;
            ProfileListSnapchat.IsVisible = false;
            ProfileListSpotify.IsVisible = false;
            ProfileListTelegram.IsVisible = false;
            ProfileListTiktok.IsVisible = true;
            ProfileListTwitch.IsVisible = false;
            ProfileListTwitter.IsVisible = false;
            ProfileListWebPage.IsVisible = false;
            ProfileListWhatsapp.IsVisible = false;
            ProfileListYoutube.IsVisible = false;
        }

        private void TwitchProfile_Clicked(object sender, EventArgs e)
        {
            ProfileListEmail.IsVisible = false;
            ProfileListPhone.IsVisible = false;
            ProfileListFacebook.IsVisible = false;
            ProfileListInstagram.IsVisible = false;
            ProfileListLinkedin.IsVisible = false;
            ProfileListSnapchat.IsVisible = false;
            ProfileListSpotify.IsVisible = false;
            ProfileListTelegram.IsVisible = false;
            ProfileListTiktok.IsVisible = false;
            ProfileListTwitch.IsVisible = true;
            ProfileListTwitter.IsVisible = false;
            ProfileListWebPage.IsVisible = false;
            ProfileListWhatsapp.IsVisible = false;
            ProfileListYoutube.IsVisible = false;
        }

        private void TwitterProfile_Clicked(object sender, EventArgs e)
        {
            ProfileListEmail.IsVisible = false;
            ProfileListPhone.IsVisible = false;
            ProfileListFacebook.IsVisible = false;
            ProfileListInstagram.IsVisible = false;
            ProfileListLinkedin.IsVisible = false;
            ProfileListSnapchat.IsVisible = false;
            ProfileListSpotify.IsVisible = false;
            ProfileListTelegram.IsVisible = false;
            ProfileListTiktok.IsVisible = false;
            ProfileListTwitch.IsVisible = false;
            ProfileListTwitter.IsVisible = true;
            ProfileListWebPage.IsVisible = false;
            ProfileListWhatsapp.IsVisible = false;
            ProfileListYoutube.IsVisible = false;
        }

        private void WebPageProfile_Clicked(object sender, EventArgs e)
        {
            ProfileListEmail.IsVisible = false;
            ProfileListPhone.IsVisible = false;
            ProfileListFacebook.IsVisible = false;
            ProfileListInstagram.IsVisible = false;
            ProfileListLinkedin.IsVisible = false;
            ProfileListSnapchat.IsVisible = false;
            ProfileListSpotify.IsVisible = false;
            ProfileListTelegram.IsVisible = false;
            ProfileListTiktok.IsVisible = false;
            ProfileListTwitch.IsVisible = false;
            ProfileListTwitter.IsVisible = false;
            ProfileListWebPage.IsVisible = true;
            ProfileListWhatsapp.IsVisible = false;
            ProfileListYoutube.IsVisible = false;
        }

        private void WhatsAppProfile_Clicked(object sender, EventArgs e)
        {
            ProfileListEmail.IsVisible = false;
            ProfileListPhone.IsVisible = false;
            ProfileListFacebook.IsVisible = false;
            ProfileListInstagram.IsVisible = false;
            ProfileListLinkedin.IsVisible = false;
            ProfileListSnapchat.IsVisible = false;
            ProfileListSpotify.IsVisible = false;
            ProfileListTelegram.IsVisible = false;
            ProfileListTiktok.IsVisible = false;
            ProfileListTwitch.IsVisible = false;
            ProfileListTwitter.IsVisible = false;
            ProfileListWebPage.IsVisible = false;
            ProfileListWhatsapp.IsVisible = true;
            ProfileListYoutube.IsVisible = false;
        }

        private void YoutubeProfile_Clicked(object sender, EventArgs e)
        {
            ProfileListEmail.IsVisible = false;
            ProfileListPhone.IsVisible = false;
            ProfileListFacebook.IsVisible = false;
            ProfileListInstagram.IsVisible = false;
            ProfileListLinkedin.IsVisible = false;
            ProfileListSnapchat.IsVisible = false;
            ProfileListSpotify.IsVisible = false;
            ProfileListTelegram.IsVisible = false;
            ProfileListTiktok.IsVisible = false;
            ProfileListTwitch.IsVisible = false;
            ProfileListTwitter.IsVisible = false;
            ProfileListWebPage.IsVisible = false;
            ProfileListWhatsapp.IsVisible = false;
            ProfileListYoutube.IsVisible = true;
        }
        private void TelegramProfile_Clicked(object sender, EventArgs e)
        {
            ProfileListEmail.IsVisible = false;
            ProfileListPhone.IsVisible = false;
            ProfileListFacebook.IsVisible = false;
            ProfileListInstagram.IsVisible = false;
            ProfileListLinkedin.IsVisible = false;
            ProfileListSnapchat.IsVisible = false;
            ProfileListSpotify.IsVisible = false;
            ProfileListTelegram.IsVisible = true;
            ProfileListTiktok.IsVisible = false;
            ProfileListTwitch.IsVisible = false;
            ProfileListTwitter.IsVisible = false;
            ProfileListWebPage.IsVisible = false;
            ProfileListWhatsapp.IsVisible = false;
            ProfileListYoutube.IsVisible = false;
        }
    }
}