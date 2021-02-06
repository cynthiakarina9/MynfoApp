namespace Mynfo.Views
{
    using Mynfo.Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using ViewModels;
    using Xamarin.Essentials;
    using Xamarin.Forms;
    using Xamarin.Forms.OpenWhatsApp;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForeingBoxPage : ContentPage
    {
        #region Properties
        public string data = Data_ntc.data_value;
        public ProfileLocal selectedItem { get; set; }
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


            ForeingBox foreing = _foreingBox;

            ForeignUserImage.Source = foreing.ImageFullPath;
            ForeignUserName.Text = foreing.FullName;


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
        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = e.CurrentSelection.FirstOrDefault() as ProfileLocal;
            if (selectedItem == null)
                return;
            switch (selectedItem.ProfileType)
            {
                case "Email":
                    await Launcher.OpenAsync(new Uri("mailto:" + selectedItem.value));
                    break;

                case "Instagram":
                    await Launcher.OpenAsync(new Uri(selectedItem.value));
                    break;
                case "Facebook":
                    await Launcher.OpenAsync(new Uri(selectedItem.value));
                    break;
                case "LinkedIn":
                    await Launcher.OpenAsync(new Uri(selectedItem.value));
                    break;
                case "Phone":
                    PhoneDialer.Open(selectedItem.value);
                    break;
                case "Snapchat":
                    await Launcher.OpenAsync(new Uri(selectedItem.value));
                    break;
                case "Spotify":
                    await Launcher.OpenAsync(new Uri(selectedItem.value));
                    break;
                case "TikTok":
                    await Launcher.OpenAsync(new Uri(selectedItem.value));
                    break;
                case "Twitch":
                    await Launcher.OpenAsync(new Uri(selectedItem.value));
                    break;
                case "Twitter":
                    await Launcher.OpenAsync(new Uri(selectedItem.value));
                    break;
                case "WebPage":
                    await Launcher.OpenAsync(new Uri(selectedItem.value));
                    break;
                case "Whatsapp":
                    try
                    {
                        Chat.Open("+52" + selectedItem.value, "Hola un gusto. Soy " + MainViewModel.GetInstance().User.FullName + ", te comparto este mensaje por Mynfo!");
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", ex.Message, "OK");
                    }
                    break;
                case "Youtube":
                    await Launcher.OpenAsync(new Uri(selectedItem.value));
                    break;
                default:
                    break;
            }
            ((CollectionView)sender).SelectedItem = null;
        }        
        #endregion
    }
}