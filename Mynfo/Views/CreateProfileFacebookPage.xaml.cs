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
    public partial class CreateProfileFacebookPage : ContentPage
    {
        ApiService apiService;
        public CreateProfileFacebookPage(bool _BoxDefault = false, int _boxId = 0)
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

            SaveWBox.Clicked += new EventHandler((sender, e) => backToAssignProfiles(sender, e, _boxId, ProfileName.Text, ProfileLink.Text, _BoxDefault));
           //BackButtonBox.Clicked += new EventHandler((sender, e) => BackBox_Clicked(sender, e, _boxId, _BoxDefault));
        }
        private async void backToAssignProfiles(object sender, EventArgs e, int _BoxId, string _profileName, string _profileLink, bool _BoxDefault)
        {
            if (string.IsNullOrEmpty(this.ProfileName.Text))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.NameValidation,
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.ProfileLink.Text))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.LinkValidation,
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
            string query = "insert into dbo.ProfileSMs(ProfileName, link, UserId, RedSocialId)" +
                            "Values('" + _profileName + "', '" + _profileLink + "', " + MainViewModel.GetInstance().User.UserId + ",1)";

            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            StringBuilder sb;

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
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

            Application.Current.MainPage = new NavigationPage(new ProfilesBYPESMPage(_BoxId, "Facebook", _BoxDefault));
        }
        private void Back_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByFacebook = new ProfilesByFacebookViewModel();
            Application.Current.MainPage = new NavigationPage(new ProfilesByFacebookPage());
        }
        private void BackBox_Clicked(object sender, EventArgs e, int _BoxId, bool _boxDefault)
        {
            Application.Current.MainPage = new NavigationPage(new ProfilesBYPESMPage(_BoxId, "Facebook", _boxDefault));
        }
    }
}