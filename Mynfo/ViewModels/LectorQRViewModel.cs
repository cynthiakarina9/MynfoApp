namespace Mynfo.ViewModels
{
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Models;
    using Mynfo.Services;
    using Mynfo.Views;
    using Plugin.Media;
    using Rg.Plugins.Popup.Services;
    using System;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using ZXing;

    public class LectorQRViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isScanning;
        private Result qrCode;
        private Box Box;
        #endregion

        #region Properties
        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
            set
            {
                SetValue(ref this.isRunning, value);
            }
        }
        public bool IsScanning
        {
            get
            {
                return this.isScanning;
            }
            set
            {
                SetValue(ref this.isScanning, value);
            }
        }
        public Result QrCode
        {
            get
            {
                return this.qrCode;
            }
            set
            {
                SetValue(ref this.qrCode, value);
            }
        }
        #endregion

        #region Constructor
        public LectorQRViewModel()
        {
            apiService = new ApiService();
            IsScanning = true;
            CrossMedia.Current.Initialize();
        }
        #endregion

        #region Methods
        public void OnScanResult(Result result)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
            {
                IsScanning = false;
                string a = result.Text;
                string[] b = a.Split('=', '&');
                await MainViewModel.GetInstance().LectorQR.GetBoxDefault(Convert.ToInt32(b[1]));
                result = null;
            });
            IsScanning = true;
        }

        public async Task<Box> GetBoxDefault(int id)
        {
            if(id == null || id ==0)
            {
                await Application.Current.MainPage.DisplayAlert(
                   Languages.Error,
                   "El usuario no es valido",
                   Languages.Accept);
                return null;
            }
            Box = new Box();
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return null;
            }

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var UserFornaneo = await this.apiService.GetUserId(
                apiSecurity,
                "/api",
                "/Users/",
                id);
            if(UserFornaneo == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "El usuario no es valido",
                    Languages.Accept);
                return null;
            }
            var BoxL = await this.apiService.GetBoxDefault<Box>(
                apiSecurity,
                "/api",
                "/Boxes/GetBoxDefault",
                id);
            if (BoxL == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "El usuario no tiene boxes",
                    Languages.Accept);
                return null;
            }

            int UserIdToSend = UserFornaneo.UserId;

            //Perfil predeterminado, que es el perfil de Mynfo y box predeterminada de Ese perfil
            if (UserFornaneo.Share != true) 
            { 
                UserIdToSend = 1;
                BoxL = await apiService.GetBoxDefault<Box>(
                apiSecurity,
                "/api",
                "/Boxes/GetBoxDefault",
                UserIdToSend);

                if (BoxL == null)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        "El usuario no tiene boxes",
                        Languages.Accept);
                    return null;
                }
            }

            Imprime_box.InsertForeignData(UserIdToSend, BoxL.BoxId);
            return Box;
        }
        #endregion
    }
}
