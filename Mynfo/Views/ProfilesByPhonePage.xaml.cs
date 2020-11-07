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
                            var editProfile = new ImageButton();
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
                            int PhoneId = (int)reader["ProfilePhoneId"];
                            editProfile.Source = "edit2";
                            editProfile.BackgroundColor = Color.Transparent;
                            editProfile.CornerRadius = 15;
                            editProfile.HeightRequest = 30;
                            editProfile.WidthRequest = 30;
                            editProfile.HorizontalOptions = LayoutOptions.End;
                            editProfile.Clicked += new EventHandler((sender, e) => EditProfilePhone(sender, e, PhoneId));

                            Line.HeightRequest = 1;
                            Line.Color = Color.FromHex("#FF5521");

                            PhoneList.Children.Add(emailProfile);
                            PhoneList.Children.Add(emailAddress);
                            //EmailList.Children.Add(deleteProfile);
                            PhoneList.Children.Add(editProfile);
                            PhoneList.Children.Add(Line);
                        }
                    }
                    connection.Close();
                }
            }
        }
        private void NewProfilePhone_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.CreateProfilePhone = new CreateProfilePhoneViewModel();
            Application.Current.MainPage = new NavigationPage(new CreateProfilePhonePage());
        }

        private void EditProfilePhone(object sender, EventArgs e, int _ProfilePhoneId)
        {
            Application.Current.MainPage = new NavigationPage(new EditProfilePhonePage(_ProfilePhoneId));
        }

        #region Methods  

        private void Back_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Profiles = new ProfilesViewModel();
            Application.Current.MainPage = new NavigationPage(new ProfilesPage());
        }

        #endregion
    }
}