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
    public partial class CreateProfilePhonePage : ContentPage
    {
        ApiService apiService;
        public CreateProfilePhonePage(bool _boxDefault = false, int _boxId = 0)
        {
            InitializeComponent();
            apiService = new ApiService();

            /*if(_boxId == 0)
            {
                SaveWBox.IsVisible = false;
                //BackButtonBox.IsVisible = false;
            }
            else
            {
                Save.IsVisible = false;
                BackButton.IsVisible = false;
            }

            SaveWBox.Clicked += new EventHandler((sender, e) => backToAssignProfiles(sender, e, _boxId,ProfileName.Text,ProfileNumber.Text, _boxDefault));
            BackButtonBox.Clicked += new EventHandler((sender, e) => BackBox_Clicked(sender, e, _boxId, _boxDefault));
        */
        }

        private async void backToAssignProfiles(object sender, EventArgs e, int _BoxId, string _profileName, string _profileNumber,bool _boxDefault)
        {
            if (string.IsNullOrEmpty(this.ProfileName.Text))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NameValidation,
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.ProfileNumber.Text))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NumberValidation,
                    Languages.Accept);
                return;
            }
            if (this.ProfileNumber.Text.Length != 10)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PhoneValidation2,
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
            string query = "insert into dbo.ProfilePhones ( Name, Number, UserId)" +
                            "Values('" + _profileName + "', '" + _profileNumber + "', " + MainViewModel.GetInstance().User.UserId + ")";

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
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            ActivityIn.IsRunning = false;
            Save.IsEnabled = true;
            //BackButton.IsEnabled = true;

            Application.Current.MainPage = new NavigationPage(new ProfilesBYPESMPage(_BoxId, "Phone", _boxDefault));
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByPhone = new ProfilesByPhoneViewModel();
            Application.Current.MainPage = new NavigationPage(new ProfilesByPhonePage());
        }
        private void BackBox_Clicked(object sender, EventArgs e, int _BoxId, bool _boxDefault)
        {
            Application.Current.MainPage = new NavigationPage(new ProfilesBYPESMPage(_BoxId, "Phone", _boxDefault));
        }
        private void BackHome_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }
    }
}