namespace Mynfo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Views;
    using Services;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class EditProfilePhoneViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        private ProfilePhone profilephone;
        #endregion

        #region Properties
        public ProfilePhone profilePhone
        {
            get { return profilephone; }
            private set
            {
                SetValue(ref profilephone, value);
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
        public EditProfilePhoneViewModel(int _ProfilePhoneId)
        {
            apiService = new ApiService();
            GetProfilePhone(_ProfilePhoneId);
            this.isEnabled = true;
        }
        #endregion

        #region Commands
        private async Task<ProfilePhone> GetProfilePhone(int _ProfilePhoneId)
        {
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            profilePhone = new ProfilePhone();
            profilePhone = await this.apiService.GetProfilePhone(
               apiSecurity,
               "/api",
               "/ProfilePhones/GetProfilePhone",
               _ProfilePhoneId);
            return profilePhone;
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
                "/ProfilePhones/PutProfilePhone",
                profilePhone);

            this.IsRunning = false;
            this.IsEnabled = true;

            #region LastCode2
            //string consultaDefault = "select * from dbo.ProfilePhones where dbo.ProfilePhones.ProfilePhoneId = "
            //                            + profilephone.ProfilePhoneId;
            //string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";

            //ProfilePhone _profilePhone = new ProfilePhone();

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
            //                _profilePhone.ProfilePhoneId = (int)reader["ProfilePhoneId"];
            //                _profilePhone.Name = (string)reader["Name"];
            //                _profilePhone.UserId = (int)reader["UserId"];
            //                _profilePhone.Number = (string)reader["Number"];
            //            }
            //        }
            //        connection.Close();
            //    }
            //}
            #endregion


            MainViewModel.GetInstance().ProfilesByPhone.updateProfile(profile);

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
                "/Box_ProfilePhone/DeleteBox_ProfilePhoneRelations",
                profilePhone.ProfilePhoneId);

            var response2 = await this.apiService.Delete(
                apiSecurity,
                "/api",
                "/ProfilePhones",
                profilePhone.ProfilePhoneId);

            this.IsRunning = false;
            this.IsEnabled = true;

            MainViewModel.GetInstance().ProfilesByPhone.removeProfile();

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
