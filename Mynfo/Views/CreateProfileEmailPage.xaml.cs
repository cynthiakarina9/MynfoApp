namespace Mynfo.Views
{
    using Mynfo.ViewModels;
    using System;
    using System.Data.SqlClient;
    using System.Text;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateProfileEmailPage : ContentPage
    {
        public CreateProfileEmailPage(bool _boxDefault = false, int _boxId = 0)
        {
            InitializeComponent();

            if (_boxId == 0)
            {
                SaveWBox.IsVisible = false;
                BackButtonBox.IsVisible = false;
            }
            else
            {
                Save.IsVisible = false;
                BackButton.IsVisible = false;
            }

            SaveWBox.Clicked += new EventHandler((sender, e) => backToAssignProfiles(sender, e, _boxId, ProfileName.Text, ProfileMail.Text, _boxDefault));
            BackButtonBox.Clicked += new EventHandler((sender, e) => BackBox_Clicked(sender, e, _boxId, _boxDefault));
        }
        private void backToAssignProfiles(object sender, EventArgs e, int _BoxId, string _profileName, string _profileMail, bool _boxDefault)
        {
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
                        DisplayAlert("Atención", "El correo que se desea guardar ya se guardó previamente", "Ok");
                    }
                }
            }

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
        private void BackHome_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Home = new HomeViewModel();
            Application.Current.MainPage = new MasterPage();
        }
    }
}