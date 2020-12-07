namespace Mynfo.Views
{
    using Mynfo.Helpers;
    using Mynfo.Services;
    using Mynfo.ViewModels;
    using System;
    using System.Data.SqlClient;
    using System.Text;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateProfileEmailPage : ContentPage
    {
        ApiService apiService;
        public CreateProfileEmailPage(bool _boxDefault = false, int _boxId = 0)
        {
            InitializeComponent();
            apiService = new ApiService();
            if (_boxId == 0)
            {
                SaveWBox.IsVisible = false;
                //BackButtonBox.IsVisible = false;
            }
            else
            {
                Save.IsVisible = false;
                //BackButton.IsVisible = false;
            }

            SaveWBox.Clicked += new EventHandler((sender, e) => backToAssignProfiles(sender, e, _boxId, ProfileName.Text, ProfileMail.Text, _boxDefault));
            //BackButtonBox.Clicked += new EventHandler((sender, e) => BackBox_Clicked(sender, e, _boxId, _boxDefault));
        }
        private async void backToAssignProfiles(object sender, EventArgs e, int _BoxId, string _profileName, string _profileMail, bool _boxDefault)
        {
            if (string.IsNullOrEmpty(this.ProfileName.Text))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NameValidation,
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.ProfileMail.Text))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation,
                    Languages.Accept);
                return;
            }
            if (!RegexUtilities.IsValidEmail(this.ProfileMail.Text))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation2,
                    Languages.Accept);
                return;
            }

            ActivityIn.IsRunning = true;
            Save.IsEnabled = false;
            //BackButton.IsEnabled = false;

            var checkConnetion = await this.apiService.CheckConnection();
            if (!checkConnetion.IsSuccess)
            {
                ActivityIn.IsRunning = false;
                Save.IsEnabled = true;
                //BackButton.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    checkConnetion.Message,
                    Languages.Accept);
                return;
            }
            string query = "insert into dbo.ProfileEmails( Name, Email, UserId)" +
                            "Values('" + _profileName + "', '" + _profileMail + "', " + MainViewModel.GetInstance().User.UserId + ")";

            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            StringBuilder sb;

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                StringBuilder errorMessages = new StringBuilder();
                sb = new System.Text.StringBuilder();
                sb.Append(query);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (SqlException ex)
                    {
                        await DisplayAlert("Atención", "El correo que se desea guardar ya se guardó previamente", "Ok");
                    }
                }
            }

            ActivityIn.IsRunning = false;
            Save.IsEnabled = true;
            //BackButton.IsEnabled = true;

            Application.Current.MainPage = new NavigationPage(new ProfilesBYPESMPage(_BoxId, "Email", _boxDefault));
        }
        private void Back_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByEmail = new ProfilesByEmailViewModel();
            Application.Current.MainPage = new NavigationPage(new ProfilesByEmailPage());
        }
        private void BackBox_Clicked(object sender, EventArgs e, int _BoxId, bool _boxDefault)
        {
            Application.Current.MainPage = new NavigationPage(new ProfilesBYPESMPage(_BoxId, "Email", _boxDefault));
        }
    }
}