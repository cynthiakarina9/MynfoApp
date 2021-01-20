namespace Mynfo.Views
{
    using Mynfo.Helpers;
    using Mynfo.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Xamarin.Forms;
    using Xamarin.Forms.PlatformConfiguration;
    using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRTabbedPage
    {
        public QRTabbedPage()
        {
            InitializeComponent();
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.MyQR = new MyQRViewModel();
            mainViewModel.LectorQR = new LectorQRViewModel();

            On<Windows>().SetHeaderIconsEnabled(true);
            On<Windows>().SetHeaderIconsSize(new Size(50, 50));

            Children.Add(new MyQRPage { Title = Languages.MyQR });
            Children.Add(new LectorQRPage { Title = Languages.EscanQR });
            
            CurrentPage = Children[0];
        }
    }
}