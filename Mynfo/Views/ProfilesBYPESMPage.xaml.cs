using Mynfo.ViewModels;
using System;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mynfo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilesBYPESMPage : ContentPage
    {
        public ProfilesBYPESMPage(int _BoxId, string _ProfileType)
        {
            InitializeComponent();
            #region Variables
            int BoxId = _BoxId;
            int UserId = MainViewModel.GetInstance().User.UserId;
            string queryGetEmailProfiles;
            string queryGetPhoneProfiles;
            string queryGetFacebookProfiles;
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            StringBuilder sb, sb2;

            #endregion

            #region Queries

            queryGetEmailProfiles = "select dbo.ProfileEmails.ProfileEmailId from dbo.ProfileEmails " +
                                            "where dbo.ProfileEmails.UserId = " + UserId + " except select " +
                                            "dbo.Box_ProfileEmail.ProfileEmailId from dbo.Box_ProfileEmail " +
                                            "where dbo.Box_ProfileEmail.BoxId = " + BoxId;
            queryGetPhoneProfiles = "SELECT dbo.ProfilePhones.ProfilePhoneId FROM dbo.ProfilePhones " +
                                            "where dbo.ProfilePhones.UserId = " + UserId + " EXCEPT SELECT " +
                                            "dbo.Box_ProfilePhone.ProfilePhoneId FROM dbo.Box_ProfilePhone " +
                                            "where dbo.Box_ProfilePhone.BoxId = " + BoxId;
            queryGetFacebookProfiles = "SELECT dbo.ProfileSMs.ProfileMSId FROM dbo.ProfileSMs " +
                                            "where dbo.ProfileSMs.UserId = " + UserId + " EXCEPT SELECT " +
                                            "dbo.Box_ProfileSM.ProfileMSId FROM dbo.Box_ProfileSM " +
                                            "where dbo.Box_ProfileSM.BoxId = " + BoxId;

            #endregion

            #region Commands

            BackDetails.Clicked += new EventHandler((sender, e) => Back_Clicked(sender, e, _BoxId));
            RefreshCommand = new Command(async () => await LoadPublications());
            GoToProfiles.Clicked += new EventHandler((sender, e) => GoToProfiles_Clicked(sender, e, _BoxId,_ProfileType));

            #endregion

            #region Consultas

            switch (_ProfileType)
            {
                case "Phone":
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
                    break;
                case "Email":
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
                    break;
                case "Facebook":
                    //Consulta para obtener Facebook
                    using (SqlConnection connection = new SqlConnection(cadenaConexion))
                    {
                        sb = new System.Text.StringBuilder();
                        sb.Append(queryGetFacebookProfiles);
                        string sql = sb.ToString();

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int ProfileSMId = (int)reader["ProfileMSId"];
                                    string queryGetProfile = "select * from dbo.ProfileSMs where dbo.ProfileSMs.ProfileMSId = " + ProfileSMId;

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
                                                    var SMProfile = new Label();
                                                    var CreateRelation = new Button();
                                                    var Line = new BoxView();

                                                    SMProfile.Text = (string)reader2["ProfileName"];
                                                    SMProfile.FontSize = 25;
                                                    SMProfile.FontAttributes = FontAttributes.Bold;
                                                    SMProfile.HorizontalTextAlignment = TextAlignment.Center;

                                                    CreateRelation.Text = "+";
                                                    CreateRelation.TextColor = Color.Black;
                                                    CreateRelation.FontSize = 10;
                                                    CreateRelation.BackgroundColor = Color.FromHex("#f9a589");
                                                    CreateRelation.CornerRadius = 15;
                                                    CreateRelation.HeightRequest = 30;
                                                    CreateRelation.WidthRequest = 30;
                                                    CreateRelation.HorizontalOptions = LayoutOptions.End;
                                                    CreateRelation.Clicked += new EventHandler((sender, e) => CreateBoxSMRelation(sender, e, BoxId, ProfileSMId));

                                                    Line.HeightRequest = 1;
                                                    Line.Color = Color.FromHex("#FF5521");

                                                    ProfileList.Children.Add(SMProfile);
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
                    break;
                default:
                    break;
            }

            #endregion
        }
        private void GoToProfiles_Clicked(object sender, EventArgs e, int _boxId, string _profileType)
        {
            var mainViewModel = MainViewModel.GetInstance();
            switch (_profileType)
            {
                case "Phone":
                    mainViewModel.CreateProfilePhone = new CreateProfilePhoneViewModel();
                    Navigation.PushAsync(new CreateProfilePhonePage());
                    break;
                case "Email":
                    mainViewModel.CreateProfileEmail = new CreateProfileEmailViewModel();
                    Navigation.PushAsync(new CreateProfileEmailPage());
                    break;
                case "Facebook":
                    mainViewModel.CreateProfileFacebook = new CreateProfileFacebookViewModel();
                    Navigation.PushAsync(new CreateProfileFacebookPage());
                    break;
                default:
                    break;
            }
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
            Application.Current.MainPage = new NavigationPage(new ProfilesBYPESMPage(_BoxId, "Email"));
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
            Application.Current.MainPage = new NavigationPage(new ProfilesBYPESMPage(_BoxId, "Phone"));
        }

        private void CreateBoxSMRelation(object sender, EventArgs e, int _BoxId, int _SMId)
        {
            //Crear la relación de la box con Facebook
            string queryCreateSMRelation = "INSERT INTO dbo.Box_ProfileSM ( BoxId, ProfileMSId) " +
                                         "VALUES(" + _BoxId + "," + _SMId + ")";
            string cadenaConexion = @"data source=serverappmyinfonfc.database.windows.net;initial catalog=mynfo;user id=adminatxnfc;password=4dmiNFC*Atx2020;Connect Timeout=60";
            StringBuilder sb;

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                sb = new System.Text.StringBuilder();
                sb.Append(queryCreateSMRelation);
                string sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            Application.Current.MainPage = new NavigationPage(new ProfilesBYPESMPage(_BoxId, "Facebook"));
        }

        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set { _isRefreshing = value; OnPropertyChanged(); }
        }

        public ICommand RefreshCommand { private set; get; }


        // methods - code omitted

        async Task LoadPublications()
        {
            // code omitted

            IsRefreshing = false;
        }
        private void Back_Clicked(object sender, EventArgs e, int _BoxId)
        {
            var mainViewModel = MainViewModel.GetInstance();
            Application.Current.MainPage = new NavigationPage(new ProfileTypeSelection(_BoxId));
        }
    }
}