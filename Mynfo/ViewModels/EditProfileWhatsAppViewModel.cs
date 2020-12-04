namespace Mynfo.ViewModels
{
    using Domain;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Services;
    using Views;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;
    using System.Data.SqlClient;

    public class EditProfileWhatsAppViewModel : BaseViewModel
    {

        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        private ProfileWhatsapp profilewhats;
        #endregion

        #region Properties
        public ProfileWhatsapp profileWhats
        {
            get { return profilewhats; }
            private set
            {
                SetValue(ref profilewhats, value);
            }
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
        public EditProfileWhatsAppViewModel( int _ProfileMSId)
        {
            this.apiService = new ApiService();
            GetProfile(_ProfileMSId);
            this.isEnabled = true;
        }
        #endregion

        #region Commands
        private async Task<ProfileWhatsapp> GetProfile(int _ProfileMSId)
        {
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            profileWhats = new ProfileWhatsapp();
            profileWhats = await this.apiService.GetProfileWhatsApp(
               apiSecurity,
               "/api",
               "/ProfileWhatsapps/GetProfileWhatsApp",
               _ProfileMSId);
            return profileWhats;
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
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
            var profile = await this.apiService.PutProfile(
                apiSecurity,
                "/api",
                "/ProfileWhatsapps/PutProfileWhatsapp",
                profileWhats);

            this.IsRunning = false;
            this.IsEnabled = true;

            #region LastCode2
            //string consultaDefault = "select * from dbo.ProfileWhatsapps where dbo.ProfileWhatsapps.ProfileWhatsappId = "
            //                            + profilewhats.ProfileWhatsappId;
            //string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";

            //ProfileWhatsapp _profileWhatsapp = new ProfileWhatsapp();

            //using (SqlConnection connection = new SqlConnection(cadenaConexion))
            //{
            //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //    sb.Append(consultaDefault);
            //    string sql = sb.ToString();

            //    using (SqlCommand command = new SqlCommand(sql, connection))
            //    {
            //        connection.Open();
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                _profileWhatsapp.ProfileWhatsappId = (int)reader["ProfileWhatsappId"];
            //                _profileWhatsapp.Name = (string)reader["Name"];
            //                _profileWhatsapp.UserId = (int)reader["UserId"];
            //                _profileWhatsapp.Number = (string)reader["Number"];
            //            }
            //        }
            //        connection.Close();
            //    }
            //}
            #endregion


            //Agregar a la lista
            MainViewModel.GetInstance().ProfilesByWhatsApp.updateProfile(profile);

            await App.Navigator.PopAsync();
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(Delete);
            }
        }

        private async void Delete()
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
            var response = await this.apiService.Delete(
                apiSecurity,
                "/api",
                "/Box_ProfileWhatsapp",
                profileWhats.ProfileWhatsappId);

            var response2 = await this.apiService.Delete(
                apiSecurity,
                "/api",
                "/ProfileWhatsapps",
                profileWhats.ProfileWhatsappId);

            this.IsRunning = false;
            this.IsEnabled = true;

            MainViewModel.GetInstance().ProfilesByWhatsApp.removeProfile();

            await App.Navigator.PopAsync();
        }

        public ICommand BackHomeCommand
        {
            get
            {
                return new RelayCommand(BackHome);
            }
        }

        private async void BackHome()
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }
        #endregion

    }
}