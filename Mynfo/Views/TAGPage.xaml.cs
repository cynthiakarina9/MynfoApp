namespace Mynfo.Views
{
    //using CoreNFC;
    //using Foundation;
    using Mynfo.Helpers;
    using Rg.Plugins.Popup.Extensions;
    using Rg.Plugins.Popup.Services;
    using System;
    using System.Threading;
    using Xamarin.Essentials;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;    
    //using Foundation;    

    //[Register("AppDelegate")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TAGPage : ContentPage
    {
        #region Attributes
        private Label uno = new Label();
        #endregion
        public static bool write_nfc = false;

        public TAGPage()
        {
            InitializeComponent();
            OSAppTheme currentTheme = App.Current.RequestedTheme;
            if (currentTheme == OSAppTheme.Dark)
            {
                Logosuperior.Source = "logo_superior2.png";
            }
            else
            {
                Logosuperior.Source = "logo_superior3.png";
            }

            uno.Text = Languages.ConfigureTAG;
            uno.TextColor = Color.FromHex("#FF5521");
            uno.FontSize = 22;
            Press.Text = Languages.Push + " '" + uno.Text + "' " + Languages.AndStick;
        }
        void escribir_tag(object sender, EventArgs e)
        {
            try
            {
                //Navigation.PushPopupAsync(new RedyToScan());

                var duration = TimeSpan.FromMilliseconds(1000);
                if (Device.RuntimePlatform == Device.iOS)
                {
                    Vibration.Vibrate(duration);
                    DependencyService.Get<IBackgroundDependency>().ExecuteCommand();
                }
                else if (Device.RuntimePlatform == Device.Android)
                {
                    App.Navigator.PushAsync(new RedyToScan());
                    Vibration.Vibrate(duration);
                    write_nfc = true;
                }
            }
            catch (Exception e2) 
            {
                Console.WriteLine(e2);
            }
        }
        public interface IBackgroundDependency
        {
            void ExecuteCommand();
        }
    }
}