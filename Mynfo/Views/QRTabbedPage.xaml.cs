namespace Mynfo.Views
{
    using Mynfo.Helpers;
    using Mynfo.ViewModels;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Xamarin.Forms;
    using Xamarin.Forms.PlatformConfiguration;
    using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
    using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
    using Xamarin.Forms.Xaml;
    using ZXing;
    using ZXing.Mobile;
    using ZXing.Net.Mobile.Forms;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRTabbedPage : INotifyPropertyChanged
    {
        private string _result;
        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }
        public QRTabbedPage()
        {
            InitializeComponent();
            var options = new MobileBarcodeScanningOptions();
            options.PossibleFormats = new List<BarcodeFormat>
            {
                BarcodeFormat.QR_CODE,
                BarcodeFormat.CODE_128,
                BarcodeFormat.EAN_13,
            };

            #region TabbedPage
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.MyQR = new MyQRViewModel();
            mainViewModel.LectorQR = new LectorQRViewModel();
            var page = new ZXingScannerPage(options) { Title = Languages.EscanQR };
            page.IsScanning = true;
            On<Windows>().SetHeaderIconsEnabled(true);
            On<Windows>().SetHeaderIconsSize(new Size(50, 50));

            Children.Add(new MyQRPage { Title = Languages.MyQR });
            Children.Add(new LectorQRPage { Title = Languages.EscanQR });

            CurrentPage = Children[0];
            #endregion
            #region Lector

                //page.OnScanResult += (result) =>
                //{
                //    page.IsScanning = false;
                //    Device.BeginInvokeOnMainThread(() =>
                //    {
                //        App.Current.MainPage.Navigation.PopModalAsync();
                //        if (string.IsNullOrEmpty(result.Text))
                //        {
                //            Result = "No valid code has been scanned";
                //        }
                //        else
                //        {
                //            Result = $"Result: {result.Text}";
                //        }
                //    });
                //};
            
            //App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(page) { BarTextColor = Color.White, BarBackgroundColor = Color.CadetBlue }, true);
            #endregion

        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}