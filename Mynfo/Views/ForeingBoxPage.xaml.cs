namespace Mynfo.Views
{
    using Mynfo.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using Xamarin.Essentials;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using Xamarin.Forms.OpenWhatsApp;
    using ViewModels;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForeingBoxPage : ContentPage
    {
        #region Properties
        public string data = Data_ntc.data_value;
        #endregion

        #region Constructor
        public ForeingBoxPage(ForeingBox _foreingBox, bool isAfterReceiving = false)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            #region Button
            //Creación del botón para volver a boxes foraneas
            var bxBtnBack = new ImageButton();
            bxBtnBack.BackgroundColor = Color.Transparent;
            bxBtnBack.Source = "back.png";
            bxBtnBack.WidthRequest = 50;
            bxBtnBack.HeightRequest = 50;
            bxBtnBack.Clicked += new EventHandler((sender, e) => Back_Clicked(sender, e, isAfterReceiving));
            BackButton.Children.Add(bxBtnBack);
            #endregion

            #region DataFill

            List<ForeingProfile> foreingProfileList = new List<ForeingProfile>();

            ForeingBox foreing = _foreingBox;

            ForeignUserImage.Source = foreing.ImageFullPath;
            ForeignUserName.Text = foreing.FullName;

            int listProfileNum = 0;

            using (var conn = new SQLite.SQLiteConnection(App.root_db))
            {
                foreingProfileList = conn.Query<ForeingProfile>("Select * from ForeingProfile where ForeingProfile.BoxId = ?", _foreingBox.BoxId);
            }

            foreach(ForeingProfile foreingProfileValue in foreingProfileList)
            {
                var profileIcon = new ImageButton();
                var profileName = new Label();
                //var deleteProfile = new ImageButton();
                var space = new BoxView();

                switch(foreingProfileValue.ProfileType)
                {
                    case "Phone":
                        profileIcon.Source = "tel2.png";
                        profileIcon.WidthRequest = 50;
                        profileIcon.HeightRequest = 50;
                        profileIcon.HorizontalOptions = LayoutOptions.Center;
                        profileIcon.IsEnabled = true;
                        profileIcon.BackgroundColor = Color.Transparent;
                        profileIcon.Clicked += new EventHandler((sender, e) => GoToProfile(sender, e, foreingProfileValue.ProfileType, foreingProfileValue.value));

                        profileName.Text = foreingProfileValue.ProfileName;
                        profileName.FontSize = 15;
                        profileName.HorizontalTextAlignment = TextAlignment.Center;
                        profileName.FontAttributes = FontAttributes.Bold;
                        profileName.TextColor = Color.Black;

                        space.HeightRequest = 30;
                        break;

                    case "Email":

                        profileIcon.Source = "mail2.png";
                        profileIcon.WidthRequest = 50;
                        profileIcon.HeightRequest = 50;
                        profileIcon.HorizontalOptions = LayoutOptions.Center;
                        profileIcon.IsEnabled = true;
                        profileIcon.BackgroundColor = Color.Transparent;
                        profileIcon.Clicked += new EventHandler((sender, e) => GoToProfile(sender, e, foreingProfileValue.ProfileType, foreingProfileValue.value));

                        profileName.Text = foreingProfileValue.ProfileName;
                        profileName.FontSize = 15;
                        profileName.HorizontalTextAlignment = TextAlignment.Center;
                        profileName.FontAttributes = FontAttributes.Bold;
                        profileName.TextColor = Color.Black;

                        space.HeightRequest = 30;
                        break;
                    case "Facebook":
                        profileIcon.Source = "facebook2.png";
                        profileIcon.WidthRequest = 50;
                        profileIcon.HeightRequest = 50;
                        profileIcon.HorizontalOptions = LayoutOptions.Center;
                        profileIcon.IsEnabled = true;
                        profileIcon.BackgroundColor = Color.Transparent;
                        profileIcon.Clicked += new EventHandler((sender, e) => GoToProfile(sender, e, foreingProfileValue.ProfileType, foreingProfileValue.value));

                        profileName.Text = foreingProfileValue.ProfileName;
                        profileName.FontSize = 15;
                        profileName.HorizontalTextAlignment = TextAlignment.Center;
                        profileName.FontAttributes = FontAttributes.Bold;
                        profileName.TextColor = Color.Black;

                        space.HeightRequest = 30;
                        break;
                    case "Instagram":
                        profileIcon.Source = "instagramlogo2.png";
                        profileIcon.WidthRequest = 50;
                        profileIcon.HeightRequest = 50;
                        profileIcon.HorizontalOptions = LayoutOptions.Center;
                        profileIcon.IsEnabled = true;
                        profileIcon.BackgroundColor = Color.Transparent;
                        profileIcon.Clicked += new EventHandler((sender, e) => GoToProfile(sender, e, foreingProfileValue.ProfileType, foreingProfileValue.value));

                        profileName.Text = foreingProfileValue.ProfileName;
                        profileName.FontSize = 15;
                        profileName.HorizontalTextAlignment = TextAlignment.Center;
                        profileName.FontAttributes = FontAttributes.Bold;
                        profileName.TextColor = Color.Black;

                        space.HeightRequest = 30;
                        break;
                    case "LinkedIn":
                        profileIcon.Source = "linkedin2.png";
                        profileIcon.WidthRequest = 50;
                        profileIcon.HeightRequest = 50;
                        profileIcon.HorizontalOptions = LayoutOptions.Center;
                        profileIcon.IsEnabled = true;
                        profileIcon.BackgroundColor = Color.Transparent;
                        profileIcon.Clicked += new EventHandler((sender, e) => GoToProfile(sender, e, foreingProfileValue.ProfileType, foreingProfileValue.value));

                        profileName.Text = foreingProfileValue.ProfileName;
                        profileName.FontSize = 15;
                        profileName.HorizontalTextAlignment = TextAlignment.Center;
                        profileName.FontAttributes = FontAttributes.Bold;
                        profileName.TextColor = Color.Black;

                        space.HeightRequest = 30;
                        break;
                    case "TikTok":
                        profileIcon.Source = "tiktok2.png";
                        profileIcon.WidthRequest = 50;
                        profileIcon.HeightRequest = 50;
                        profileIcon.HorizontalOptions = LayoutOptions.Center;
                        profileIcon.IsEnabled = true;
                        profileIcon.BackgroundColor = Color.Transparent;
                        profileIcon.Clicked += new EventHandler((sender, e) => GoToProfile(sender, e, foreingProfileValue.ProfileType, foreingProfileValue.value));

                        profileName.Text = foreingProfileValue.ProfileName;
                        profileName.FontSize = 15;
                        profileName.HorizontalTextAlignment = TextAlignment.Center;
                        profileName.FontAttributes = FontAttributes.Bold;
                        profileName.TextColor = Color.Black;

                        space.HeightRequest = 30;
                        break;
                    case "Twitch":
                        profileIcon.Source = "twitch2.png";
                        profileIcon.WidthRequest = 50;
                        profileIcon.HeightRequest = 50;
                        profileIcon.HorizontalOptions = LayoutOptions.Center;
                        profileIcon.IsEnabled = true;
                        profileIcon.BackgroundColor = Color.Transparent;
                        profileIcon.Clicked += new EventHandler((sender, e) => GoToProfile(sender, e, foreingProfileValue.ProfileType, foreingProfileValue.value));

                        profileName.Text = foreingProfileValue.ProfileName;
                        profileName.FontSize = 15;
                        profileName.HorizontalTextAlignment = TextAlignment.Center;
                        profileName.FontAttributes = FontAttributes.Bold;
                        profileName.TextColor = Color.Black;

                        space.HeightRequest = 30;
                        break;
                    case "Twitter":
                        profileIcon.Source = "twitterlogo2.png";
                        profileIcon.WidthRequest = 50;
                        profileIcon.HeightRequest = 50;
                        profileIcon.HorizontalOptions = LayoutOptions.Center;
                        profileIcon.IsEnabled = true;
                        profileIcon.BackgroundColor = Color.Transparent;
                        profileIcon.Clicked += new EventHandler((sender, e) => GoToProfile(sender, e, foreingProfileValue.ProfileType, foreingProfileValue.value));

                        profileName.Text = foreingProfileValue.ProfileName;
                        profileName.FontSize = 15;
                        profileName.HorizontalTextAlignment = TextAlignment.Center;
                        profileName.FontAttributes = FontAttributes.Bold;
                        profileName.TextColor = Color.Black;

                        space.HeightRequest = 30;
                        break;
                    case "Snapchat":
                        profileIcon.Source = "snapchat2.png";
                        profileIcon.WidthRequest = 50;
                        profileIcon.HeightRequest = 50;
                        profileIcon.HorizontalOptions = LayoutOptions.Center;
                        profileIcon.IsEnabled = true;
                        profileIcon.BackgroundColor = Color.Transparent;
                        profileIcon.Clicked += new EventHandler((sender, e) => GoToProfile(sender, e, foreingProfileValue.ProfileType, foreingProfileValue.value));

                        profileName.Text = foreingProfileValue.ProfileName;
                        profileName.FontSize = 15;
                        profileName.HorizontalTextAlignment = TextAlignment.Center;
                        profileName.FontAttributes = FontAttributes.Bold;
                        profileName.TextColor = Color.Black;

                        space.HeightRequest = 30;
                        break;
                    case "Spotify":
                        profileIcon.Source = "spotify2.png";
                        profileIcon.WidthRequest = 50;
                        profileIcon.HeightRequest = 50;
                        profileIcon.HorizontalOptions = LayoutOptions.Center;
                        profileIcon.IsEnabled = true;
                        profileIcon.BackgroundColor = Color.Transparent;
                        profileIcon.Clicked += new EventHandler((sender, e) => GoToProfile(sender, e, foreingProfileValue.ProfileType, foreingProfileValue.value));

                        profileName.Text = foreingProfileValue.ProfileName;
                        profileName.FontSize = 15;
                        profileName.HorizontalTextAlignment = TextAlignment.Center;
                        profileName.FontAttributes = FontAttributes.Bold;
                        profileName.TextColor = Color.Black;

                        space.HeightRequest = 30;
                        break;
                    case "WebPage":
                        profileIcon.Source = "gmail2.png";
                        profileIcon.WidthRequest = 50;
                        profileIcon.HeightRequest = 50;
                        profileIcon.HorizontalOptions = LayoutOptions.Center;
                        profileIcon.IsEnabled = true;
                        profileIcon.BackgroundColor = Color.Transparent;
                        profileIcon.Clicked += new EventHandler((sender, e) => GoToProfile(sender, e, foreingProfileValue.ProfileType, foreingProfileValue.value));

                        profileName.Text = foreingProfileValue.ProfileName;
                        profileName.FontSize = 15;
                        profileName.HorizontalTextAlignment = TextAlignment.Center;
                        profileName.FontAttributes = FontAttributes.Bold;
                        profileName.TextColor = Color.Black;

                        space.HeightRequest = 30;
                        break;
                    case "Whatsapp":
                        profileIcon.Source = "whatsapp2.png";
                        profileIcon.WidthRequest = 50;
                        profileIcon.HeightRequest = 50;
                        profileIcon.HorizontalOptions = LayoutOptions.Center;
                        profileIcon.IsEnabled = true;
                        profileIcon.BackgroundColor = Color.Transparent;
                        profileIcon.Clicked += new EventHandler((sender, e) => GoToProfile(sender, e, foreingProfileValue.ProfileType, foreingProfileValue.value));

                        profileName.Text = foreingProfileValue.ProfileName;
                        profileName.FontSize = 15;
                        profileName.HorizontalTextAlignment = TextAlignment.Center;
                        profileName.FontAttributes = FontAttributes.Bold;
                        profileName.TextColor = Color.Black;

                        space.HeightRequest = 30;
                        break;
                    case "Youtube":
                        profileIcon.Source = "youtube2.png";
                        profileIcon.WidthRequest = 50;
                        profileIcon.HeightRequest = 50;
                        profileIcon.HorizontalOptions = LayoutOptions.Center;
                        profileIcon.IsEnabled = true;
                        profileIcon.BackgroundColor = Color.Transparent;
                        profileIcon.Clicked += new EventHandler((sender, e) => GoToProfile(sender, e, foreingProfileValue.ProfileType, foreingProfileValue.value));

                        profileName.Text = foreingProfileValue.ProfileName;
                        profileName.FontSize = 15;
                        profileName.HorizontalTextAlignment = TextAlignment.Center;
                        profileName.FontAttributes = FontAttributes.Bold;
                        profileName.TextColor = Color.Black;

                        space.HeightRequest = 30;
                        break;
                    default:
                        break;
                }

                //Asignación de perfiles en columnas
                switch (listProfileNum)
                {
                    case 0:
                        listProfileNum = 2;

                        ProfilesList1.Children.Add(profileIcon);
                        ProfilesList1.Children.Add(profileName);
                        ProfilesList1.Children.Add(space);
                        //ProfilesList1.Children.Add(deleteProfile);
                        break;

                    case 1:
                        listProfileNum = 2;

                        ProfilesList1.Children.Add(profileIcon);
                        ProfilesList1.Children.Add(profileName);
                        ProfilesList1.Children.Add(space);
                        // ProfilesList1.Children.Add(deleteProfile);
                        break;

                    case 2:
                        listProfileNum = 3;

                        ProfilesList2.Children.Add(profileIcon);
                        ProfilesList2.Children.Add(profileName);
                        ProfilesList2.Children.Add(space);
                        //ProfilesList2.Children.Add(deleteProfile);
                        break;

                    case 3:
                        listProfileNum = 1;

                        ProfilesList3.Children.Add(profileIcon);
                        ProfilesList3.Children.Add(profileName);
                        ProfilesList3.Children.Add(space);
                        //ProfilesList3.Children.Add(deleteProfile);
                        break;

                    default:
                        break;
                }

            }

            #endregion
        }
        #endregion

        #region Command
        private void Back_Clicked(object sender, EventArgs e, bool isAfterReceiving)
        {
            if (isAfterReceiving == true)
            {

                Application.Current.MainPage = new MasterPage();
            }
            else
            {
                Navigation.PopAsync();
            }
        }

        async private void GoToProfile(object sender, EventArgs e, string _profileType, string _profileValue)
        {
            switch(_profileType)
            {
                case "Email":
                    await Launcher.OpenAsync(new Uri("mailto:" + _profileValue));
                    break;

                case "Instagram":
                    await Launcher.OpenAsync(new Uri( _profileValue));
                    break;
                case "Facebook":
                    await Launcher.OpenAsync(new Uri(_profileValue));
                    break;
                case "LinkedIn":
                    await Launcher.OpenAsync(new Uri(_profileValue));
                    break;
                case "Phone":
                    PhoneDialer.Open(_profileValue);
                    break;
                case "Snapchat":
                    await Launcher.OpenAsync(new Uri(_profileValue));
                    break;
                case "Spotify":
                    await Launcher.OpenAsync(new Uri(_profileValue));
                    break;
                case "TikTok":
                    await Launcher.OpenAsync(new Uri(_profileValue));
                    break;
                case "Twitch":
                    await Launcher.OpenAsync(new Uri(_profileValue));
                    break;
                case "Twitter":
                    await Launcher.OpenAsync(new Uri(_profileValue));
                    break;
                case "WebPage":
                    await Launcher.OpenAsync(new Uri(_profileValue));
                    break;
                case "Whatsapp":
                    try
                    {
                        Chat.Open("+52" + _profileValue, "Hola un gusto. Soy " + MainViewModel.GetInstance().User.FullName + ", te comparto este mensaje por Mynfo!");
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", ex.Message, "OK");
                    }
                    break;
                case "Youtube":
                    await Launcher.OpenAsync(new Uri(_profileValue));
                    break;
                default:
                    break;
            } 
            //await Launcher.OpenAsync(new Uri("fb://page/100000686899395"));
            //await Launcher.OpenAsync(new Uri("https://twitter.com/RToachee"));
            //await Launcher.OpenAsync(new Uri("instagram:page_id//user?username=rodritoachee"));
            //await Launcher.OpenAsync(new Uri("mailto:rrodriguez@atx.com"));
        }        
        #endregion
    }
}