namespace Mynfo.ViewModels
{
    using Domain;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Mynfo.Views;
    using Services;
    using System.Data.SqlClient;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class CreateProfileEmailViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Atributtes
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
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
        public string Name
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public CreateProfileEmailViewModel()
        {
            this.apiService = new ApiService();

            this.IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand SaveProfileEmailCommand
        {
            get
            {
                return new RelayCommand(SaveProfileEmail);
            }
        }
        
        private async void SaveProfileEmail()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NameValidation,
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation,
                    Languages.Accept);
                return;
            }
            if (!RegexUtilities.IsValidEmail(this.Email))
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

            var mainViewModel = MainViewModel.GetInstance();

            var profileEmail = new ProfileEmail
            {
                Name = this.Name,
                Email = this.Email,
                UserId = mainViewModel.User.UserId,
            };

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var response = await this.apiService.Post(
                apiSecurity,
                "/api",
                "/ProfileEmails",
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

            this.Name = string.Empty;
            this.Email = string.Empty;

            string consultaDefault = "SELECT Top 1 * FROM dbo.ProfileEmails where dbo.ProfileEmails.UserId = "
                                        + MainViewModel.GetInstance().User.UserId +
                                        " ORDER BY dbo.ProfileEmails.ProfileEmailId DESC";
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
            MainViewModel.GetInstance().ProfilesByEmail.addProfile(_profileEmail);

            await App.Navigator.PopAsync(); 

            /*MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();*/
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
