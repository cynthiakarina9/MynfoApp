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
    public partial class EditProfilePhonePage : ContentPage
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        ProfilePhone profilePhone = new ProfilePhone();
        int UserID = MainViewModel.GetInstance().User.UserId;
        #endregion
        #region Constructor
        public EditProfilePhonePage(int _ProfilePhoneId)
        {
            //var mainViewModel = MainViewModel.GetInstance();
            //mainViewModel.EditProfileEmail = new EditProfileEmailViewModel(_ProfileEmailId);
            apiService = new ApiService();
            InitializeComponent();
            
            //apiService
            
            System.Text.StringBuilder sb;
            string consultaPhone = "select * from dbo.ProfilePhones where dbo.ProfilePhones.ProfilePhoneId =" + _ProfilePhoneId + "and dbo.ProfilePhones.UserId = " + UserID;
            string cadenaConexion = @"data source=serverappmynfo.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";


            //Seleccionar ProfileEmail
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(consultaPhone);

                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            profilePhone.UserId = (int)reader["UserId"];
                            profilePhone.ProfilePhoneId = (int)reader["ProfilePhoneId"];
                            profilePhone.Name = (string)reader["Name"];
                            profilePhone.Number = (string)reader["Number"];
                        }
                    }

                    connection.Close();
                }
                EntryName.Text = profilePhone.Name;
                EntryPhone.Text = profilePhone.Number;
            }
            #endregion
        }

        #region Commands
        private async void Save_Clicked(object sender, EventArgs e)
        {
            ButtonDelete.IsEnabled = false;
            ButtonSave.IsEnabled = false;
            if (string.IsNullOrEmpty(EntryName.Text))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NameProfile,
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(EntryPhone.Text))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation,
                    Languages.Accept);
                return;
            }
            var checkConnetion = await this.apiService.CheckConnection();
            if (!checkConnetion.IsSuccess)
            {
                //this.IsRunning = false;
                ButtonSave.IsEnabled = true;
                ButtonSave.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    checkConnetion.Message,
                    Languages.Accept);
                return;
            }

            string queryUpdateProfileEmail = "update dbo.ProfilePhones set Name = '" + EntryName.Text + "', Number = '" + EntryPhone.Text + "' where dbo.ProfilePhones.ProfilePhoneId = " + profilePhone.ProfilePhoneId + " and dbo.ProfilePhones.UserId = " + profilePhone.UserId;
            string cadenaConexion = @"data source=serverappmynfo.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";
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
            await App.Navigator.PopAsync();
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            ButtonSave.IsEnabled = false;
            ButtonDelete.IsEnabled = false;
            var checkConnetion = await this.apiService.CheckConnection();
            if (!checkConnetion.IsSuccess)
            {
                //this.IsRunning = false;
                ButtonDelete.IsEnabled = true;
                ButtonSave.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    checkConnetion.Message,
                    Languages.Accept);
                return;
            }

            string SelectBoxPhone = "delete from dbo.Box_ProfilePhone where dbo.Box_ProfilePhone.ProfilePhoneId = " + profilePhone.ProfilePhoneId;
            string SelectProfilePhone = "delete from dbo.ProfilePhones where dbo.ProfilePhones.ProfilePhoneId = " + profilePhone.ProfilePhoneId;
            string cadenaConexion = @"data source=serverappmynfo.database.windows.net;initial catalog=mynfo;user id=adminmynfo;password=4dmiNFC*Atx2020;Connect Timeout=60";
            StringBuilder sb;
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(SelectBoxPhone);
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
                sb.Append(SelectProfilePhone);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            Application.Current.MainPage = new NavigationPage(new ProfilesByPhonePage());
        }
        private void Back_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByPhone = new ProfilesByPhoneViewModel();
            Application.Current.MainPage = new NavigationPage(new ProfilesByPhonePage());
        }
        #endregion
    }
}