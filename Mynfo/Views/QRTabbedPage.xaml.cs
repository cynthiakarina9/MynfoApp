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
        public QRTabbedPage()
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

            #region TabbedPage
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.MyQR = new MyQRViewModel();
            mainViewModel.LectorQR = new LectorQRViewModel();
            On<Windows>().SetHeaderIconsEnabled(true);
            On<Windows>().SetHeaderIconsSize(new Size(50, 50));

            if (Device.RuntimePlatform == Device.iOS)
            {
                Children.Add(new MyQRPage { Title = Languages.MyQR });
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                Children.Add(new MyQRPage { Title = Languages.MyQR });
                Children.Add(new LectorQRPage { Title = Languages.EscanQR });
            }


            CurrentPage = Children[0];
            #endregion
        }
    }
}