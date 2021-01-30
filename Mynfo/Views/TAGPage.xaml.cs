namespace Mynfo.Views
{
    using Mynfo.Helpers;
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
    }
}