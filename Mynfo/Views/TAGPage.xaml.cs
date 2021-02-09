namespace Mynfo.Views
{
    using Mynfo.Helpers;
    using System;
    using Xamarin.Essentials;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;    
    using Foundation;    

    [Register("AppDelegate")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TAGPage : ContentPage
    {
        public static bool write_nfc = false;

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

            var duration = TimeSpan.FromMilliseconds(1000);
            if (Device.RuntimePlatform == Device.iOS)
            {                            
                Vibration.Vibrate(duration);
                DependencyService.Get<IBackgroundDependency>().ExecuteCommand();
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                System.Threading.Tasks.Task task = App.DisplayAlertAsync("Acerca tu tag para escribir");                               
                Vibration.Vibrate(duration);
                write_nfc = true;
            }
        }
        public interface IBackgroundDependency
        {
            void ExecuteCommand();
        }
    }
}