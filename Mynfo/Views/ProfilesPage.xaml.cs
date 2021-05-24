namespace Mynfo.Views
{
    using Mynfo.Domain;
    using System;
    using System.Linq;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilesPage : ContentPage
    {
        public ProfileEmail selectedItem { get; set; }

        public ProfilesPage()
        {
            InitializeComponent();
            MainViewModel.GetInstance().Profiles.GetListEmail();
            selectedItem = null;
            #region Listas
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
            #endregion

            #region Buttons
            ButtonEmail.IsVisible = true;
            ButtonFacebook.IsVisible = false;
            ButtonInstagram.IsVisible = false;
            ButtonLinkedin.IsVisible = false;
            ButtonPhone.IsVisible = false;
            ButtonSnapchat.IsVisible = false;
            ButtonSpotify.IsVisible = false;
            ButtonTelegram.IsVisible = false;
            ButtonTiktok.IsVisible = false;
            ButtonTwitch.IsVisible = false;
            ButtonTwitter.IsVisible = false;
            ButtonWebPage.IsVisible = false;
            ButtonWhatsapp.IsVisible = false;
            ButtonYoutube.IsVisible = false;
            #endregion

            #region Image
            ImageEmail.IsVisible = true;
            ImageFacebook.IsVisible = false;
            ImageInstagram.IsVisible = false;
            ImageLinkedIn.IsVisible = false;
            ImagePhone.IsVisible = false;
            ImageSnapchat.IsVisible = false;
            ImageSpotify.IsVisible = false;
            ImageTelegram.IsVisible = false;
            ImageTiktok.IsVisible = false;
            ImageTwitch.IsVisible = false;
            ImageTwitter.IsVisible = false;
            ImageWeb.IsVisible = false;
            ImageWhatsapp.IsVisible = false;
            ImageYoutube.IsVisible = false;
            #endregion

            #region Commands
            ButtonFacebook.Clicked += new EventHandler((sender, e) => ButtonSM_Clicked(sender, e, "Facebook"));
            ButtonInstagram.Clicked += new EventHandler((sender, e) => ButtonSM_Clicked(sender, e, "Instagram"));
            ButtonLinkedin.Clicked += new EventHandler((sender, e) => ButtonSM_Clicked(sender, e, "Linkedin"));
            ButtonSnapchat.Clicked += new EventHandler((sender, e) => ButtonSM_Clicked(sender, e, "Snapchat"));
            ButtonSpotify.Clicked += new EventHandler((sender, e) => ButtonSM_Clicked(sender, e, "Spotify"));
            ButtonTelegram.Clicked += new EventHandler((sender, e) => ButtonSM_Clicked(sender, e, "Telegram"));
            ButtonTiktok.Clicked += new EventHandler((sender, e) => ButtonSM_Clicked(sender, e, "Tiktok"));
            ButtonTwitch.Clicked += new EventHandler((sender, e) => ButtonSM_Clicked(sender, e, "Twitch"));
            ButtonTwitter.Clicked += new EventHandler((sender, e) => ButtonSM_Clicked(sender, e, "Twitter"));
            ButtonWebPage.Clicked += new EventHandler((sender, e) => ButtonSM_Clicked(sender, e, "WebPage"));
            ButtonYoutube.Clicked += new EventHandler((sender, e) => ButtonSM_Clicked(sender, e, "Youtube"));
            #endregion
        }
        #region Commands


        #region Button Lateral
        private void EmailProfile_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Profiles.GetListEmail();

            #region Listas
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
            #endregion

            #region Buttons
            ButtonEmail.IsVisible = true;
            ButtonFacebook.IsVisible = false;
            ButtonInstagram.IsVisible = false;
            ButtonLinkedin.IsVisible = false;
            ButtonPhone.IsVisible = false;
            ButtonSnapchat.IsVisible = false;
            ButtonSpotify.IsVisible = false;
            ButtonTelegram.IsVisible = false;
            ButtonTiktok.IsVisible = false;
            ButtonTwitch.IsVisible = false;
            ButtonTwitter.IsVisible = false;
            ButtonWebPage.IsVisible = false;
            ButtonWhatsapp.IsVisible = false;
            ButtonYoutube.IsVisible = false;
            #endregion

            #region Image
            ImageEmail.IsVisible = true;
            ImageFacebook.IsVisible = false;
            ImageInstagram.IsVisible = false;
            ImageLinkedIn.IsVisible = false;
            ImagePhone.IsVisible = false;
            ImageSnapchat.IsVisible = false;
            ImageSpotify.IsVisible = false;
            ImageTelegram.IsVisible = false;
            ImageTiktok.IsVisible = false;
            ImageTwitch.IsVisible = false;
            ImageTwitter.IsVisible = false;
            ImageWeb.IsVisible = false;
            ImageWhatsapp.IsVisible = false;
            ImageYoutube.IsVisible = false;
            #endregion
        }

        private void PhoneProfile_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Profiles.GetListPhone();

            #region Listas
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
            #endregion

            #region Buttons
            ButtonEmail.IsVisible = false;
            ButtonFacebook.IsVisible = false;
            ButtonInstagram.IsVisible = false;
            ButtonLinkedin.IsVisible = false;
            ButtonPhone.IsVisible = true;
            ButtonSnapchat.IsVisible = false;
            ButtonSpotify.IsVisible = false;
            ButtonTelegram.IsVisible = false;
            ButtonTiktok.IsVisible = false;
            ButtonTwitch.IsVisible = false;
            ButtonTwitter.IsVisible = false;
            ButtonWebPage.IsVisible = false;
            ButtonWhatsapp.IsVisible = false;
            ButtonYoutube.IsVisible = false;
            #endregion

            #region Image
            ImageEmail.IsVisible = false;
            ImageFacebook.IsVisible = false;
            ImageInstagram.IsVisible = false;
            ImageLinkedIn.IsVisible = false;
            ImagePhone.IsVisible = true;
            ImageSnapchat.IsVisible = false;
            ImageSpotify.IsVisible = false;
            ImageTelegram.IsVisible = false;
            ImageTiktok.IsVisible = false;
            ImageTwitch.IsVisible = false;
            ImageTwitter.IsVisible = false;
            ImageWeb.IsVisible = false;
            ImageWhatsapp.IsVisible = false;
            ImageYoutube.IsVisible = false;
            #endregion
        }

        private void FacebookProfile_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Profiles.GetListSM(1);

            #region Listas
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
            #endregion

            #region Buttons
            ButtonEmail.IsVisible = false;
            ButtonFacebook.IsVisible = true;
            ButtonInstagram.IsVisible = false;
            ButtonLinkedin.IsVisible = false;
            ButtonPhone.IsVisible = false;
            ButtonSnapchat.IsVisible = false;
            ButtonSpotify.IsVisible = false;
            ButtonTelegram.IsVisible = false;
            ButtonTiktok.IsVisible = false;
            ButtonTwitch.IsVisible = false;
            ButtonTwitter.IsVisible = false;
            ButtonWebPage.IsVisible = false;
            ButtonWhatsapp.IsVisible = false;
            ButtonYoutube.IsVisible = false;
            #endregion

            #region Image
            ImageEmail.IsVisible = false;
            ImageFacebook.IsVisible = true;
            ImageInstagram.IsVisible = false;
            ImageLinkedIn.IsVisible = false;
            ImagePhone.IsVisible = false;
            ImageSnapchat.IsVisible = false;
            ImageSpotify.IsVisible = false;
            ImageTelegram.IsVisible = false;
            ImageTiktok.IsVisible = false;
            ImageTwitch.IsVisible = false;
            ImageTwitter.IsVisible = false;
            ImageWeb.IsVisible = false;
            ImageWhatsapp.IsVisible = false;
            ImageYoutube.IsVisible = false;
            #endregion
        }

        private void LinkedinProfile_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Profiles.GetListSM(5);

            #region Listas
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
            #endregion 

            #region Buttons
            ButtonEmail.IsVisible = false;
            ButtonFacebook.IsVisible = false;
            ButtonInstagram.IsVisible = false;
            ButtonLinkedin.IsVisible = true;
            ButtonPhone.IsVisible = false;
            ButtonSnapchat.IsVisible = false;
            ButtonSpotify.IsVisible = false;
            ButtonTelegram.IsVisible = false;
            ButtonTiktok.IsVisible = false;
            ButtonTwitch.IsVisible = false;
            ButtonTwitter.IsVisible = false;
            ButtonWebPage.IsVisible = false;
            ButtonWhatsapp.IsVisible = false;
            ButtonYoutube.IsVisible = false;
            #endregion

            #region Image
            ImageEmail.IsVisible = false;
            ImageFacebook.IsVisible = false;
            ImageInstagram.IsVisible = false;
            ImageLinkedIn.IsVisible = true;
            ImagePhone.IsVisible = false;
            ImageSnapchat.IsVisible = false;
            ImageSpotify.IsVisible = false;
            ImageTelegram.IsVisible = false;
            ImageTiktok.IsVisible = false;
            ImageTwitch.IsVisible = false;
            ImageTwitter.IsVisible = false;
            ImageWeb.IsVisible = false;
            ImageWhatsapp.IsVisible = false;
            ImageYoutube.IsVisible = false;
            #endregion
        }

        private void InstagramProfile_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Profiles.GetListSM(2);

            #region Listas
            ProfileListEmail.IsVisible = false;
            ProfileListPhone.IsVisible = false;
            ProfileListFacebook.IsVisible = false;
            ProfileListInstagram.IsVisible = true;
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
            #endregion

            #region Buttons
            ButtonEmail.IsVisible = false;
            ButtonFacebook.IsVisible = false;
            ButtonInstagram.IsVisible = true;
            ButtonLinkedin.IsVisible = false;
            ButtonPhone.IsVisible = false;
            ButtonSnapchat.IsVisible = false;
            ButtonSpotify.IsVisible = false;
            ButtonTelegram.IsVisible = false;
            ButtonTiktok.IsVisible = false;
            ButtonTwitch.IsVisible = false;
            ButtonTwitter.IsVisible = false;
            ButtonWebPage.IsVisible = false;
            ButtonWhatsapp.IsVisible = false;
            ButtonYoutube.IsVisible = false;
            #endregion

            #region Image
            ImageEmail.IsVisible = false;
            ImageFacebook.IsVisible = false;
            ImageInstagram.IsVisible = true;
            ImageLinkedIn.IsVisible = false;
            ImagePhone.IsVisible = false;
            ImageSnapchat.IsVisible = false;
            ImageSpotify.IsVisible = false;
            ImageTelegram.IsVisible = false;
            ImageTiktok.IsVisible = false;
            ImageTwitch.IsVisible = false;
            ImageTwitter.IsVisible = false;
            ImageWeb.IsVisible = false;
            ImageWhatsapp.IsVisible = false;
            ImageYoutube.IsVisible = false;
            #endregion
        }

        private void SnapchatProfile_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Profiles.GetListSM(4);

            #region Listas
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
            #endregion

            #region Buttons
            ButtonEmail.IsVisible = false;
            ButtonFacebook.IsVisible = false;
            ButtonInstagram.IsVisible = false;
            ButtonLinkedin.IsVisible = false;
            ButtonPhone.IsVisible = false;
            ButtonSnapchat.IsVisible = true;
            ButtonSpotify.IsVisible = false;
            ButtonTelegram.IsVisible = false;
            ButtonTiktok.IsVisible = false;
            ButtonTwitch.IsVisible = false;
            ButtonTwitter.IsVisible = false;
            ButtonWebPage.IsVisible = false;
            ButtonWhatsapp.IsVisible = false;
            ButtonYoutube.IsVisible = false;
            #endregion

            #region Image
            ImageEmail.IsVisible = false;
            ImageFacebook.IsVisible = false;
            ImageInstagram.IsVisible = false;
            ImageLinkedIn.IsVisible = false;
            ImagePhone.IsVisible = false;
            ImageSnapchat.IsVisible = true;
            ImageSpotify.IsVisible = false;
            ImageTelegram.IsVisible = false;
            ImageTiktok.IsVisible = false;
            ImageTwitch.IsVisible = false;
            ImageTwitter.IsVisible = false;
            ImageWeb.IsVisible = false;
            ImageWhatsapp.IsVisible = false;
            ImageYoutube.IsVisible = false;
            #endregion
        }

        private void SpotifyProfile_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Profiles.GetListSM(8);

            #region Listas
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
            #endregion

            #region Buttons
            ButtonEmail.IsVisible = false;
            ButtonFacebook.IsVisible = false;
            ButtonInstagram.IsVisible = false;
            ButtonLinkedin.IsVisible = false;
            ButtonPhone.IsVisible = false;
            ButtonSnapchat.IsVisible = false;
            ButtonSpotify.IsVisible = true;
            ButtonTelegram.IsVisible = false;
            ButtonTiktok.IsVisible = false;
            ButtonTwitch.IsVisible = false;
            ButtonTwitter.IsVisible = false;
            ButtonWebPage.IsVisible = false;
            ButtonWhatsapp.IsVisible = false;
            ButtonYoutube.IsVisible = false;
            #endregion

            #region Image
            ImageEmail.IsVisible = false;
            ImageFacebook.IsVisible = false;
            ImageInstagram.IsVisible = false;
            ImageLinkedIn.IsVisible = false;
            ImagePhone.IsVisible = false;
            ImageSnapchat.IsVisible = false;
            ImageSpotify.IsVisible = true;
            ImageTelegram.IsVisible = false;
            ImageTiktok.IsVisible = false;
            ImageTwitch.IsVisible = false;
            ImageTwitter.IsVisible = false;
            ImageWeb.IsVisible = false;
            ImageWhatsapp.IsVisible = false;
            ImageYoutube.IsVisible = false;
            #endregion

        }

        private void TiktokProfile_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Profiles.GetListSM(6);

            #region Listas
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
            #endregion

            #region Buttons
            ButtonEmail.IsVisible = false;
            ButtonFacebook.IsVisible = false;
            ButtonInstagram.IsVisible = false;
            ButtonLinkedin.IsVisible = false;
            ButtonPhone.IsVisible = false;
            ButtonSnapchat.IsVisible = false;
            ButtonSpotify.IsVisible = false;
            ButtonTelegram.IsVisible = false;
            ButtonTiktok.IsVisible = true;
            ButtonTwitch.IsVisible = false;
            ButtonTwitter.IsVisible = false;
            ButtonWebPage.IsVisible = false;
            ButtonWhatsapp.IsVisible = false;
            ButtonYoutube.IsVisible = false;
            #endregion

            #region Image
            ImageEmail.IsVisible = false;
            ImageFacebook.IsVisible = false;
            ImageInstagram.IsVisible = false;
            ImageLinkedIn.IsVisible = false;
            ImagePhone.IsVisible = false;
            ImageSnapchat.IsVisible = false;
            ImageSpotify.IsVisible = false;
            ImageTelegram.IsVisible = false;
            ImageTiktok.IsVisible = true;
            ImageTwitch.IsVisible = false;
            ImageTwitter.IsVisible = false;
            ImageWeb.IsVisible = false;
            ImageWhatsapp.IsVisible = false;
            ImageYoutube.IsVisible = false;
            #endregion
        }

        private void TwitchProfile_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Profiles.GetListSM(9);

            #region Listas
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
            #endregion

            #region Buttons
            ButtonEmail.IsVisible = false;
            ButtonFacebook.IsVisible = false;
            ButtonInstagram.IsVisible = false;
            ButtonLinkedin.IsVisible = false;
            ButtonPhone.IsVisible = false;
            ButtonSnapchat.IsVisible = false;
            ButtonSpotify.IsVisible = false;
            ButtonTelegram.IsVisible = false;
            ButtonTiktok.IsVisible = false;
            ButtonTwitch.IsVisible = true;
            ButtonTwitter.IsVisible = false;
            ButtonWebPage.IsVisible = false;
            ButtonWhatsapp.IsVisible = false;
            ButtonYoutube.IsVisible = false;
            #endregion

            #region Image
            ImageEmail.IsVisible = false;
            ImageFacebook.IsVisible = false;
            ImageInstagram.IsVisible = false;
            ImageLinkedIn.IsVisible = false;
            ImagePhone.IsVisible = false;
            ImageSnapchat.IsVisible = false;
            ImageSpotify.IsVisible = false;
            ImageTelegram.IsVisible = false;
            ImageTiktok.IsVisible = false;
            ImageTwitch.IsVisible = true;
            ImageTwitter.IsVisible = false;
            ImageWeb.IsVisible = false;
            ImageWhatsapp.IsVisible = false;
            ImageYoutube.IsVisible = false;
            #endregion
        }

        private void TwitterProfile_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Profiles.GetListSM(3);

            #region Listas
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
            #endregion

            #region Buttons
            ButtonEmail.IsVisible = false;
            ButtonFacebook.IsVisible = false;
            ButtonInstagram.IsVisible = false;
            ButtonLinkedin.IsVisible = false;
            ButtonPhone.IsVisible = false;
            ButtonSnapchat.IsVisible = false;
            ButtonSpotify.IsVisible = false;
            ButtonTelegram.IsVisible = false;
            ButtonTiktok.IsVisible = false;
            ButtonTwitch.IsVisible = false;
            ButtonTwitter.IsVisible = true;
            ButtonWebPage.IsVisible = false;
            ButtonWhatsapp.IsVisible = false;
            ButtonYoutube.IsVisible = false;
            #endregion

            #region Image
            ImageEmail.IsVisible = false;
            ImageFacebook.IsVisible = false;
            ImageInstagram.IsVisible = false;
            ImageLinkedIn.IsVisible = false;
            ImagePhone.IsVisible = false;
            ImageSnapchat.IsVisible = false;
            ImageSpotify.IsVisible = false;
            ImageTelegram.IsVisible = false;
            ImageTiktok.IsVisible = false;
            ImageTwitch.IsVisible = false;
            ImageTwitter.IsVisible = true;
            ImageWeb.IsVisible = false;
            ImageWhatsapp.IsVisible = false;
            ImageYoutube.IsVisible = false;
            #endregion
        }

        private void WebPageProfile_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Profiles.GetListSM(10);

            #region Listas
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
            #endregion

            #region Buttons
            ButtonEmail.IsVisible = false;
            ButtonFacebook.IsVisible = false;
            ButtonInstagram.IsVisible = false;
            ButtonLinkedin.IsVisible = false;
            ButtonPhone.IsVisible = false;
            ButtonSnapchat.IsVisible = false;
            ButtonSpotify.IsVisible = false;
            ButtonTelegram.IsVisible = false;
            ButtonTiktok.IsVisible = false;
            ButtonTwitch.IsVisible = false;
            ButtonTwitter.IsVisible = false;
            ButtonWebPage.IsVisible = true;
            ButtonWhatsapp.IsVisible = false;
            ButtonYoutube.IsVisible = false;
            #endregion

            #region Image
            ImageEmail.IsVisible = false;
            ImageFacebook.IsVisible = false;
            ImageInstagram.IsVisible = false;
            ImageLinkedIn.IsVisible = false;
            ImagePhone.IsVisible = false;
            ImageSnapchat.IsVisible = false;
            ImageSpotify.IsVisible = false;
            ImageTelegram.IsVisible = false;
            ImageTiktok.IsVisible = false;
            ImageTwitch.IsVisible = false;
            ImageTwitter.IsVisible = false;
            ImageWeb.IsVisible = true;
            ImageWhatsapp.IsVisible = false;
            ImageYoutube.IsVisible = false;
            #endregion
        }

        private void WhatsAppProfile_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Profiles.GetListWhatsapp();

            #region Listas
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
            #endregion

            #region Buttons
            ButtonEmail.IsVisible = false;
            ButtonFacebook.IsVisible = false;
            ButtonInstagram.IsVisible = false;
            ButtonLinkedin.IsVisible = false;
            ButtonPhone.IsVisible = false;
            ButtonSnapchat.IsVisible = false;
            ButtonSpotify.IsVisible = false;
            ButtonTelegram.IsVisible = false;
            ButtonTiktok.IsVisible = false;
            ButtonTwitch.IsVisible = false;
            ButtonTwitter.IsVisible = false;
            ButtonWebPage.IsVisible = false;
            ButtonWhatsapp.IsVisible = true;
            ButtonYoutube.IsVisible = false;
            #endregion

            #region Image
            ImageEmail.IsVisible = false;
            ImageFacebook.IsVisible = false;
            ImageInstagram.IsVisible = false;
            ImageLinkedIn.IsVisible = false;
            ImagePhone.IsVisible = false;
            ImageSnapchat.IsVisible = false;
            ImageSpotify.IsVisible = false;
            ImageTelegram.IsVisible = false;
            ImageTiktok.IsVisible = false;
            ImageTwitch.IsVisible = false;
            ImageTwitter.IsVisible = false;
            ImageWeb.IsVisible = false;
            ImageWhatsapp.IsVisible = true;
            ImageYoutube.IsVisible = false;
            #endregion
        }

        private void YoutubeProfile_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Profiles.GetListSM(7);

            #region Listas
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
            #endregion

            #region Buttons
            ButtonEmail.IsVisible = false;
            ButtonFacebook.IsVisible = false;
            ButtonInstagram.IsVisible = false;
            ButtonLinkedin.IsVisible = false;
            ButtonPhone.IsVisible = false;
            ButtonSnapchat.IsVisible = false;
            ButtonSpotify.IsVisible = false;
            ButtonTelegram.IsVisible = false;
            ButtonTiktok.IsVisible = false;
            ButtonTwitch.IsVisible = false;
            ButtonTwitter.IsVisible = false;
            ButtonWebPage.IsVisible = false;
            ButtonWhatsapp.IsVisible = false;
            ButtonYoutube.IsVisible = true;
            #endregion

            #region Image
            ImageEmail.IsVisible = false;
            ImageFacebook.IsVisible = false;
            ImageInstagram.IsVisible = false;
            ImageLinkedIn.IsVisible = false;
            ImagePhone.IsVisible = false;
            ImageSnapchat.IsVisible = false;
            ImageSpotify.IsVisible = false;
            ImageTelegram.IsVisible = false;
            ImageTiktok.IsVisible = false;
            ImageTwitch.IsVisible = false;
            ImageTwitter.IsVisible = false;
            ImageWeb.IsVisible = false;
            ImageWhatsapp.IsVisible = false;
            ImageYoutube.IsVisible = true;
            #endregion
        }

        private void TelegramProfile_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Profiles.GetListSM(11);

            #region Listas
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
            #endregion

            #region Buttons
            ButtonEmail.IsVisible = false;
            ButtonFacebook.IsVisible = false;
            ButtonInstagram.IsVisible = false;
            ButtonLinkedin.IsVisible = false;
            ButtonPhone.IsVisible = false;
            ButtonSnapchat.IsVisible = false;
            ButtonSpotify.IsVisible = false;
            ButtonTelegram.IsVisible = true;
            ButtonTiktok.IsVisible = false;
            ButtonTwitch.IsVisible = false;
            ButtonTwitter.IsVisible = false;
            ButtonWebPage.IsVisible = false;
            ButtonWhatsapp.IsVisible = false;
            ButtonYoutube.IsVisible = false;
            #endregion

            #region Image
            ImageEmail.IsVisible = false;
            ImageFacebook.IsVisible = false;
            ImageInstagram.IsVisible = false;
            ImageLinkedIn.IsVisible = false;
            ImagePhone.IsVisible = false;
            ImageSnapchat.IsVisible = false;
            ImageSpotify.IsVisible = false;
            ImageTelegram.IsVisible = true;
            ImageTiktok.IsVisible = false;
            ImageTwitch.IsVisible = false;
            ImageTwitter.IsVisible = false;
            ImageWeb.IsVisible = false;
            ImageWhatsapp.IsVisible = false;
            ImageYoutube.IsVisible = false;
            #endregion
        }
        #endregion

        #region Button Plus
        private void ButtonEmail_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().CreateProfileEmail = new CreateProfileEmailViewModel();
            App.Navigator.PushAsync(new CreateProfileEmailPage());
        }

        private void ButtonPhone_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().CreateProfilePhone = new CreateProfilePhoneViewModel();
            App.Navigator.PushAsync(new CreateProfilePhonePage());
        }

        private void ButtonSM_Clicked(object sender, EventArgs e, string Type)
        {
            MainViewModel.GetInstance().CreateProfileSM = new CreateProfileSMViewModel(Type);
            switch(Type)
            {
                case "Facebook":
                    App.Navigator.PushAsync(new CreateProfileFacebookPage());
                    break;
                case "Instagram":
                    App.Navigator.PushAsync(new CreateProfileInstagramPage());
                    break;
                case "Twitter":
                    App.Navigator.PushAsync(new CreateProfileTwitterPage());
                    break;
                case "Snapchat":
                    App.Navigator.PushAsync(new CreateProfileSnapchatPage());
                    break;
                case "Linkedin":
                    App.Navigator.PushAsync(new CreateProfileLinkedinPage());
                    break;
                case "Spotify":
                    App.Navigator.PushAsync(new CreateProfileSpotifyPage());
                    break;
                case "Twitch":
                    App.Navigator.PushAsync(new CreateProfileTwitchPage());
                    break;
                case "Youtube":
                    App.Navigator.PushAsync(new CreateProfileYoutubePage());
                    break;
                case "Tiktok":
                    App.Navigator.PushAsync(new CreateProfileTiktokPage());
                    break;                
                case "WebPage":
                    App.Navigator.PushAsync(new CreateProfileWebPagePage());
                    break;
                case "Telegram":
                    App.Navigator.PushAsync(new CreateProfileTelegramPage());
                    break;
                default:
                    break;
            }
        }

        private void ButtonWhatsapp_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().CreateProfileWhatsApp = new CreateProfileWhatsAppViewModel();
            App.Navigator.PushAsync(new CreateProfileWhatsAppPage());
        }
        #endregion

         void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = e.CurrentSelection.FirstOrDefault() as ProfileEmail;
            if (selectedItem == null)
                return;
            MainViewModel.GetInstance().EditProfileEmail = new EditProfileEmailViewModel(selectedItem.ProfileEmailId);
            App.Navigator.PushAsync(new EditProfileEmailPage());
            if(MainViewModel.GetInstance().Profiles.IsUp == false)
            {
                EmailList.SelectedItem = null;
            }                                              
        }
        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            ProfileEmail tappedItemEmail = e.Item as ProfileEmail;
            ProfilePhone tappedItemPhone = e.Item as ProfilePhone;
            ProfileSM tappedItemSM = e.Item as ProfileSM;
            ProfileWhatsapp tappedItemWhatsapp = e.Item as ProfileWhatsapp;

            if (tappedItemEmail != null)
            {
                MainViewModel.GetInstance().EditProfileEmail = new EditProfileEmailViewModel(tappedItemEmail.ProfileEmailId);
                App.Navigator.PushAsync(new EditProfileEmailPage());
            }

            else if (tappedItemPhone != null)
            {
                MainViewModel.GetInstance().EditProfilePhone = new EditProfilePhoneViewModel(tappedItemPhone.ProfilePhoneId);
                App.Navigator.PushAsync(new EditProfilePhonePage(tappedItemPhone.ProfilePhoneId));
                tappedItemPhone = null;
            }

            else if (tappedItemSM != null)
            {
                switch(tappedItemSM.RedSocialId)
                {
                    case 1:
                        MainViewModel.GetInstance().EdithProfile = new EdithProfileViewModel(tappedItemSM.ProfileMSId);
                        App.Navigator.PushAsync(new EditProfileFacebookPage());
                        tappedItemSM = null;
                        break;
                    case 2:
                        MainViewModel.GetInstance().EdithProfile = new EdithProfileViewModel(tappedItemSM.ProfileMSId);
                        App.Navigator.PushAsync(new EditProfileInstagramPage());
                        tappedItemSM = null;
                        break;
                    case 3:
                        MainViewModel.GetInstance().EdithProfile = new EdithProfileViewModel(tappedItemSM.ProfileMSId);
                        App.Navigator.PushAsync(new EditProfileTwitterPage());
                        tappedItemSM = null;
                        break;
                    case 4:
                        MainViewModel.GetInstance().EdithProfile = new EdithProfileViewModel(tappedItemSM.ProfileMSId);
                        App.Navigator.PushAsync(new EditProfileSnapchatPage());
                        tappedItemSM = null;
                        break;
                    case 5:
                        MainViewModel.GetInstance().EdithProfile = new EdithProfileViewModel(tappedItemSM.ProfileMSId);
                        App.Navigator.PushAsync(new EditProfileLinkedinPage());
                        tappedItemSM = null;
                        break;
                    case 6:
                        MainViewModel.GetInstance().EdithProfile = new EdithProfileViewModel(tappedItemSM.ProfileMSId);
                        App.Navigator.PushAsync(new EditProfileTiktokPage());
                        tappedItemSM = null;
                        break;
                    case 7:
                        MainViewModel.GetInstance().EdithProfile = new EdithProfileViewModel(tappedItemSM.ProfileMSId);
                        App.Navigator.PushAsync(new EditProfileYoutubePage());
                        tappedItemSM = null;
                        break;
                    case 8:
                        MainViewModel.GetInstance().EdithProfile = new EdithProfileViewModel(tappedItemSM.ProfileMSId);
                        App.Navigator.PushAsync(new EditProfileSpotifyPage());
                        tappedItemSM = null;
                        break;
                    case 9:
                        MainViewModel.GetInstance().EdithProfile = new EdithProfileViewModel(tappedItemSM.ProfileMSId);
                        App.Navigator.PushAsync(new EditProfileTwitchPage());
                        tappedItemSM = null;
                        break;
                    case 10:
                        MainViewModel.GetInstance().EdithProfile = new EdithProfileViewModel(tappedItemSM.ProfileMSId);
                        App.Navigator.PushAsync(new EditProfileWebPagePage());
                        tappedItemSM = null;
                        break;
                    case 11:
                        MainViewModel.GetInstance().EdithProfile = new EdithProfileViewModel(tappedItemSM.ProfileMSId);
                        App.Navigator.PushAsync(new EditProfileTelegramPage());
                        tappedItemSM = null;
                        break;
                    default:
                        break;
                }
                
            }

            else if (tappedItemWhatsapp != null)
            {
                MainViewModel.GetInstance().EditProfileWhatsApp = new EditProfileWhatsAppViewModel(tappedItemWhatsapp.ProfileWhatsappId);
                App.Navigator.PushAsync(new EditProfileWhatsAppPage());
                tappedItemWhatsapp = null;
            }
            var a = tappedItemEmail;
        }
        #endregion
    }
}