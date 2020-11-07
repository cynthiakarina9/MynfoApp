namespace Mynfo.Views
{
    using System;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilesByFacebookPage : ContentPage
    {
        #region Constructors
        public ProfilesByFacebookPage()
        {
            InitializeComponent();
            int UserId = MainViewModel.GetInstance().User.UserId;
            string queryGetFacebookByUser = "select * from dbo.ProfileSMs  where dbo.ProfileSMs.UserId = " + UserId +"and RedSocialId = 1";
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            System.Text.StringBuilder sb;

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(queryGetFacebookByUser);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var FacebookProfile = new Label();
                            var emailAddress = new Label();
                            //var deleteProfile = new Button();
                            var editProfile = new ImageButton();
                            var Line = new BoxView();

                            FacebookProfile.Text = (string)reader["ProfileName"];
                            FacebookProfile.FontSize = 25;
                            FacebookProfile.FontAttributes = FontAttributes.Bold;

                            /*deleteProfile.Text = "B";
                            deleteProfile.TextColor = Color.Black;
                            deleteProfile.FontSize = 10;
                            deleteProfile.BackgroundColor = Color.FromHex("#f9a589");
                            deleteProfile.CornerRadius = 15;
                            deleteProfile.HeightRequest = 30;
                            deleteProfile.WidthRequest = 30;
                            deleteProfile.HorizontalOptions = LayoutOptions.End;
                            deleteProfile.Clicked += new EventHandler((sender, e) => DeleteBoxEmail(sender, e, BoxId, EmailId));*/
                            //int FacebookId = (int)reader["ProfileMSId"];
                            //editProfile.Source = "facebook2";
                            //editProfile.BackgroundColor = Color.Transparent;
                            //editProfile.CornerRadius = 15;
                            //editProfile.HeightRequest = 30;
                            //editProfile.WidthRequest = 30;
                            //editProfile.HorizontalOptions = LayoutOptions.End;
                            //editProfile.Clicked += new EventHandler((sender, e) => EditProfileEmail(sender, e, EmailId));

                            Line.HeightRequest = 1;
                            Line.Color = Color.FromHex("#FF5521");

                            FacebookList.Children.Add(FacebookProfile);
                            //FacebookList.Children.Add(emailAddress);
                            //EmailList.Children.Add(deleteProfile);
                            //FacebookList.Children.Add(editProfile);
                            FacebookList.Children.Add(Line);
                        }
                    }
                    connection.Close();
                }
            }
        }
        #endregion

        #region Commands
        private void NewProfileFacebook_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.CreateProfileFacebook = new CreateProfileFacebookViewModel();
            Application.Current.MainPage = new NavigationPage(new CreateProfileFacebookPage());
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Profiles = new ProfilesViewModel();
            Application.Current.MainPage = new NavigationPage(new ProfilesPage());
        }
        #endregion
    }
}