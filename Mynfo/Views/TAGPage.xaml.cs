namespace Mynfo.Views
{
    using Mynfo.Helpers;
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
    }
}