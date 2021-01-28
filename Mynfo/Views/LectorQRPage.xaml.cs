using Mynfo.Domain;
using Mynfo.Helpers;
using Mynfo.Models;
using Mynfo.Services;
using Mynfo.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace Mynfo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LectorQRPage : ZXingScannerPage
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        Box Box;
        #endregion
        public LectorQRPage()
        {
            apiService = new ApiService();
            InitializeComponent();
        }
        public void scanView_OnScanResult(Result result)
        {
            MainViewModel.GetInstance().LectorQR.OnScanResult(result);
            //scanView.IsScanning = false;
            //Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
            //{
            //    await MainViewModel.GetInstance().LectorQR.GetBoxDefault(Convert.ToInt32(result.Text));
            //    //await DisplayAlert("Scanned result", "The barcode's text is " + result.Text + ". The barcode's format is " + result.BarcodeFormat, "OK");
            //});
        }
    }
}