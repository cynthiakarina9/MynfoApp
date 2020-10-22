namespace Mynfo.Views
{
    using System;
    using System.Data.SqlClient;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilesByPhonePage : ContentPage
    {
        public ProfilesByPhonePage()
        {
            InitializeComponent();

            int UserId = MainViewModel.GetInstance().User.UserId;
            string queryGetPhoneByUser = "select * from dbo.ProfilePhones where dbo.ProfilePhones.UserId = " + UserId;
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            System.Text.StringBuilder sb;

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(queryGetPhoneByUser);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var emailProfile = new Label();
                            var emailAddress = new Label();
                            //var deleteProfile = new Button();
                            var Line = new BoxView();

                            emailProfile.Text = (string)reader["Name"];
                            emailProfile.FontSize = 15;
                            emailProfile.FontAttributes = FontAttributes.Bold;

                            emailAddress.Text = (string)reader["Number"];
                            emailAddress.FontSize = 25;
                            emailAddress.HorizontalTextAlignment = TextAlignment.Center;

                            /*deleteProfile.Text = "B";
                            deleteProfile.TextColor = Color.Black;
                            deleteProfile.FontSize = 10;
                            deleteProfile.BackgroundColor = Color.FromHex("#f9a589");
                            deleteProfile.CornerRadius = 15;
                            deleteProfile.HeightRequest = 30;
                            deleteProfile.WidthRequest = 30;
                            deleteProfile.HorizontalOptions = LayoutOptions.End;
                            deleteProfile.Clicked += new EventHandler((sender, e) => DeleteBoxEmail(sender, e, BoxId, EmailId));*/

                            Line.HeightRequest = 1;
                            Line.Color = Color.FromHex("#FF5521");

                            PhoneList.Children.Add(emailProfile);
                            PhoneList.Children.Add(emailAddress);
                            //EmailList.Children.Add(deleteProfile);
                            PhoneList.Children.Add(Line);
                        }
                    }
                    connection.Close();
                }
            }
        }
        private async void NewProfilePhone_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.CreateProfilePhone = new CreateProfilePhoneViewModel();
            await Navigation.PushAsync(new CreateProfilePhonePage());
        }
    }
}