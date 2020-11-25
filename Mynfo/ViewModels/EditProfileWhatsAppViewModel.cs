namespace Mynfo.ViewModels
{
    using Domain;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Services;
    using System;
    using System.Data.SqlClient;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class EditProfileWhatsAppViewModel : BaseViewModel
    {

        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public ProfileEmail profileEmail
        {
            get;
            set;
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        #endregion

        #region Constructor
        public EditProfileWhatsAppViewModel( int _ProfileEmailId = 0)
        {
            //var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            //ProfileEmail EmailProfile = new ProfileEmail();
            //EmailProfile.ProfileEmailId = _ProfileEmailId;
            //var EmailProfile2 = this.apiService.Get(
            //    apiSecurity,
            //    "/api",
            //    "/ProfileEmails",
            //    MainViewModel.GetInstance().Token.TokenType,
            //    MainViewModel.GetInstance().Token.AccessToken,
            //    EmailProfile.ProfileEmailId);// profileEmail.ProfileEmailId);
            //ProfileEmail EmailLocal = EmailProfile;
            this.apiService = new ApiService();
            this.isEnabled = true;
        }
        #endregion

        #region Commands

        public ICommand DeleteProfileCommand
        {
            get
            {
                return new RelayCommand(DeleteProfile);
            }
        }

        private async void DeleteProfile()
        {

            this.IsRunning = true;
            this.IsEnabled = false;

            var checkConnetion = await this.apiService.CheckConnection();
            if (!checkConnetion.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    checkConnetion.Message,
                    Languages.Accept);
                return;
            }
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            string SelectBoxEmail = "delete from dbo.Box_ProfileEmail where dbo.Box_ProfileEmail.ProfileEmailId = " + profileEmail.ProfileEmailId;
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            StringBuilder sb;
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(SelectBoxEmail);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            
            var response = await this.apiService.Delete(
                apiSecurity,
                "/api",
                "/ProfileEmails",
                MainViewModel.GetInstance().Token.TokenType,
                MainViewModel.GetInstance().Token.AccessToken,
                profileEmail);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            this.IsRunning = false;
            this.IsEnabled = true;

            await App.Navigator.PopAsync();
        }
        #endregion

    }
}