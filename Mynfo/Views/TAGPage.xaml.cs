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
        public TAGPage()
        {
            InitializeComponent();
            Label uno = new Label();
            uno.Text = Languages.ConfigureTAG;
            uno.TextColor = Color.FromHex("#FF5521");
            Press.Text = Languages.Push + " '" + uno.Text + "' " + Languages.AndStick;            
        }
        void escribir_tag(object sender, EventArgs e)
        {
            System.Threading.Tasks.Task task = App.DisplayAlertAsync("Acerca tu tag para escribir");
            var duration = TimeSpan.FromMilliseconds(500);
            Vibration.Vibrate(duration);
            Vibration.Vibrate(duration);
            SettingsPage.write_nfc = true;
        }
    }
}