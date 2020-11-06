using Mynfo.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mynfo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateProfileEmailPage : ContentPage
    {
        public CreateProfileEmailPage(bool _boxDefault = false, int _boxId = 0)
        {
            InitializeComponent();

            if (_boxId == 0)
            {
                SaveWBox.IsVisible = false;
            }
            else
            {
                Save.IsVisible = false;
            }

            SaveWBox.Clicked += new EventHandler((sender, e) => backToAssignProfiles(sender, e, _boxId, ProfileName.Text, ProfileMail.Text, _boxDefault));

        }
        private void backToAssignProfiles(object sender, EventArgs e, int _BoxId, string _profileName, string _profileMail, bool _boxDefault)
        {
            string query = "insert into dbo.ProfileEmails( Name, Email, UserId)" +
                            "Values('" + _profileName + "', '" + _profileMail + "', " + MainViewModel.GetInstance().User.UserId + ")";

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

            Application.Current.MainPage = new NavigationPage(new ProfilesBYPESMPage(_BoxId, "Email", _boxDefault));
        }
        private void Back_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProfilesByEmail = new ProfilesByEmailViewModel();
            Application.Current.MainPage = new NavigationPage(new ProfilesByEmailPage());
        }
    }
}