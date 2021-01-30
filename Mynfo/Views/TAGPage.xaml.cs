namespace Mynfo.Views
{
    using Mynfo.Helpers;
    using System;
    using Xamarin.Essentials;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TAGPage : ContentPage
    {
        #region Attributes
        private Label uno = new Label();
        #endregion
        public TAGPage()
        {
            InitializeComponent();
            uno.Text = Languages.ConfigureTAG;
            uno.TextColor = Color.FromHex("#FF5521");
            uno.FontSize = 22;
            Press.Text = Languages.Push + " '" + uno.Text + "' " + Languages.AndStick;
        }
        void escribir_tag(object sender, EventArgs e)
        {
            var duration = TimeSpan.FromMilliseconds(500);
            Vibration.Vibrate(duration);
            Vibration.Vibrate(duration);
            SettingsPage.write_nfc = true;
        }
    }
}