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

    public class EditProfileEmailViewModel : BaseViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Attributes
        private ProfileEmail profilemail;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public ProfileEmail profileEmail
        {
            get { return profilemail; }
            private set
            {
                SetValue(ref profilemail, value);
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
        public EditProfileEmailViewModel(int _ProfileEmailId)
        {
            apiService = new ApiService();
            GetProfileEmail( _ProfileEmailId);
        }
        #endregion

        #region Commands
        private async Task<ProfileEmail> GetProfileEmail(int _ProfileEmailId)
        {
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            profileEmail = new ProfileEmail();
            profileEmail = await this.apiService.GetProfileEmail(
               apiSecurity,
               "/api",
               "/ProfileEmails/GetProfileEmail",
               _ProfileEmailId);
            return profileEmail;
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

            if (string.IsNullOrEmpty(this.profileEmail.Name))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.LastNameValidation,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.profileEmail.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation,
                    Languages.Accept);
                return;
            }

            if (!RegexUtilities.IsValidEmail(this.profileEmail.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation2,
                    Languages.Accept);
                return;
            }

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
            var response = await this.apiService.PutProfile(
                apiSecurity,
                "/api",
                "/ProfileEmails/PutProfileEmail",
                profileEmail);

            this.IsRunning = false;
            this.IsEnabled = true;

            string consultaDefault = "select * from dbo.ProfileEmails where dbo.ProfileEmails.ProfileEmailId = "
                                        + profileEmail.ProfileEmailId ;
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            
            ProfileEmail _profileEmail = new ProfileEmail();

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(consultaDefault);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            _profileEmail.ProfileEmailId = (int)reader["ProfileEmailId"];
                            _profileEmail.Name = (string)reader["Name"];
                            _profileEmail.UserId = (int)reader["UserId"];
                            _profileEmail.Email = (string)reader["Email"];
                        }
                    }
                    connection.Close();
                }
            }

            //Agregar a la lista
            MainViewModel.GetInstance().ProfilesByEmail.updateProfile(_profileEmail);

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
                "/Box_ProfileEmail",
                profileEmail.ProfileEmailId);

            var response2 = await this.apiService.Delete(
                apiSecurity,
                "/api",
                "/ProfileEmails",
                profileEmail.ProfileEmailId);

            this.IsRunning = false;
            this.IsEnabled = true;

            MainViewModel.GetInstance().ProfilesByEmail.removeProfile();

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
