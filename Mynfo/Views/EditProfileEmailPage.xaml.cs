namespace Mynfo.Views
{
    using Mynfo.Domain;
    using Mynfo.Helpers;
    using Mynfo.Services;
    using System;
    using System.Data.SqlClient;
    using System.Text;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfileEmailPage : ContentPage
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        ProfileEmail profileEmail = new ProfileEmail();
        int UserID = MainViewModel.GetInstance().User.UserId;
        #endregion

        #region Construtor
        public EditProfileEmailPage(int _ProfileEmailId)
        {
            //var mainViewModel = MainViewModel.GetInstance();
            //mainViewModel.EditProfileEmail = new EditProfileEmailViewModel(_ProfileEmailId);
            InitializeComponent();
            //apiService
            apiService = new ApiService();
            System.Text.StringBuilder sb;
            string consultaEmail = "select * from dbo.ProfileEmails where dbo.ProfileEmails.ProfileEmailId =" + _ProfileEmailId +"and dbo.ProfileEmails.UserId = "+ UserID;
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            

            //Seleccionar ProfileEmail
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(consultaEmail);

                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            profileEmail.UserId = (int)reader["UserId"];
                            profileEmail.ProfileEmailId = (int)reader["ProfileEmailId"];
                            profileEmail.Name = (string)reader["Name"];
                            profileEmail.Email = (string)reader["Email"];
                        }
                    }

                    connection.Close();
                }

                EntryEmail.Text = profileEmail.Email;
                EntryName.Text = profileEmail.Name;
            }
        }
        #endregion

        #region Commands
        private async void Save_Clicked(object sender, EventArgs e)
        {
            ButtonSave.IsEnabled = false;
            if (string.IsNullOrEmpty(EntryName.Text))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NameProfile,
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(EntryEmail.Text))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation,
                    Languages.Accept);
                return;
            }

            if (!RegexUtilities.IsValidEmail(EntryEmail.Text))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation2,
                    Languages.Accept);
                return;
            }
            var checkConnetion = await this.apiService.CheckConnection();
            if (!checkConnetion.IsSuccess)
            {
                //this.IsRunning = false;
                ButtonSave.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    checkConnetion.Message,
                    Languages.Accept);
                return;
            }

            string queryUpdateProfileEmail = "update dbo.ProfileEmails set Name = '" + EntryName.Text + "', Email = '" + EntryEmail.Text + "' where dbo.ProfileEmails.ProfileEmailId = " + profileEmail.ProfileEmailId + " and dbo.ProfileEmails.UserId = " + profileEmail.UserId;
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            StringBuilder sb;
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(queryUpdateProfileEmail);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            Application.Current.MainPage = new NavigationPage(new ProfilesByEmailPage());
        }
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            ButtonSave.IsEnabled = false;
            var checkConnetion = await this.apiService.CheckConnection();
            if (!checkConnetion.IsSuccess)
            {
                //this.IsRunning = false;
                ButtonSave.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    checkConnetion.Message,
                    Languages.Accept);
                return;
            }

            string SelectBoxEmail = "delete from dbo.Box_ProfileEmail where dbo.Box_ProfileEmail.ProfileEmailId = " + profileEmail.ProfileEmailId;
            string SelectProfileEmail = "delete from dbo.ProfileEmails where dbo.ProfileEmails.ProfileEmailId = " + profileEmail.ProfileEmailId;
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
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(SelectProfileEmail);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            Application.Current.MainPage = new NavigationPage(new ProfilesByEmailPage());
        }
        private void Back_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByEmail = new ProfilesByEmailViewModel();
            Application.Current.MainPage = new NavigationPage(new ProfilesByEmailPage());
        }
        #endregion
    }
}