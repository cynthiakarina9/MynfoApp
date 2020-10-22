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
    public partial class ProfilesBYPESMPage : ContentPage
    {
        public ProfilesBYPESMPage(int _BoxId)
        {
            InitializeComponent();

            int BoxId = _BoxId;
            int UserId = MainViewModel.GetInstance().User.UserId;

            string queryGetEmailProfiles = "select dbo.ProfileEmails.ProfileEmailId from dbo.ProfileEmails " +
                                            "where dbo.ProfileEmails.UserId = " + UserId + " except select " +
                                            "dbo.Box_ProfileEmail.ProfileEmailId from dbo.Box_ProfileEmail";
            string queryGetPhoneProfiles = "SELECT dbo.ProfilePhones.ProfilePhoneId FROM dbo.ProfilePhones "+
                                            "where dbo.ProfilePhones.UserId = " + UserId + " EXCEPT SELECT " +
                                            "dbo.Box_ProfilePhone.ProfilePhoneId FROM dbo.Box_ProfilePhone";

            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            System.Text.StringBuilder sb, sb2;

            //Consulta para obtener teléfonos
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(queryGetPhoneProfiles);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int ProfilePhoneId = (int)reader["ProfilePhoneId"];
                            string queryGetProfile = "select * from dbo.ProfilePhones where dbo.ProfilePhones.ProfilePhoneId = " + ProfilePhoneId;

                            //Validamos los valores que obtenemos
                            using (SqlConnection connection2 = new SqlConnection(cadenaConexion))
                            {
                                sb2 = new System.Text.StringBuilder();
                                sb2.Append(queryGetProfile);
                                string sql2 = sb2.ToString();

                                using (SqlCommand command2 = new SqlCommand(sql2, connection2))
                                {
                                    connection2.Open();
                                    using (SqlDataReader reader2 = command2.ExecuteReader())
                                    {
                                        while (reader2.Read())
                                        {
                                            var phoneName = new Label();
                                            var phoneNumber = new Label();
                                            var CreateRelation = new Button();
                                            var Line = new BoxView();

                                            phoneName.Text = (string)reader2["Name"];
                                            phoneName.FontSize = 15;
                                            phoneName.FontAttributes = FontAttributes.Bold;

                                            phoneNumber.Text = (string)reader2["Number"];
                                            phoneNumber.FontSize = 25;
                                            phoneNumber.HorizontalTextAlignment = TextAlignment.Center;

                                            CreateRelation.Text = "+";
                                            CreateRelation.TextColor = Color.Black;
                                            CreateRelation.FontSize = 10;
                                            CreateRelation.BackgroundColor = Color.FromHex("#f9a589");
                                            CreateRelation.CornerRadius = 15;
                                            CreateRelation.HeightRequest = 30;
                                            CreateRelation.WidthRequest = 30;
                                            CreateRelation.HorizontalOptions = LayoutOptions.End;
                                            CreateRelation.Clicked += new EventHandler((sender, e) => CreateBoxPhoneRelation(sender, e, BoxId, ProfilePhoneId));

                                            Line.HeightRequest = 1;
                                            Line.Color = Color.FromHex("#FF5521");

                                            ProfileList.Children.Add(phoneName);
                                            ProfileList.Children.Add(phoneNumber);
                                            ProfileList.Children.Add(CreateRelation);
                                            ProfileList.Children.Add(Line);
                                        }
                                    }
                                    connection2.Close();
                                }
                            }
                        }
                    }
                    connection.Close();
                }
            }

            //Consulta para obtener Emails
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(queryGetEmailProfiles);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int ProfileEmailId = (int)reader["ProfileEmailId"];
                            string queryGetProfile = "select * from dbo.ProfileEmails where dbo.ProfileEmails.ProfileEmailId = " + ProfileEmailId;

                            //Validamos los valores que obtenemos
                            using (SqlConnection connection2 = new SqlConnection(cadenaConexion))
                            {
                                sb2 = new System.Text.StringBuilder();
                                sb2.Append(queryGetProfile);
                                string sql2 = sb2.ToString();

                                using (SqlCommand command2 = new SqlCommand(sql2, connection2))
                                {
                                    connection2.Open();
                                    using (SqlDataReader reader2 = command2.ExecuteReader())
                                    {
                                        while (reader2.Read())
                                        {
                                            var emailProfile = new Label();
                                            var emailAddress = new Label();
                                            var CreateRelation = new Button();
                                            var Line = new BoxView();

                                            emailProfile.Text = (string)reader2["Name"];
                                            emailProfile.FontSize = 15;
                                            emailProfile.FontAttributes = FontAttributes.Bold;

                                            emailAddress.Text = (string)reader2["Email"];
                                            emailAddress.FontSize = 25;
                                            emailAddress.HorizontalTextAlignment = TextAlignment.Center;

                                            CreateRelation.Text = "+";
                                            CreateRelation.TextColor = Color.Black;
                                            CreateRelation.FontSize = 10;
                                            CreateRelation.BackgroundColor = Color.FromHex("#f9a589");
                                            CreateRelation.CornerRadius = 15;
                                            CreateRelation.HeightRequest = 30;
                                            CreateRelation.WidthRequest = 30;
                                            CreateRelation.HorizontalOptions = LayoutOptions.End;
                                            CreateRelation.Clicked += new EventHandler((sender, e) => CreateBoxEmailRelation(sender, e, BoxId, ProfileEmailId));

                                            Line.HeightRequest = 1;
                                            Line.Color = Color.FromHex("#FF5521");

                                            ProfileList.Children.Add(emailProfile);
                                            ProfileList.Children.Add(emailAddress);
                                            ProfileList.Children.Add(CreateRelation);
                                            ProfileList.Children.Add(Line);
                                        }
                                    }
                                    connection2.Close();
                                }
                            }
                        }
                    }
                    connection.Close();
                }
            }
        }
        private async void GoToProfiles_Clicked(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Profiles = new ProfilesViewModel();
            await Navigation.PushAsync(new ProfilesPage());
        }

        private void CreateBoxEmailRelation(object sender, EventArgs e, int _BoxId, int _EmailId)
        {
            //Crear la relación de la box con el correo
            string queryCreateEmailRelation = "INSERT INTO dbo.Box_ProfileEmail ( BoxId, ProfileEmailId) " +
                                         "VALUES(" + _BoxId + ","+ _EmailId + ")";
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            StringBuilder sb;

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(queryCreateEmailRelation);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            Application.Current.MainPage = new NavigationPage(new ProfilesBYPESMPage(_BoxId));
        }

        private void CreateBoxPhoneRelation(object sender, EventArgs e, int _BoxId, int _PhoneId)
        {
            //Crear la relación de la box con el teléfono
            string queryCreatePhoneRelation = "INSERT INTO dbo.Box_ProfilePhone ( BoxId, ProfilePhoneId) " +
                                         "VALUES(" + _BoxId + "," + _PhoneId + ")";
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            StringBuilder sb;

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(queryCreatePhoneRelation);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            Application.Current.MainPage = new NavigationPage(new ProfilesBYPESMPage(_BoxId));
        }
    }
}