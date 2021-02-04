namespace Mynfo.Views
{
    using Mynfo.ViewModels;
    using Xamarin.Forms.Xaml;
    using ZXing;
    using ZXing.Net.Mobile.Forms;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LectorQRPage : ZXingScannerPage
    {
        public LectorQRPage()
        {
            InitializeComponent();
        }
        public void scanView_OnScanResult(Result result)
        {
            MainViewModel.GetInstance().LectorQR.OnScanResult(result);
        }
    }
}