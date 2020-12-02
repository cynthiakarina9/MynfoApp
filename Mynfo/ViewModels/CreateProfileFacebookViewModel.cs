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

    public class CreateProfileFacebookViewModel : BaseViewModel
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
        public string Link
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public CreateProfileFacebookViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        #region Commands
        public ICommand SaveProfileFacebookCommand
        {
            get
            {
                return new RelayCommand(SaveProfileFacebook);
            }
        }

        private async void SaveProfileFacebook()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NameValidation,
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.Link))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.LinkValidation,
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

            var profileFB = new ProfileSM
            {
                ProfileName = this.Name,
                link = this.Link,
                UserId = mainViewModel.User.UserId,
                RedSocialId = 1,
            };

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var response = await this.apiService.Post(
                apiSecurity,
                "/api",
                "/ProfileSMs",
                profileFB);

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
            this.Link = string.Empty;

            string consultaDefault = "SELECT Top 1 * FROM dbo.ProfileSMs where dbo.ProfileSMs.UserId = "
                                        + MainViewModel.GetInstance().User.UserId +
                                        " ORDER BY dbo.ProfileSMs.ProfileMSId DESC";
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            ProfileSM _profileSM = new ProfileSM();

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
                            _profileSM.ProfileMSId = (int)reader["ProfileMSId"];
                            _profileSM.ProfileName = (string)reader["ProfileName"];
                            _profileSM.UserId = (int)reader["UserId"];
                            _profileSM.link = (string)reader["link"];
                            _profileSM.RedSocialId = (int)reader["RedSocialId"];
                        }
                    }
                    connection.Close();
                }
            }

            //Agregar a la lista
            MainViewModel.GetInstance().ProfilesByFacebook.addProfile(_profileSM);

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
